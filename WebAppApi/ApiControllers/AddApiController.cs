using System.IO.Compression;
using System.Net;
using App.BLL.Contracts;
using App.BLL.DTO;
using App.Helpers.EmailService;
using Asp.Versioning;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spire.Pdf;
using Spire.Pdf.Texts;
using WebApp.DTO;
using WebApp.Models;
using WebAppApi.Models;
using AppUser = App.Domain.Identity.AppUser;

namespace WebAppApi.ApiControllers;

/// <summary>
/// Api controller with actions related to adding mentee, mentor, document sample and supervisor
/// </summary>
/// <param name="bll"></param>
/// <param name="emailService"></param>
/// <param name="userManager"></param>
[ApiVersion( "1.0" )]
[ApiController]
[Route("/api/v{version:apiVersion}/[controller]/[action]")]
public class AddApiController(IAppBLL bll, IEmailService emailService, UserManager<AppUser> userManager): ControllerBase
{
    /// <summary>
    /// Api action for Mentee page loading (Mentors, factory and intern supervisor selects)
    /// </summary>
    /// <returns>AddMenteeViewModel</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType<AddMenteeViewModel>((int)HttpStatusCode.OK)]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    public IActionResult GetMenteeInfo()
    {
        var menteeViewModel = new AddMenteeViewModel
        {
            Mentors = bll.Mentors.GetAll(),
            FactorySupervisors = bll.FactorySupervisors.GetAll(),
            InternSupervisors = bll.InternSupervisors.GetAll()
        };

        return Ok(menteeViewModel);
    }
    
    /// <summary>
    /// Api action for adding supervisor
    /// </summary>
    /// <param name="supervisorDto"></param>
    /// <returns>void</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddSupervisor([FromBody] SupervisorDTO supervisorDto)
    {
        switch (supervisorDto.Type)
        {
            case "Factory":
                var factorySupervisor = new FactorySupervisor()
                {
                    FullName = supervisorDto.FullName,
                };
                bll.FactorySupervisors.Add(factorySupervisor);
                await bll.SaveChangesAsync();
                break;
            
            case "Vocational school":
                var vcSupervisor = new InternSupervisor()
                {
                    FullName = supervisorDto.FullName
                };
                bll.InternSupervisors.Add(vcSupervisor);
                await bll.SaveChangesAsync();
                break;
        }

        return Ok();
    }
    
    /// <summary>
    /// Api action for adding mentor
    /// </summary>
    /// <param name="supervisorDto"></param>
    /// <returns>void</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddMentor([FromBody] MentorDTO supervisorDto)
    {
        if (ModelState.IsValid)
        {
            var user = new AppUser()
            {
                Id = Guid.NewGuid(),
                FirstName = supervisorDto.FirstName,
                LastName = supervisorDto.LastName,
                Email = supervisorDto.Email,
                UserName = supervisorDto.Email,
                PersonalCode = supervisorDto.PersonalCode,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            
            var userPassword = emailService.GenerateUserPassword();
            var result = await userManager.CreateAsync(user, userPassword);
            
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Mentor");
                
                // var emailBody = emailService.GenerateAccountEmailBody(supervisorDto.FirstName!, supervisorDto.Email!, userPassword);
                // await emailService.SendEmailAsync(supervisorDto.Email!, "HANZA Mentors account created", emailBody);
                     
                var employee = new Employee()
                {
                    Id = Guid.NewGuid(),
                    AppUserId = user.Id,
                    FirstName = supervisorDto.FirstName!,
                    LastName = supervisorDto.LastName!,
                    EmployeeType = "Full-time",
                    Profession = supervisorDto.Profession!,
                    Email = supervisorDto.Email
                };
                
                bll.Employees.Add(employee);
                await bll.SaveChangesAsync();
        
                var mentor = new Mentor()
                {
                    EmployeeId = employee.Id,
                    FirstName = supervisorDto.FirstName,
                    LastName = supervisorDto.LastName,
                    PaymentAmount = null,
                    PaymentOrderDate = null,
                    Profession = supervisorDto.Profession
                };
            
                bll.Mentors.Add(mentor);
                await bll.SaveChangesAsync();  
            }
        }

        return Ok();
    }
    
