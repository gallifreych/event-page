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
                
            }

            return View(); // Views/Home/Index.cshtml 
        }
    } 
}
