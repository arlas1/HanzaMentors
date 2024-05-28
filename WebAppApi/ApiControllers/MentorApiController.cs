using System.Net;
using App.BLL.Contracts;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
using WebApp.Models;
using WebAppApi.Models;

namespace WebAppApi.ApiControllers;

/// <summary>
/// Api controller with action related to mentee and mentors home pages
/// </summary>
/// <param name="bll"></param>
[ApiVersion( "1.0" )]
[ApiController]
[Route("/api/v{version:apiVersion}/[controller]/[action]")]
public class MentorApiController(IAppBLL bll, IConfiguration configuration) : ControllerBase
{
    /// <summary>
    /// Api action loading the mentors home page
    /// </summary>
    /// <returns>MentorsViewModel</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType<MentorsViewModel>((int)HttpStatusCode.OK)]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Index([FromBody] string token)
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
        
        var mentorsViewModel = new Mentors1ViewModel()
        {
            MentorMentees = new Dictionary<Guid, List<string>>(),
            Mentors = (await bll.Mentors.GetAllAsync()).ToList()
        };

        var employesMentors = (await bll.EmployeesMentors.GetAllAsync()).ToList();
        var employeeMentorships = (await bll.EmployeeMentorships.GetAllAsync()).ToList();
        var employees = (await bll.Employees.GetAllAsync()).ToList();

        var internsMentors = (await bll.InternsMentors.GetAllAsync()).ToList();
        var internMentorships = (await bll.InternMentorships.GetAllAsync()).ToList();
        var interns = (await bll.Interns.GetAllAsync()).ToList();

        foreach (var mentor in mentorsViewModel.Mentors)
        {
            var menteeData = new List<string>();

            foreach (var mentorship in employesMentors)
            {
                if (mentorship.MentorId == mentor.Id && mentorship.IsCurrentlyActive)
                {
                    var mentorshipId = mentorship.EmployeeMentorshipId;
                    var mentee = employees.FirstOrDefault(e =>
                        e.Id == employeeMentorships.FirstOrDefault(em => em.Id == mentorshipId)?.EmployeeId);
                    if (mentee != null)
                    {
                        var menteeFullname = $"{mentee.FirstName} {mentee.LastName} - Employee ({mentee.EmployeeType})";
                        menteeData.Add(menteeFullname);
                    }
                }
            }

            foreach (var mentorship in internsMentors)
            {
                if (mentorship.MentorId == mentor.Id && mentorship.IsCurrentlyActive)
                {
                    var mentorshipId = mentorship.InternMentorshipId;
                    var mentee = interns.FirstOrDefault(i =>
                        i.Id == internMentorships.FirstOrDefault(im => im.Id == mentorshipId)?.InternId);
                    if (mentee != null)
                    {
                        var menteeFullname = $"{mentee.FirstName} {mentee.LastName} - Intern({mentee.InternType})";
                        menteeData.Add(menteeFullname);
                    }
                }
            }

            if (!mentorsViewModel.MentorMentees.TryAdd(mentor.Id, menteeData))
            {
                mentorsViewModel.MentorMentees[mentor.Id].AddRange(menteeData);
            }
        }

        return Ok(mentorsViewModel);
    }
}