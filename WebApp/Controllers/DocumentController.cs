using App.BLL.Contracts;
using App.Domain.Identity;
using App.Helpers.EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class DocumentController (IAppBLL bll, UserManager<AppUser> userManager) : Controller
{
    public IActionResult Documents()
    {
        var documentsViewModel = new DocumentsViewModel
        {
            DocumentSamples = bll.DocumentSamples.GetAll(),
            InternMentorshipDocuments = bll.InternMentorshipDocuments.GetAll(),
            EmployeeMentorshipDocuments = bll.EmployeeMentorshipDocuments.GetAll()
        };
        
        return View(documentsViewModel);
    }

    public IActionResult DeleteSampleDocument(Guid documentId)
    {
        bll.DocumentSamples.Remove(bll.DocumentSamples.FirstOrDefault(documentId)!);
        bll.SaveChangesAsync();
        
        return RedirectToAction("Documents", "Document");
    }
    
    public IActionResult DeleteInternDocument(Guid documentId)
    {
        bll.InternMentorshipDocuments.Remove(bll.InternMentorshipDocuments.FirstOrDefault(documentId)!);
        bll.SaveChangesAsync();
        
        return RedirectToAction("Documents", "Document");
    }
    
    public IActionResult DeleteEmployeeDocument(Guid documentId)
    {
        bll.EmployeeMentorshipDocuments.Remove(bll.EmployeeMentorshipDocuments.FirstOrDefault(documentId)!);
        bll.SaveChangesAsync();
        
        return RedirectToAction("Documents", "Document");
    }
}