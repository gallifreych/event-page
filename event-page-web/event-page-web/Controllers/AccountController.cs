using event_page_web.Data;
using event_page_web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Threading.Tasks;

public class AccountController : Controller

{
    private readonly MyDbContext _context;
    public AccountController(MyDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult Register()
    {
        var model = new User();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Register(User model)
    {
        var yeniKullanici = new User
        {
            Kullanici_Adi = model.Kullanici_Adi,
            Sifre = model.Sifre,
            Yetki = "Guest"
        };

        _context.kullanıcılar.Add(yeniKullanici);
        await _context.SaveChangesAsync();

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, model.Kullanici_Adi),
        new Claim(ClaimTypes.Role, "Guest")
    };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
   
    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = await _context.kullanıcılar
            .FirstOrDefaultAsync(u => u.Kullanici_Adi == username && u.Sifre == password);

        if (user != null)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Kullanici_Adi),
            new Claim(ClaimTypes.Role, user.Yetki)
        };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Kullanıcı adı veya şifre yanlış.";
        return View();
    }
}
