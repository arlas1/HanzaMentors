using System.Net;
using App.BLL.Contracts;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
using WebApp.Models;
namespace WebAppApi.ApiControllers;

/// <summary>
/// Api controller with action related to mentee and mentors home pages
/// </summary>
/// <param name="bll"></param>
[ApiVersion( "1.0" )]
[ApiController]
[Route("/api/v{version:apiVersion}/[controller]/[action]")]
public class MenteeApiController(IAppBLL bll, IConfiguration configuration) : ControllerBase
{
    /// <summary>
    /// Api action loading the employee mentees home page
    /// </summary>
    /// <returns>HomeViewModel</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType<HomeViewModel>((int)HttpStatusCode.OK)]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> EmployeeMentee([FromBody] string token)
    {
        if (!IdentityHelpers.ValidateJWT(
                token,
                configuration.GetValue<string>("JWT:key")!,
                configuration.GetValue<string>("JWT:issuer")!,
                configuration.GetValue<string>("JWT:audience")!
            ))
        {
            return BadRequest("JWT validation fail");
        }
        
        var documentSamples = (await bll.DocumentSamples.GetAllAsync())
            .ToDictionary(sample => sample.Id, sample => sample.Title);
        
        var menteesViewModel = new MenteeViewModel
        {
            InternMentees = (await bll.Interns.GetAllAsync()).ToList(),
            Mentors = (await bll.Mentors.GetAllAsync()).ToList(),
            MenteesMentor = new Dictionary<Guid, List<Guid>>(),
            EmployeeMentorships = (await bll.EmployeeMentorships.GetAllAsync()).ToList(),
            DocumentSamples = new Dictionary<Guid, string?>(),
            EmployeeMentees = (await bll.Employees.GetAllAsync())
                .Where(employee => 
                    !bll.Mentors.GetAll().Select(mentor => mentor.EmployeeId).Contains(employee.Id)
                ).ToList()
        };

        menteesViewModel.DocumentSamples = documentSamples;
        
        var employeesMentors = (await bll.EmployeesMentors.GetAllAsync()).ToList();
        var employees = (await bll.Employees.GetAllAsync()).ToList();
        
        foreach (var employeeMentorship in menteesViewModel.EmployeeMentorships)
        {
            var menteeId = employeeMentorship.EmployeeId;
            var mentee = employees.FirstOrDefault(e => e.Id == menteeId);
            
            if (mentee != null)
            {
                var menteeMentorIds = new List<Guid>(); 
                
                foreach (var empMentor in employeesMentors.Where(em => em.EmployeeMentorshipId == employeeMentorship.Id))
                {
                    menteeMentorIds.Add((Guid)empMentor.MentorId!);
                }
            
                var activeMentorId = employeesMentors
                    .Where(em => em.EmployeeMentorshipId == employeeMentorship.Id && em.IsCurrentlyActive)
                    .Select(em => em.MentorId)
                    .FirstOrDefault();

                if (activeMentorId != null && menteeMentorIds.Contains(activeMentorId.Value))
                {
                    menteeMentorIds.Remove(activeMentorId.Value);
                }
                menteeMentorIds.Add(activeMentorId ?? Guid.Empty);
            
                menteesViewModel.MenteesMentor.Add(mentee.Id, menteeMentorIds);
            }
        }
        
        return Ok(menteesViewModel);
    }
    
    
    /// <summary>
    /// Api action loading the intern mentees home page
    /// </summary>
    /// <returns>HomeViewModel</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType<HomeViewModel>((int)HttpStatusCode.OK)]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult InternMentee([FromBody] string token)
    {
        if (!IdentityHelpers.ValidateJWT(
                token,
                configuration.GetValue<string>("JWT:key")!,
                configuration.GetValue<string>("JWT:issuer")!,
                configuration.GetValue<string>("JWT:audience")!
            ))
        {
            return BadRequest("JWT validation fail");
        }
        
        var documentSamples = bll.DocumentSamples.GetAll()
            .ToDictionary(sample => sample.Id, sample => sample.Title);
        
        var menteesViewModel = new MenteeViewModel
        {
            InternMentees = bll.Interns.GetAll().ToList(),
            Mentors = bll.Mentors.GetAll().ToList(),
            MenteesMentor = new Dictionary<Guid, List<Guid>>(),
            InternMentorships = bll.InternMentorships.GetAll().ToList(),
            DocumentSamples = new Dictionary<Guid, string?>()
        };
        
        menteesViewModel.DocumentSamples = documentSamples;
        
        var internsMentors = bll.InternsMentors.GetAll().ToList();
        
        foreach (var internMentorship in menteesViewModel.InternMentorships)
        {
            var menteeId = internMentorship.InternId;
            var mentee = menteesViewModel.InternMentees.FirstOrDefault(e => e.Id == menteeId);
            
            if (mentee != null)
            {
                var menteeMentorIds = new List<Guid>(); 
                
                foreach (var intMentor in internsMentors.Where(em => em.InternMentorshipId == internMentorship.Id))
                {
                    menteeMentorIds.Add((Guid)intMentor.MentorId!);
                }
            
                var activeMentorId = internsMentors
                    .Where(em => em.InternMentorshipId == internMentorship.Id && em.IsCurrentlyActive)
                    .Select(em => em.MentorId)
                    .FirstOrDefault();

                if (activeMentorId != null && menteeMentorIds.Contains(activeMentorId.Value))
                {
                    menteeMentorIds.Remove(activeMentorId.Value);
                }
                menteeMentorIds.Add(activeMentorId ?? Guid.Empty);
            
                menteesViewModel.MenteesMentor.Add(mentee.Id, menteeMentorIds);
            }
        }
        
        return Ok(menteesViewModel);
    }
}