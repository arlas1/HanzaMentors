using System.Net;
using App.BLL.Contracts;
using App.BLL.DTO;
using App.Helpers.EmailService;
using Asp.Versioning;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
using WebApp.Models;
using WebAppApi.Models;
using AppUser = App.Domain.Identity.AppUser;

namespace WebAppApi.ApiControllers;

[ApiVersion( "1.0" )]
[ApiController]
[Route("/api/v{version:apiVersion}/[controller]/[action]")]
public class AddApiController(IAppBLL bll, IEmailService emailService, UserManager<AppUser> userManager): ControllerBase
{
    
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
                
                // var emailBody = emailService.GenerateAccountEmailBody(mentorViewModel.FirstName!, mentorViewModel.Email!, userPassword);
                // await emailService.SendEmailAsync(mentorViewModel.Email!, "HANZA Mentors account created", emailBody);
                    
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
        
            // var emailBody = emailService.GenerateAccountEmailBody(menteeViewModel.FirstName!, menteeViewModel.Email!, userPassword);
            // await emailService.SendEmailAsync(menteeViewModel.Email!, "HANZA Mentors account created", emailBody);
        
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

}