using event_page_web.Data;
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

