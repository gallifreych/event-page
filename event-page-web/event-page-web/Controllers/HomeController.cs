using Microsoft.AspNetCore.Mvc;

namespace event_page_web.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index
        public IActionResult Index()
        {
            return View(); // Views/Home/Index.cshtml 
        }
    } 
}
