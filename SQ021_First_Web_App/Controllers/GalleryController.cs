using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQ021_First_Web_App.Data;
using SQ021_First_Web_App.Models.Entity;
using SQ021_First_Web_App.Models.ViewModels;
using SQ021_First_Web_App.Services.Interfaces;

namespace SQ021_First_Web_App.Controllers
{
    [Authorize]
    public class GalleryController : Controller
    {
        //private readonly MyDbContext _ctx;
        private readonly IImageService _imageService;
        private readonly IDogRepositoryService _dogRepositorySerivce;
        public GalleryController(IDogRepositoryService dogRepositoryService, IImageService imageService)
        {
            _dogRepositorySerivce = dogRepositoryService;
            _imageService = imageService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index(string? id)
        {
            List<DogPortriat> dogPortriats = new List<DogPortriat>();

            var dogs = _dogRepositorySerivce.GetAsync();
            if(dogs != null && dogs.Any())
            {
                dogPortriats = dogs.Select(x => new DogPortriat
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    PhotoUrl = x.PhotoUrl
                }).ToList();
            }

            var viewModel = new GalleryViewModel { 
                DogPortriats = dogPortriats.OrderByDescending(x => x.Id).ToList(),
                
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ViewDetail(string id)
        {
            DogPortriat dogPortriat = new DogPortriat();

            var dog = await _dogRepositorySerivce.GetAsync(id);

            if(dog == null)
            {
                ViewBag.ErrMsg = $"The dog with id: {id} was not found";
                return View();
            }

            dogPortriat = new DogPortriat
            {
                Id = dog.Id,
                Name = dog.Name,
                Description = dog.Description,
                PhotoUrl = dog.PhotoUrl
            };

            return View(dogPortriat);
        }


        [HttpGet]
        public IActionResult AddNew()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(AddDogViewModel model)
        {
            // validating the model
            if (ModelState.IsValid)
            {
                Dog newDog = new Dog
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = model.Name,
                    Description = model.Description,
                    PhotoUrl = ""
                };

                // add new input to the existing list
                await _dogRepositorySerivce.AddDogAsync(newDog);

                return RedirectToAction("Index");
            }

            //ModelState.AddModelError("xyz","message");
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var dog = await _dogRepositorySerivce.GetAsync(id);
            if(dog != null)
            {
                await _dogRepositorySerivce.DeleteDogAsync(dog);
            }

            return RedirectToAction("Index");
        }

       
        [HttpGet]
        [Authorize(Roles = "admin, editor")]
        [Authorize(Roles = "regular")]
        public async Task<IActionResult> Update(string id)
        {
            UpdateDogViewModel dogPortriat = new UpdateDogViewModel();
            
            var dog = await _dogRepositorySerivce.GetAsync(id);
            dogPortriat = new UpdateDogViewModel
            {
                Name = dog.Name,
                Description = dog.Description
            };
            
            return View(dogPortriat);
        }

        [HttpPost]
        [Authorize(Roles = "admin, editor")]
        [Authorize(Roles = "regular")]
        public async Task<IActionResult> Update(string id, UpdateDogViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dog = await _dogRepositorySerivce.GetAsync(id);
                if(dog != null) { 
                    dog.Name = model.Name;
                    dog.Description = model.Description;
                    await _dogRepositorySerivce.UpdateDogAsync(dog);

                    return RedirectToAction("index");
                }

                ModelState.AddModelError("Id mismatch!", $"No record matched id: {id}");
            }

            return View(model);
        }


        [HttpGet]
        [Authorize(Roles = "admin, editor")]
        public async Task<IActionResult> ImageUpload(string id)
        {
            var dog = await _dogRepositorySerivce.GetAsync(id);
            var viewModel = new ImageUploadViewModel();

            if (dog != null)
            {
                viewModel.Id = dog.Id;
                viewModel.DogName = dog.Name;
            }

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin, editor")]
        public async Task<IActionResult> ImageUpload(ImageUploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dog = await _dogRepositorySerivce.GetAsync(model.Id);
                if(dog != null)
                {
                    // upload image using image upload service
                    var response = _imageService.UploadImage(model.Photo, dog);
                    if (response.ContainsKey(200))
                    {
                        dog.PhotoUrl = response[200];
                        await _dogRepositorySerivce.UpdateDogAsync(dog);
                        return RedirectToAction("index");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", response[400]);
                    }
                }
                else
                {
                    ModelState.AddModelError("Not found", $"Dog with Id: {model.Id} was not found.");
                }
            }
            return View(model);
        }
    }
}
