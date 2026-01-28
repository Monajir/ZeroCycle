using Microsoft.AspNetCore.Mvc;

namespace SmartWaste.Web.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet("/Auth")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Access";
            return View();
        }
    }
}
