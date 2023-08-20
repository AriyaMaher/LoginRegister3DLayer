using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginRegister3DLayer.Controllers;

[Authorize]
public class ProfileController : Controller
{
    public IActionResult UserView()
    {
        return View();
    }

    public IActionResult AdminView()
    {
        return View();
    }
}
