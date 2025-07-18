using event_page_web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace event_page_web.Controllers
{

    public class HomeController : Controller
    {
        private readonly MyDbContext _context;

        public HomeController(MyDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "admin,guest")]
        public IActionResult ViewEvents()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult CreateEvent(int id)
        {
            return View();
        }
        // GET: /Home/Index
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Username = User.Identity.Name;
                
            }

            var Etkinlik = _context.Etkinlikler.OrderBy(e => e.Date).ToList();
            return View(Etkinlik);
        }
    } 
}

