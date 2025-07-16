using event_page_web.Models;
using Microsoft.AspNetCore.Mvc;
using event_page_web.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace event_page_web.Controllers
{
    public class EventController : Controller
    {
        private readonly MyDbContext _context;

        public EventController(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var etkinlikler = await _context.Etkinlikler.ToListAsync();
            return View(etkinlikler);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Event newEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Etkinlikler.Add(newEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(newEvent);
        }
    }
}
