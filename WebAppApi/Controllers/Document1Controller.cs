using System.Runtime.Intrinsics.Arm;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

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
    
    [HttpGet]
    public IActionResult SigningTimesEmployee(string documentId)
    {
        var viewModel = new SignTimeViewModel()
        {
            DocumentId = Guid.Parse(documentId)
        };
        
        return View(viewModel);
    }
    
    [HttpGet]
    public IActionResult SigningTimesIntern(string documentId)
    {
        var viewModel = new SignTimeViewModel()
        {
            DocumentId = Guid.Parse(documentId)
        };
        
        return View(viewModel);
    }
}