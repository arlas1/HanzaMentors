using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebAppApi.Controllers;

public class Details1Controller : Controller
{
    [HttpGet]
    public IActionResult Mentor(string mentorId)
    {
        var details = new DetailsViewModel
        {
            InitialMentorId = Guid.Parse(mentorId)
        };
        
        return View(details);
    }
    
    [HttpGet]
    public IActionResult EmployeeMentee(string menteeId)
    {
        var details = new DetailsViewModel
        {
            EmployeeId = Guid.Parse(menteeId)
        };
        
        return View(details);
    }
    
    [HttpGet]
    public IActionResult InternMentee(string menteeId)
    {
        var details = new DetailsViewModel
        {
            EmployeeId = Guid.Parse(menteeId)
        };
        
        return View(details);
    }
}