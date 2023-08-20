using Microsoft.AspNetCore.Mvc;
using LoginRegister3DLayer.Core.Interface;
using LoginRegister3DLayer.Core.ViewModels;
using LoginRegister3DLayer.Database.Context;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace LoginRegister3DLayer.Controllers;

public class HomeController : Controller
{
    IAccount _account;
    private readonly DatabaseContext _context;
    public HomeController(IAccount account,DatabaseContext context)
    {
        _account = account;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel register)
    {
        if (ModelState.IsValid)
        {
            if (register.Password!=register.RePassword)
            {
                ModelState.AddModelError("RePassword", "Passwords are not equal");
            }
            var mobile = _context.Users.SingleOrDefault(f => f.Mobile == register.Mobile);
            if (mobile!=null)
            {
                ModelState.AddModelError("Mobile", "This phone number is already registered");
                return View(register);
            }

            if (await _account.AddUser(register))
            {
                return RedirectToAction(nameof(Login));
            }

            return View(register);
        }
        return View(register);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel login)
    {
        if (ModelState.IsValid)
        {
            var user = await _account.LoginUser(login);
            if (user!=null)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.Mobile),
                    new Claim(ClaimTypes.Role,user.Role.RoleName),
                };
                var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                var principale = new ClaimsPrincipal(identity);

                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true, //remember me
                };
                await HttpContext.SignInAsync(principale, properties);

                //Redirect base on user role(admin/user)
                if (user.Role.RoleName == "admin")
                {
                    return RedirectToAction("AdminView","Profile");
                }
                return RedirectToAction("UserView", "Profile");
            }
        }
        return View(login);
    }

}
