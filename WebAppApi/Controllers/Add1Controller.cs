using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebAppApi.Controllers;

public class Add1Controller(IAppBLL bll): Controller
{
    public IActionResult Supervisor()
    {
        return View();
    }
    
    public IActionResult Mentor()
    {
        return View();
    }
    
    public IActionResult Mentee()
    {
        // var menteeViewModel = new AddMenteeViewModel
        // {
        //     Mentors = bll.Mentors.GetAll(),
        //     FactorySupervisors = bll.FactorySupervisors.GetAll(),
        //     InternSupervisors = bll.InternSupervisors.GetAll()
        // };
        //
        // return View(menteeViewModel);
        return View();
    }
    
    public IActionResult DocumentSample()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult DocumentIntern(string menteeId)
    {
        var viewModel = new MenteeViewModel()
        {
            MenteeId = Guid.Parse(menteeId)
        };
        
        return View(viewModel);
    }
    
    [HttpGet]
    public IActionResult DocumentEmployee(string menteeId)
    {
        var viewModel = new MenteeViewModel()
        {
            MenteeId = Guid.Parse(menteeId)
        };
        
        return View(viewModel);
    }
}