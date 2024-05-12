using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using WebApp.Models;

namespace WebApp.Controllers;

public class MentorController(IAppBLL bll, UserManager<AppUser> userManager) : Controller
{
    // GET: Mentor
    public IActionResult Index(MentorsViewModel viewModel)
    {
        // var userId = Guid.Parse(userManager.GetUserId(User)!);
        var mentorsViewModel = new MentorsViewModel()
        {
            MentorMentees = new Dictionary<List<Guid>, string>()
        };

        switch (viewModel.FilterType)
        {
            case "Name":
                mentorsViewModel.Mentors = bll.Mentors.GetAll().Where(mentor =>
                    mentor.FirstName!.Contains(viewModel.FilterRequest, StringComparison.OrdinalIgnoreCase) ||
                    mentor.LastName!.Contains(viewModel.FilterRequest, StringComparison.OrdinalIgnoreCase)
                ).ToList();
                break;
        
            case "Profession":
                mentorsViewModel.Mentors = bll.Mentors.GetAll().Where(mentor =>
                    mentor.Profession!.Contains(viewModel.FilterRequest, StringComparison.OrdinalIgnoreCase)
                ).ToList();
                break;
            default:
                mentorsViewModel.Mentors = bll.Mentors.GetAll().ToList();
                break;
        }
        
        var employesMentors = bll.EmployeesMentors.GetAll().ToList();
        var employeeMentorships = bll.EmployeeMentorships.GetAll().ToList();
        var employees = bll.Employees.GetAll().ToList();

        foreach (var mentor in mentorsViewModel.Mentors)
        {
            var mentorMenteeId = new List<Guid>();
            var menteeFullname = "";

            foreach (var mentorship in employesMentors)
            {
                if (mentorship.MentorId == mentor.Id && mentorship.IsCurrentlyActive)
                {
                    var mentorshipId = mentorship.EmployeeMentorshipId;

                    foreach (var employeeMentorship in employeeMentorships)
                    {
                        if (employeeMentorship.Id == mentorshipId)
                        {
                            var menteeId = employeeMentorship.EmployeeId;
                            var mentee = employees.FirstOrDefault(e => e.Id == menteeId);
                            if (mentee != null)
                            {
                                mentorMenteeId.Add(mentor.Id);
                                mentorMenteeId.Add(mentee.Id);
                                menteeFullname = $"{mentee.FirstName} {mentee.LastName}, Employee({mentee.EmployeeType})";
                            }
                        }
                    }
                }
            }

            mentorsViewModel.MentorMentees.Add(mentorMenteeId, menteeFullname);
        }
        
        var internsMentors = bll.InternsMentors.GetAll().ToList();
        var internMentorships = bll.InternMentorships.GetAll().ToList();
        var interns = bll.Interns.GetAll().ToList();
        
        foreach (var mentor in mentorsViewModel.Mentors)
        {
            var mentorMenteeId = new List<Guid>();
            var menteeFullname = "";
        
            foreach (var mentorship in internsMentors)
            {
                if (mentorship.MentorId == mentor.Id && mentorship.IsCurrentlyActive)
                {
                    var mentorshipId = mentorship.InternMentorshipId;
        
                    foreach (var internMentorship in internMentorships)
                    {
                        if (internMentorship.Id == mentorshipId)
                        {
                            var menteeId = internMentorship.InternId;
                            var mentee = interns.FirstOrDefault(e => e.Id == menteeId);
                            if (mentee != null)
                            {
                                mentorMenteeId.Add(mentor.Id);
                                mentorMenteeId.Add(mentee.Id);
                                menteeFullname = $"{mentee.FirstName} {mentee.LastName}, Intern({mentee.InternType})";
                            }
                        }
                    }
                }
            }
        
            mentorsViewModel.MentorMentees.Add(mentorMenteeId, menteeFullname);
        }

        return View(mentorsViewModel);
    }
    
}