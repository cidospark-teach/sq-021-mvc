using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SQ021_First_Web_App.Models.Entity;
using SQ021_First_Web_App.Models.ViewModels;
using SQ021_First_Web_App.Services.Interfaces;
using System.Security.Claims;

namespace SQ021_First_Web_App.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signManager;
        private readonly IEmailService _emailService;
        public AccountController(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signManager = signInManager;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(SignupViewModel model)
        {
           if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    ModelState.AddModelError("", "Email already taken!");
                }

                var userToAdd = new AppUser
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email
                };

                var identityResult = await _userManager.CreateAsync(userToAdd, model.Password);
                if(identityResult.Succeeded)
                {
                    // add the regular role to the user
                    await _userManager.AddToRoleAsync(userToAdd, "regular");

                    // add the CanAdd claim to a regular user
                    var canAddClaim = new Claim("CanAdd", "true");
                    await _userManager.AddClaimAsync(userToAdd, canAddClaim);

                    // send confirmation link to email provided on sign up
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(userToAdd);
                    var link = Url.Action("ConfirmEmail", "Account", new { userToAdd.Email, token }, Request.Scheme);
                    
                    var body = @$"
                        <h1>Your email confirmation link</h1>
                        <p>
                            {link}
                        </p>
                    ";

                    var response = await _emailService.SendEmail(model.Email, "Confrim email link", body);

                    return RedirectToAction("Index", "Home");
                }

                foreach(var err in identityResult.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
            }

           return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Login(string? ReturnUrl)
        {
            if(!string.IsNullOrEmpty(ReturnUrl))
                ViewBag.ReturnUrl = ReturnUrl;  

            if (_signManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    if (await _userManager.IsEmailConfirmedAsync(user))
                    {
                        var signInResult = await _signManager.PasswordSignInAsync(user, model.Password,
                            model.RememberMe, false);
                        if (signInResult.Succeeded)
                        {
                            if(string.IsNullOrEmpty(returnUrl))
                                return RedirectToAction("Index", "Home");

                            return LocalRedirect(returnUrl);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email is not confrimed yet!");
                    }
                }
                else
                {

                    ModelState.AddModelError("", "Invaid credential!");

                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string Email, string token)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if(result.Succeeded)
                {
                    return RedirectToAction("CongratulatoryPage", "Account");
                }
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult CongratulatoryPage()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
