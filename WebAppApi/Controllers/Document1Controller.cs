using Microsoft.AspNetCore.Mvc;

namespace WebAppApi.Controllers;

public class Document1Controller : Controller
{
    public IActionResult DocumentSamples()
    {
        return View();
    }
    
    public IActionResult MenteeEmployeeDocuments()
    {
        return View();
    }
    
    public IActionResult MenteeInternsDocuments()
    {
        return View();
    }
}