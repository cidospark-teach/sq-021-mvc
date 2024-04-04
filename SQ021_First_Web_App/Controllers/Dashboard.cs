using Microsoft.AspNetCore.Mvc;

namespace SQ021_First_Web_App.Controllers
{
    public class Dashboard : Controller
    {
        public Dashboard()
        {

        }

        public IActionResult Index() {
            ViewBag.CurrentPage = "overview";
            return View(); 
        }
        public IActionResult ManageRoles()
        {
            ViewBag.CurrentPage = "manage-roles";
            return View(); 
        }
        public IActionResult ManageClaims()
        {
            ViewBag.CurrentPage = "manage-claims"; 
            return View(); 
        }
    }
}
