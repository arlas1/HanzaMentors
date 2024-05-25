using Microsoft.AspNetCore.Mvc;

namespace WebAppApi.Controllers;

public class Mentor1Controller : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}