    /// <summary>
    /// Api action for adding mentee
    /// </summary>
    /// <param name="menteeDto"></param>
    /// <returns>void</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddMentee([FromBody] MenteeDTO menteeDto)
    {
        var user = new AppUser()
        {
            Id = Guid.NewGuid(),
            FirstName = menteeDto.FirstName,
            LastName = menteeDto.LastName,
            Email = menteeDto.Email,
            UserName = menteeDto.Email,
            PersonalCode = menteeDto.PersonalCode,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        
        var userPassword = emailService.GenerateUserPassword();
        var result = await userManager.CreateAsync(user, userPassword);
        
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Mentee");
        
            var emailBody = emailService.GenerateAccountEmailBody(menteeDto.FirstName!, menteeDto.Email!, userPassword);
            await emailService.SendEmailAsync(menteeDto.Email!, "HANZA Mentors account created", emailBody);
        
            switch (menteeDto.MenteeType)
            {
                case "Intern":
                    var intern = new Intern()
                    {
                        Id = Guid.NewGuid(),
                        AppUserId = user.Id,
                        FirstName = menteeDto.FirstName!,
                        LastName = menteeDto.LastName!,
                        InternType = menteeDto.InternType,
                        StudyProgram = menteeDto.Profession!,
                        Email = menteeDto.Email
                    };
                    
                    bll.Interns.Add(intern);
                    await bll.SaveChangesAsync();
        
                    var internMentorship = new InternMentorship()
                    {
                        Id = Guid.NewGuid(),
                        InternId = intern.Id,
                        InternSupervisorId = Guid.Parse(menteeDto.InternSupervisorId),
                        FactorySupervisorId = Guid.Parse(menteeDto.InternFactorySupervisorId),
                        FromDate = menteeDto.MenteeFromDate,
                        UntilDate = menteeDto.MenteeUntilDate,
                        TotalHours = menteeDto.MenteeTotalHours,
                        IsCurrentlyActive = true
                    };
                    
                    bll.InternMentorships.Add(internMentorship);
                    await bll.SaveChangesAsync();

                    var internsMentor = new InternsMentor
                    {
                        Id = Guid.NewGuid(),
                        MentorId = Guid.Parse(menteeDto.ChosenMentorId),
                        InternMentorshipId = internMentorship.Id,
                        FromDate = menteeDto.MentorFromDate,
                        UntilDate = menteeDto.MentorUntilDate,
                        TotalHours = menteeDto.MentorTotalHours,
                        IsCurrentlyActive = true,
                        ChangeReason = null
                    };
                    
                    bll.InternsMentors.Add(internsMentor);
                    await bll.SaveChangesAsync();
                    
                    break;
                
                case "Employee":
                    var employee = new Employee
                    {
                        Id = Guid.NewGuid(),
                        AppUserId = user.Id,
                        FirstName = menteeDto.FirstName!,
                        LastName = menteeDto.LastName!,
                        EmployeeType = menteeDto.EmployeeType,
                        Profession = menteeDto.Profession!,
                        Email = menteeDto.Email
                    };
                    
                    bll.Employees.Add(employee);
                    await bll.SaveChangesAsync();
                    
                    var employeeMentorship = new EmployeeMentorship()
                    {
                        Id = Guid.NewGuid(),
                        EmployeeId = employee.Id,
                        FactorySupervisorId = Guid.Parse(menteeDto.EmployeeFactorySupervisorId),
                        FromDate = menteeDto.MenteeFromDate,
                        UntilDate = menteeDto.MenteeUntilDate,
                        TotalHours = menteeDto.MenteeTotalHours,
                        IsCurrentlyActive = true
                    };
                    
                    bll.EmployeeMentorships.Add(employeeMentorship);
                    await bll.SaveChangesAsync();

                    var employeesMentor = new EmployeesMentor
                    {
                        Id = Guid.NewGuid(),
                        MentorId = Guid.Parse(menteeDto.ChosenMentorId),
                        EmployeeMentorshipId = employeeMentorship.Id,
                        FromDate = menteeDto.MentorFromDate,
                        UntilDate = menteeDto.MentorUntilDate,
                        TotalHours = menteeDto.MentorTotalHours,
                        IsCurrentlyActive = true,
                        ChangeReason = null
                    };

                    bll.EmployeesMentors.Add(employeesMentor);
                    await bll.SaveChangesAsync();
                    
                    break;
            }
        }

        return Ok();
    }
    
