using Microsoft.AspNetCore.Mvc;

namespace WebAppApi.Controllers;

public class Mentee1Controller: Controller
{
    public IActionResult EmployeeMentee()
    {
        return View();
    }
    
    public IActionResult InternMentee()
    {
        return View();
    }
}