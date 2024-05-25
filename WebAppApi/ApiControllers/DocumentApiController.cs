using System.Net;
using App.BLL.Contracts;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApp.DTO;
using WebApp.Models;

namespace WebAppApi.ApiControllers;

[ApiVersion( "1.0" )]
[ApiController]
[Route("/api/v{version:apiVersion}/[controller]/[action]")]
public class DocumentApiController(IAppBLL bll) : ControllerBase
{
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType<DocumentsViewModel>((int)HttpStatusCode.OK)]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetSamples()
    {
        var documentsViewModel = new DocumentsViewModel
        {
            DocumentSamples = await bll.DocumentSamples.GetAllAsync()
        };
        
        return Ok(documentsViewModel);
    }
    
    
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType<DocumentsViewModel>((int)HttpStatusCode.OK)]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetMenteeEmployeeDocuments()
    {
        var documentsViewModel = new DocumentsViewModel
        {
            EmployeeMentorshipDocuments = await bll.EmployeeMentorshipDocuments.GetAllAsync()
        };
        
        return Ok(documentsViewModel);
    }
    
    
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType<DocumentsViewModel>((int)HttpStatusCode.OK)]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetMenteeInternsDocuments()
    {
        var documentsViewModel = new DocumentsViewModel
        {
            InternMentorshipDocuments = await bll.InternMentorshipDocuments.GetAllAsync()
        };
        
        return Ok(documentsViewModel);
    }
    
    
    [HttpPost]
    [Produces("application/octet-stream")]
    [Consumes("application/json")]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    public IActionResult DownloadSampleDocument([FromBody] string documentId)
    {
        var pdfDoc = bll.DocumentSamples.GetAll()
            .ToList()
            .FirstOrDefault(me => me.Id.Equals(Guid.Parse(documentId)));
        byte[] fileBytes = Convert.FromBase64String(pdfDoc!.Base64Code!);
        
        return File(fileBytes, "application/pdf", $"{pdfDoc.Id}.pdf");
        
    }
    
    
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult DeleteSampleDocument([FromBody] string documentId)
    {
        bll.DocumentSamples.Remove(bll.DocumentSamples.FirstOrDefault(Guid.Parse(documentId))!);
        bll.SaveChangesAsync();
        
        return Ok();
    }
    
    
    [HttpPost]
    [Produces("application/octet-stream")]
    [Consumes("application/json")]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    public IActionResult DownloadInternDocument([FromBody] string documentId)
    {
        var pdfDoc = bll.InternMentorshipDocuments.GetAll()
            .ToList()
            .FirstOrDefault(me => me.Id.Equals(Guid.Parse(documentId)));
        byte[] fileBytes = Convert.FromBase64String(pdfDoc!.Base64Code!);
        
        // return File(fileBytes, "application/pdf", pdfDoc.Id + ".pdf");
        return File(fileBytes, "application/pdf", $"{pdfDoc.Id}.pdf");    }
    
    
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult DeleteInternDocument([FromBody] string documentId)
    {
        var signingTimes = bll.DocumentSigningTimes.GetAll()
            .Where(time => time.InternMentorshipDocumentId.Equals(Guid.Parse(documentId))!);
        
        if (!signingTimes.IsNullOrEmpty())
        {
            foreach (var time in signingTimes)
            {
                bll.DocumentSigningTimes.Remove(time);
            }
        }
        
        bll.InternMentorshipDocuments.Remove(bll.InternMentorshipDocuments.FirstOrDefault(Guid.Parse(documentId))!);
        bll.SaveChangesAsync();
        
        return Ok();
    }
    
    
    [HttpPost]
    [Produces("application/octet-stream")]
    [Consumes("application/json")]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    public IActionResult DownloadEmployeeDocument([FromBody] string documentId)
    {
        var pdfDoc = bll.EmployeeMentorshipDocuments.GetAll()
            .ToList()
            .FirstOrDefault(me => me.Id.Equals(Guid.Parse(documentId)));
        byte[] fileBytes = Convert.FromBase64String(pdfDoc!.Base64Code!);
        
        return File(fileBytes, "application/pdf", $"{pdfDoc.Id}.pdf");
        
    }
    
    
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult DeleteEmployeeDocument([FromBody] string documentId)
    {
        var signingTimes = bll.DocumentSigningTimes.GetAll()
            .Where(time => time.EmployeeMentorshipDocumentId.Equals(Guid.Parse(documentId))!);
        
        if (!signingTimes.IsNullOrEmpty())
        {
            foreach (var time in signingTimes)
            {
                bll.DocumentSigningTimes.Remove(time);
            }
        }
        
        bll.EmployeeMentorshipDocuments.Remove(bll.EmployeeMentorshipDocuments.FirstOrDefault(Guid.Parse(documentId))!);
        bll.SaveChangesAsync();
        
        return Ok();
    }
}