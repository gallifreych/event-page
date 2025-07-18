using event_page_web.Models;
using Microsoft.AspNetCore.Mvc;
using event_page_web.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace event_page_web.Controllers
{
    public class EventController : Controller
    {
        private readonly MyDbContext _context;

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public EventController(MyDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var Etkinlik = _context.Etkinlikler.OrderBy(e => e.Date).ToList();
            return View(Etkinlik);
        }


        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        //[HttpPost]
        // public async Task<IActionResult> Create(Event newEvent)
        // {
        //   if (ModelState.IsValid)
        // {
        //   _context.Etkinlikler.Add(newEvent);
        // await _context.SaveChangesAsync();
        // return RedirectToAction("Index", "Home");
        //}
        //return View(newEvent);
        //}
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var etkinlik = await _context.Etkinlikler.FindAsync(id);
            if (etkinlik != null)
            {
                _context.Etkinlikler.Remove(etkinlik);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Event model, IFormFile? cover)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (cover != null && cover.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(cover.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await cover.CopyToAsync(stream);
                }

                model.CoverImagePath = "/uploads/" + uniqueFileName;
            }

            _context.Etkinlikler.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index","Home");
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var etkinlik = await _context.Etkinlikler.FindAsync(id);
            if (etkinlik == null)
            {
                return NotFound();
            }
            return View(etkinlik);
        }
        [Authorize(Roles ="Admin" )]
        [HttpPost]
        public async Task<IActionResult> Edit(Event model, IFormFile? cover)
        {
            if (ModelState.IsValid)
            {
                if (cover != null && cover.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    Directory.CreateDirectory(uploadsFolder);
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(cover.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await cover.CopyToAsync(stream);
                    }
                    model.CoverImagePath = "/uploads/" + uniqueFileName;
                }
                _context.Etkinlikler.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

    }
}
        