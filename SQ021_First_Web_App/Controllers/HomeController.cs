using Microsoft.AspNetCore.Mvc;
using SQ021_First_Web_App.Models;
using SQ021_First_Web_App.Models.ViewModels;
using SQ021_First_Web_App.Services;
using SQ021_First_Web_App.Services.Interfaces;
using System.Diagnostics;

namespace SQ021_First_Web_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailService _emailService;
        public HomeController(ILogger<HomeController> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;   
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       
        [HttpGet("contact-page")]
        public IActionResult ContactPage()
        {
            return View();
        }

        [HttpPost("contact-page")]
        public async Task<IActionResult> ContactPage(ContactViewModel model)
        {
            if(ModelState.IsValid)
            {
                var body = @$"
                        <h1>Message sending test!</h1>
                        <p>
                            {model.Message}
                        </p>
                ";

                var response = await _emailService.SendEmail(model.RecipientEmail, model.Subject, body); ;
                if(string.IsNullOrEmpty(response))
                {
                    return RedirectToAction("index");
                }

                ModelState.AddModelError("", response);
            }

            return View(model);
        }
    }
}