    /// <summary>
    /// Api action for adding document sample
    /// </summary>
    /// <param name="documentSampleDto"></param>
    /// <returns>void</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddDocumentSample([FromBody] DocumentSampleDTO documentSampleDto)
    {
        var documentSample = new DocumentSample
        {
            Id = Guid.NewGuid(),
            Title = documentSampleDto.Title,
            Base64Code = documentSampleDto.FileBase64Code
        };

        bll.DocumentSamples.Add(documentSample);
        await bll.SaveChangesAsync();

        return RedirectToAction("DocumentSamples", "Document");
    }
    
    /// <summary>
    /// Api action for adding a sick leave for intern and recalculating his total hours of mentorship
    /// </summary>
    /// <param name="request"></param>
    /// <returns>void</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> SickLeaveIntern([FromBody] SickLeaveDTO request)
    {
        var internship = (await bll.InternMentorships.GetAllAsync())
            .FirstOrDefault(ms => ms.InternId.Equals(Guid.Parse(request.MenteeId)));
        var sickLeave = new MenteeSickLeave
        {
            Id = Guid.NewGuid(),
            InternMentorshipId = internship!.Id,
            EmployeeMentorshipId = null,
            FromDate = request.SickLeaveFromDate,
            UntilDate = request.SickLeaveUntilDate,
            Reason = request.SickLeaveReason
        };

        var workingDays = 0;

        var fromDate = request.SickLeaveFromDate!.Value;
        var untilDate = request.SickLeaveUntilDate!.Value;
        var fromDateTime = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day);
        var untilDateTime = new DateTime(untilDate.Year, untilDate.Month, untilDate.Day);

        for (var date = fromDateTime; date <= untilDateTime; date = date.AddDays(1))
        {
            if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
            {
                workingDays++;
            }
        }
    

        var sickLeaveHours = workingDays * 8;
        var newTotalHours = internship.TotalHours - sickLeaveHours;
        
        internship.TotalHours = newTotalHours;
        // internship.CurrentlyOnSickLeave = true;
        
        bll.InternMentorships.Update(internship);
        await bll.SaveChangesAsync();
        bll.MenteeSickLeaves.Add(sickLeave);
        await bll.SaveChangesAsync();

