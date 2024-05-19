using App.BLL.Contracts;
using App.Domain.Identity;
using App.Helpers.EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class DocumentController (IAppBLL bll, UserManager<AppUser> userManager) : Controller
{
    public IActionResult DocumentSamples()
    {
        var documentsViewModel = new DocumentsViewModel
        {
            DocumentSamples = bll.DocumentSamples.GetAll(),
        };
        
        return View(documentsViewModel);
    }

    public IActionResult MenteeEmployeeDocuments()
    {
        var documentsViewModel = new DocumentsViewModel
        {
            EmployeeMentorshipDocuments = bll.EmployeeMentorshipDocuments.GetAll()
        };
        
        return View(documentsViewModel);
    }
    
    public IActionResult MenteeInternsDocuments()
    {
        var documentsViewModel = new DocumentsViewModel
        {
            InternMentorshipDocuments = bll.InternMentorshipDocuments.GetAll()
        };
        
        return View(documentsViewModel);
    }
    
    public IActionResult DeleteSampleDocument(Guid documentId)
    {
        bll.DocumentSamples.Remove(bll.DocumentSamples.FirstOrDefault(documentId)!);
        bll.SaveChangesAsync();
        
        return RedirectToAction("DocumentSamples", "Document");
    }
    
    public IActionResult DeleteInternDocument(Guid documentId)
    {
        bll.InternMentorshipDocuments.Remove(bll.InternMentorshipDocuments.FirstOrDefault(documentId)!);
        bll.SaveChangesAsync();
        
        return RedirectToAction("MenteeInternsDocuments", "Document");
    }
    
    public IActionResult DeleteEmployeeDocument(Guid documentId)
    {
        bll.EmployeeMentorshipDocuments.Remove(bll.EmployeeMentorshipDocuments.FirstOrDefault(documentId)!);
        bll.SaveChangesAsync();
        
        return RedirectToAction("MenteeEmployeeDocuments", "Document");
    }
}