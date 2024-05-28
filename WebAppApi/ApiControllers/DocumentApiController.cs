using System.Net;
using App.BLL.Contracts;
using App.Helpers.EmailService;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApp.DTO;
using WebApp.Models;
using WebAppApi.Models;

namespace WebAppApi.ApiControllers;

/// <summary>
/// Api controller storing actions related to document processing side of the application
/// </summary>
/// <param name="bll"></param>
/// <param name="emailService"></param>
[ApiVersion( "1.0" )]
[ApiController]
[Route("/api/v{version:apiVersion}/[controller]/[action]")]
public class DocumentApiController(IAppBLL bll, IEmailService emailService) : ControllerBase
{
    /// <summary>
    /// Api action loading the page with all of the samples available in the application
    /// </summary>
    /// <returns>DocumentsViewModel</returns>
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
    
    
    /// <summary>
    /// Api action loading employee mentee related documents
    /// </summary>
    /// <returns>DocumentsViewModel</returns>
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
    
    
    /// <summary>
    /// Api action loading intern mentee related documents
    /// </summary>
    /// <returns>DocumentsViewModel</returns>
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
    
    
    /// <summary>
    /// Api action for downloading document sample
    /// </summary>
    /// <param name="documentId"></param>
    /// <returns>void</returns>
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
    
    
    /// <summary>
    /// Api action for deleting document sample
    /// </summary>
    /// <param name="documentId"></param>
    /// <returns></returns>
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
    
    
    /// <summary>
    /// Api action for downloading intern mentee document
    /// </summary>
    /// <param name="documentId"></param>
    /// <returns>void</returns>
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
    
    
    /// <summary>
    /// Api action for deleting intern mentee document
    /// </summary>
    /// <param name="documentId"></param>
    /// <returns>void</returns>
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
    
    
    /// <summary>
    ///  Api action for downloading employee mentee document
    /// </summary>
    /// <param name="documentId"></param>
    /// <returns>void</returns>
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
    
    
    /// <summary>
    /// Api action for deleting employee mentee document
    /// </summary>
    /// <param name="documentId"></param>
    /// <returns>void</returns>
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
    
    
    /// <summary>
    /// Api action for loading employee mentee document signing page
    /// </summary>
    /// <param name="documentId"></param>
    /// <returns>SignTimeDTO</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType<SignTimeDTO>((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult GetSigningTimesEmployee([FromBody] string documentId)
    {
        var signingTimes = bll.DocumentSigningTimes.GetAll()
            .Where(time => time.EmployeeMentorshipDocumentId.Equals(Guid.Parse(documentId)));

        var signTimesViewModel = new SignTimeDTO
        {
            AvailableTimes = new List<string>(),
            DocumentId = documentId,
        };

        foreach (var signingTime in signingTimes)
        {
            signTimesViewModel.AvailableTimes.Add(signingTime.Time!);
        }
        
        return Ok(signTimesViewModel);
    }
    
    
    /// <summary>
    /// Api action for choosing signing times for employee mentee document
    /// </summary>
    /// <param name="request"></param>
    /// <returns>void</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChoseSigningTimeEmployee([FromBody] SignTimeDTO request)
    {
        var document = await bll.EmployeeMentorshipDocuments.FirstOrDefaultAsync(Guid.Parse(request.DocumentId));

        document!.DocumentStatus = "Signing option chosen";
        document.ChoosenSigningTime = request.ChosenTime;
        document.WayOfSigning = request.ChosenWay;

        bll.EmployeeMentorshipDocuments.Update(document);
        await bll.SaveChangesAsync();

        var mentorship = (await bll.EmployeeMentorships.GetAllAsync())
            .FirstOrDefault(m => m.Id.Equals(document.EmployeeMentorshipId));
        
        var employee = (await bll.Employees.GetAllAsync())
            .FirstOrDefault(m => m.Id.Equals(mentorship!.EmployeeId));
        
        var title = "";
        var emailBody = emailService.GenerateDocSignEmailBody(
            document.Title ?? title,
            document.ChoosenSigningTime!,
            $"{employee!.FirstName} {employee.LastName}");
        await emailService.SendEmailAsync("lasimer0406@gmail.com", "Mentee-Employee have chosen signing time", emailBody);
        
        return Ok();
    }
    
    
    /// <summary>
    /// Api action for loading intern mentee document signing page
    /// </summary>
    /// <param name="documentId"></param>
    /// <returns>SignTimeDTO</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType<SignTimeDTO>((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult GetSigningTimesIntern([FromBody] string documentId)
    {
        var signingTimes = bll.DocumentSigningTimes.GetAll()
            .Where(time => time.InternMentorshipDocumentId.Equals(Guid.Parse(documentId)));

        var signTimesViewModel = new SignTimeDTO
        {
            AvailableTimes = new List<string>(),
            DocumentId = documentId,
        };

        foreach (var signingTime in signingTimes)
        {
            signTimesViewModel.AvailableTimes.Add(signingTime.Time!);
        }
        
        return Ok(signTimesViewModel);
    }
    
    
    /// <summary>
    /// Api action for choosing signing times for intern mentee document
    /// </summary>
    /// <param name="request"></param>
    /// <returns>void</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChoseSigningTimeIntern([FromBody] SignTimeDTO request)
    {
        var document = await bll.InternMentorshipDocuments.FirstOrDefaultAsync(Guid.Parse(request.DocumentId));

        document!.DocumentStatus = "Signing option chosen";
        document.ChoosenSigningTime = request.ChosenTime;
        document.WayOfSigning = request.ChosenWay;

        bll.InternMentorshipDocuments.Update(document);
        await bll.SaveChangesAsync();

        var mentorship = (await bll.InternMentorships.GetAllAsync())
            .FirstOrDefault(m => m.Id.Equals(document.InternMentorshipId));
        
        var intern = (await bll.Interns.GetAllAsync())
            .FirstOrDefault(m => m.Id.Equals(mentorship!.InternId));

        var title = "";
        var emailBody = emailService.GenerateDocSignEmailBody(
            document.Title ?? title,
            document.ChoosenSigningTime!,
            $"{intern!.FirstName} {intern.LastName}");
        await emailService.SendEmailAsync("lasimer0406@gmail.com", "Mentee-Intern have chosen signing time", emailBody);
        
        return Ok();
    }
}