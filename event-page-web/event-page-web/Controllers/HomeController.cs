using Microsoft.AspNetCore.Mvc;

namespace event_page_web.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Username = User.Identity.Name;
                TempData["Merhaba"] = $"Başarıyla {User.Identity.Name} Hesabına Giriş Yapıldı.";
            }
            return View(); // Views/Home/Index.cshtml 
        }
    } 
}
