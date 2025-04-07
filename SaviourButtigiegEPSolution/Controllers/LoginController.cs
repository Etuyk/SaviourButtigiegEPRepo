using Microsoft.AspNetCore.Mvc;

namespace SaviourButtigiegEP.Presentation.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                HttpContext.Session.SetString("UserId", username);
                return RedirectToAction("Index", "Poll");
            }

            ViewBag.Error = "Username is required.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Index");
        }
    }
}