        return Ok();
    }
    
    /// <summary>
    /// Api action for adding a sick leave for employee and recalculating his until date of mentorship
    /// </summary>
    /// <param name="request"></param>
    /// <returns>void</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> SickLeaveEmployee([FromBody] SickLeaveDTO request)
    {
        var mentorship = (await bll.EmployeeMentorships.GetAllAsync()).FirstOrDefault(ms => ms.EmployeeId.Equals(Guid.Parse(request.MenteeId)));
        var sickLeave = new MenteeSickLeave
        {
            Id = Guid.NewGuid(),
            InternMentorshipId = null,
            EmployeeMentorshipId = mentorship!.Id,
            FromDate = request.SickLeaveFromDate,
            UntilDate = request.SickLeaveUntilDate,
            Reason = request.SickLeaveReason
        };
        
        var fromDate = request.SickLeaveFromDate!.Value;
        var untilDate = request.SickLeaveUntilDate!.Value;
        var fromDateTime = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day);
        var untilDateTime = new DateTime(untilDate.Year, untilDate.Month, untilDate.Day);
        
        var sickLeaveDays = (untilDateTime - fromDateTime).Days + 1;
        mentorship.UntilDate = mentorship.UntilDate!.Value.AddDays(sickLeaveDays);
        
        bll.EmployeeMentorships.Update(mentorship);
        await bll.SaveChangesAsync();
        bll.MenteeSickLeaves.Add(sickLeave);
        await bll.SaveChangesAsync();
        
        return Ok();
    }
    
    /// <summary>
    /// Api action for documents generation page for intern mentees
    /// </summary>
    /// <param name="request"></param>
    /// <returns>MenteeViewModel</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType<MenteeViewModel>((int)HttpStatusCode.OK)]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    public IActionResult GetInternInfo([FromBody] GenerateDoucmentDTO request)
    {
        var documentSamples = bll.DocumentSamples.GetAll()
            .ToDictionary(sample => sample.Id, sample => sample.Title);
        
        var menteesViewModel = new MenteeViewModel
        {
            DocumentSamples = new Dictionary<Guid, string?>(),
            MenteeId = Guid.Parse(request.MenteeId)
        };
        menteesViewModel.DocumentSamples = documentSamples;

        var internMentorship1 = bll.InternMentorships.GetAll()
            .FirstOrDefault(internship => internship.InternId == Guid.Parse(request.MenteeId));

        if (internMentorship1 != null)
        {
            var internMentorIds = bll.InternsMentors.GetAll()
                .Where(mentor => mentor.InternMentorshipId == internMentorship1.Id)
                .Select(mentor => mentor.MentorId)
                .ToList();

            var mentors = bll.Mentors.GetAll()
                .Where(mentor => internMentorIds.Contains(mentor.Id))
                .ToList();
            
            menteesViewModel.Mentors = mentors;
        }
  
        return Ok(menteesViewModel);
    }
    
    /// <summary>
    /// Api action for generating document for intern mentee
    /// </summary>
    /// <param name="request"></param>
    /// <returns>void</returns>
    [HttpPost]
    [Produces("application/octet-stream")]
    [Consumes("application/json")]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GenerateDocumentIntern([FromBody] GenerateDoucmentDTO request)
    {
        var samples = (await bll.DocumentSamples.GetAllAsync()).Where(sample => request.SelectedSamples.Contains(sample.Id.ToString()));
        var mentor = await bll.Mentors.FirstOrDefaultAsync(Guid.Parse(request.SelectedMentorId));
        var mentee = await bll.Interns.FirstOrDefaultAsync(Guid.Parse(request.MenteeId));
        var menteesMentor = (await bll.InternsMentors.GetAllAsync()).FirstOrDefault(ms => ms.MentorId.Equals(mentor!.Id));
        var mentorship = (await bll.InternMentorships.GetAllAsync()).FirstOrDefault(ms => ms.InternId.Equals(mentee!.Id));

        if (mentor == null || mentee == null || menteesMentor == null || mentorship == null)
        {
            return NotFound();
        }

        var placeholders = new Dictionary<string, string>
        {
            { "%mentorFirstName%", mentor.FirstName! },
            { "%mentorLastName%", mentor.LastName! },
            { "%menteeFirstName%", mentee.FirstName! },
            { "%menteeLastName%", mentee.LastName! },
            { "%mentorFromDate%", menteesMentor.FromDate!.ToString()! },
            { "%mentorUntilDate%", menteesMentor.UntilDate!.ToString()! }
        };

        var tempFolderPath = Path.GetTempPath();
        var randomFileName = $"{Path.GetRandomFileName()}.zip";
        var zipFilePath = Path.Combine(tempFolderPath, randomFileName);

        using (var zipArchive = new ZipArchive(System.IO.File.Create(zipFilePath), ZipArchiveMode.Create))
        {
            foreach (var sampleDocument in samples)
            {
                var decodedDocument = Convert.FromBase64String(sampleDocument.Base64Code!);

                using (var memoryStream = new MemoryStream(decodedDocument))
                {
                    var pdfDocument = new PdfDocument();
                    pdfDocument.LoadFromStream(memoryStream);

                    for (var i = 0; i < pdfDocument.Pages.Count; i++)
                    {
                        var page = pdfDocument.Pages[i];
                        var textReplacer = new PdfTextReplacer(page);
                        var textReplaceOptions = new PdfTextReplaceOptions
                        {
                            ReplaceType = PdfTextReplaceOptions.ReplaceActionType.IgnoreCase |
                                           PdfTextReplaceOptions.ReplaceActionType.WholeWord |
                                           PdfTextReplaceOptions.ReplaceActionType.AutofitWidth
                        };
                        textReplacer.Options = textReplaceOptions;

                        foreach (var placeholder in placeholders)
                        {
                            textReplacer.ReplaceAllText(placeholder.Key, placeholder.Value);
                        }
                    }

                    var pdfFileName = $"{sampleDocument.Title}-{Guid.NewGuid()}.pdf";
                    var pdfPath = Path.Combine(tempFolderPath, pdfFileName);
                    pdfDocument.SaveToFile(pdfPath, FileFormat.PDF);
                    
                    var pdfBytes = await System.IO.File.ReadAllBytesAsync(pdfPath);
                    string base64String = Convert.ToBase64String(pdfBytes);
                    var internsDocument = new InternMentorshipDocument
                    {
                        Id = Guid.NewGuid(),
                        InternMentorshipId = mentorship.Id,
                        DocumentSampleId = sampleDocument.Id,
                        Title = pdfFileName,
                        Base64Code = base64String,
                        DocumentStatus = "Not signed",
                        ChoosenSigningTime = "Not chosen",
                        WayOfSigning = "Not chosen"
                    };
                    
                    bll.InternMentorshipDocuments.Add(internsDocument);
                    await bll.SaveChangesAsync();
                    
                    foreach (var time in request.SigningTimes)
                    {
                        var signingTime = new DoucmentSigningTime
                        {
                            Id = Guid.NewGuid(),
                            EmployeeMentorshipDocumentId = null,
                            InternMentorshipDocumentId = internsDocument.Id,
                            Time = time
                        };
                        bll.DocumentSigningTimes.Add(signingTime);
                    }
                    
                    await bll.SaveChangesAsync();
                    
                    zipArchive.CreateEntryFromFile(pdfPath, pdfFileName);
                }
            }
        }

        var emailBody = emailService.GenerateDocumentEmailBody($"{mentee.FirstName} {mentee.LastName}");
        await emailService.SendEmailAsync(mentee.Email!, "New documents assigned", emailBody);
        
        byte[] zipFileBytes = System.IO.File.ReadAllBytes(zipFilePath);
        return File(zipFileBytes, "application/zip", randomFileName);
    }
    
    /// <summary>
    /// Api action for documents generation page for employee mentees
    /// </summary>
    /// <param name="request"></param>
    /// <returns>MenteeViewModel</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType<MenteeViewModel>((int)HttpStatusCode.OK)]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    public IActionResult GetEmployeeInfo([FromBody] GenerateDoucmentDTO request)
    {
        var documentSamples = bll.DocumentSamples.GetAll()
            .ToDictionary(sample => sample.Id, sample => sample.Title);
        
        var menteesViewModel = new MenteeViewModel
        {
            DocumentSamples = new Dictionary<Guid, string?>(),
            MenteeId = Guid.Parse(request.MenteeId)

        };
        menteesViewModel.DocumentSamples = documentSamples;

        var employeeMentorship = bll.EmployeeMentorships.GetAll()
            .FirstOrDefault(internship => internship.EmployeeId == Guid.Parse(request.MenteeId));

        if (employeeMentorship != null)
        {
            var employeeMentorIds = bll.EmployeesMentors.GetAll()
                .Where(mentor => mentor.EmployeeMentorshipId == employeeMentorship.Id)
                .Select(mentor => mentor.MentorId)
                .ToList();

            var mentors = bll.Mentors.GetAll()
                .Where(mentor => employeeMentorIds.Contains(mentor.Id))
                .ToList();
            
            menteesViewModel.Mentors = mentors;
        }
  
        return Ok(menteesViewModel);
    }
    
    /// <summary>
    /// Api action for generating document for employee mentee
    /// </summary>
    /// <param name="request"></param>
    /// <returns>void</returns>
    [HttpPost]
    [Produces("application/octet-stream")]
    [Consumes("application/json")]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GenerateDocumentEmployee([FromBody] GenerateDoucmentDTO request)
    {
        var samples = (await bll.DocumentSamples.GetAllAsync()).Where(sample => request.SelectedSamples.Contains(sample.Id.ToString()));
        var mentor = await bll.Mentors.FirstOrDefaultAsync(Guid.Parse(request.SelectedMentorId));
        var mentee = await bll.Employees.FirstOrDefaultAsync(Guid.Parse(request.MenteeId));
        var menteesMentor = (await bll.EmployeesMentors.GetAllAsync()).FirstOrDefault(ms => ms.MentorId.Equals(mentor!.Id));
        var mentorship = (await bll.EmployeeMentorships.GetAllAsync()).FirstOrDefault(ms => ms.EmployeeId.Equals(mentee!.Id));

        if (mentor == null || mentee == null || menteesMentor == null || mentorship == null)
        {
            return NotFound();
        }

        var placeholders = new Dictionary<string, string>
        {
            { "%mentorFirstName%", mentor.FirstName! },
            { "%mentorLastName%", mentor.LastName! },
            { "%menteeFirstName%", mentee.FirstName! },
            { "%menteeLastName%", mentee.LastName! },
            { "%mentorFromDate%", menteesMentor.FromDate!.ToString()! },
            { "%mentorUntilDate%", menteesMentor.UntilDate!.ToString()! }
        };

        var tempFolderPath = Path.GetTempPath();
        var randomFileName = $"{Path.GetRandomFileName()}.zip";
        var zipFilePath = Path.Combine(tempFolderPath, randomFileName);

        using (var zipArchive = new ZipArchive(System.IO.File.Create(zipFilePath), ZipArchiveMode.Create))
        {
            foreach (var sampleDocument in samples)
            {
                var decodedDocument = Convert.FromBase64String(sampleDocument.Base64Code!);

                using (var memoryStream = new MemoryStream(decodedDocument))
                {
                    var pdfDocument = new PdfDocument();
                    pdfDocument.LoadFromStream(memoryStream);

                    for (var i = 0; i < pdfDocument.Pages.Count; i++)
                    {
                        var page = pdfDocument.Pages[i];
                        var textReplacer = new PdfTextReplacer(page);
                        var textReplaceOptions = new PdfTextReplaceOptions
                        {
                            ReplaceType = PdfTextReplaceOptions.ReplaceActionType.IgnoreCase |
                                           PdfTextReplaceOptions.ReplaceActionType.WholeWord |
                                           PdfTextReplaceOptions.ReplaceActionType.AutofitWidth
                        };
                        textReplacer.Options = textReplaceOptions;

                        foreach (var placeholder in placeholders)
                        {
                            textReplacer.ReplaceAllText(placeholder.Key, placeholder.Value);
                        }
                    }

                    var pdfFileName = $"{sampleDocument.Title}-{Guid.NewGuid()}.pdf";
                    var pdfPath = Path.Combine(tempFolderPath, pdfFileName);
                    pdfDocument.SaveToFile(pdfPath, FileFormat.PDF);

                    var pdfBytes = await System.IO.File.ReadAllBytesAsync(pdfPath);
                    string base64String = Convert.ToBase64String(pdfBytes);
                    var employeesDocument = new EmployeeMentorshipDocument
                    {
                        Id = Guid.NewGuid(),
                        EmployeeMentorshipId = mentorship.Id,
                        DocumentSampleId = sampleDocument.Id,
                        Title = pdfFileName,
                        Base64Code = base64String,
                        DocumentStatus = "Not signed",
                        ChoosenSigningTime = "Not chosen",
                        WayOfSigning = "Not chosen",

                    };
                    
                    bll.EmployeeMentorshipDocuments.Add(employeesDocument);
                    await bll.SaveChangesAsync();
                    
                    foreach (var time in request.SigningTimes)
                    {
                        var signingTime = new DoucmentSigningTime
                        {
                            Id = Guid.NewGuid(),
                            EmployeeMentorshipDocumentId = employeesDocument.Id,
                            InternMentorshipDocumentId = null,
                            Time = time
                        };
                        bll.DocumentSigningTimes.Add(signingTime);
                    }
                    
                    await bll.SaveChangesAsync();
                    
                    zipArchive.CreateEntryFromFile(pdfPath, pdfFileName);
                }
            }
        }

        var emailBody = emailService.GenerateDocumentEmailBody($"{mentee.FirstName} {mentee.LastName}");
        await emailService.SendEmailAsync(mentee.Email!, "New documents assigned", emailBody);
        
        byte[] zipFileBytes = System.IO.File.ReadAllBytes(zipFilePath);
        return File(zipFileBytes, "application/zip", randomFileName);
    }
}