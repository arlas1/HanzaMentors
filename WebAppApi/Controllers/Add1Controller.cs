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
        var menteeViewModel = new AddMenteeViewModel();
        menteeViewModel.Mentors = bll.Mentors.GetAll();
        menteeViewModel.FactorySupervisors = bll.FactorySupervisors.GetAll();
        menteeViewModel.InternSupervisors = bll.InternSupervisors.GetAll();
        
        return View(menteeViewModel);
    }
    
    public IActionResult DocumentSample()
    {
        return View();
    }
}