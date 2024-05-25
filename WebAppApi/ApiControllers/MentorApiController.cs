using System.Net;
using App.BLL.Contracts;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
using WebApp.Models;
using WebAppApi.Models;

namespace WebAppApi.ApiControllers;

[ApiVersion( "1.0" )]
[ApiController]
[Route("/api/v{version:apiVersion}/[controller]/[action]")]
public class MentorApiController(IAppBLL bll) : ControllerBase
{
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType<MentorsViewModel>((int)HttpStatusCode.OK)]
    [ProducesResponseType<RestApiErrorResponse>((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Index()
    {
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
            
            // Handling employee mentorships
            foreach (var mentorship in employesMentors)
            {
                if (mentorship.MentorId == mentor.Id && mentorship.IsCurrentlyActive)
                {
                    var mentorshipId = mentorship.EmployeeMentorshipId;
                    var mentee = employees.FirstOrDefault(e => e.Id == employeeMentorships.FirstOrDefault(em => em.Id == mentorshipId)?.EmployeeId);
                    if (mentee != null)
                    {
                        var menteeFullname = $"{mentee.FirstName} {mentee.LastName} - Employee ({mentee.EmployeeType})";
                        menteeData.Add(menteeFullname);
                    }
                }
            }

            // Handling intern mentorships
            foreach (var mentorship in internsMentors)
            {
                if (mentorship.MentorId == mentor.Id && mentorship.IsCurrentlyActive)
                {
                    var mentorshipId = mentorship.InternMentorshipId;
                    var mentee = interns.FirstOrDefault(i => i.Id == internMentorships.FirstOrDefault(im => im.Id == mentorshipId)?.InternId);
                    if (mentee != null)
                    {
                        var menteeFullname = $"{mentee.FirstName} {mentee.LastName} - Intern({mentee.InternType})";
                        menteeData.Add(menteeFullname);
                    }
                }
            }

            // Check if the mentor.Id already has an entry in the dictionary
            if (!mentorsViewModel.MentorMentees.TryAdd(mentor.Id, menteeData))
            {
                mentorsViewModel.MentorMentees[mentor.Id].AddRange(menteeData);
            }
        }

        return Ok(mentorsViewModel);
    }

    
    
}