using Microsoft.AspNetCore.Mvc;

namespace WebAppApi.Controllers;

public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
}