using System.Diagnostics;
using App.BLL.Contracts;
using App.Domain.Identity;
using App.Public.DTO;
using Asp.Versioning;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApp.Models;
using Spire.Pdf;

namespace WebApp.Controllers;

public class HomeController(IAppBLL bll, UserManager<AppUser> userManager) : Controller
{
    // GET: Intern
    public async Task<IActionResult> Index()
    {
        if (User.Identity!.IsAuthenticated && User.IsInRole("Admin"))
        {
            var mentors = await bll.Mentors.GetAllAsync();
            var internMentorships = await bll.InternMentorships.GetAllAsync();
            var employeeMentorships = await bll.EmployeeMentorships.GetAllAsync();
            var factorySupervisors = await bll.FactorySupervisors.GetAllAsync();

            var internMentorshipsList = internMentorships.ToList();
            var employeeMentorshipsList = employeeMentorships.ToList();
            
            var homeViewModel = new HomeViewModel
            {
                TotalMentorsAmount = mentors.Count(),
                ActiveMentorshipsAmount = internMentorshipsList.Count(mentorship => mentorship.IsCurrentlyActive) + employeeMentorshipsList.Count(mentorship => mentorship.IsCurrentlyActive),
                // MenteesOnSickLeaveAmount = internMentorshipsList.Count(mentorship => mentorship.CurrentlyOnSickLeave) + employeeMentorshipsList.Count(mentorship => mentorship.CurrentlyOnSickLeave),
                FactorySupervisorsAmount = factorySupervisors.Count(),
            };
            
            return View(homeViewModel);
        }
        if (User.Identity!.IsAuthenticated && User.IsInRole("Mentee"))
        {
            return View();
        }
        if (User.Identity!.IsAuthenticated && User.IsInRole("Mentor"))
        {
            var userId = Guid.Parse(userManager.GetUserId(User)!);
            var userEmployee = (await bll.Employees.GetAllAsync()).Where(employee => employee.AppUserId.Equals(userId));
            var userEmployeeId = userEmployee!.First().Id;
            var userMentor = (await bll.Mentors.GetAllAsync()).Where(mentor => mentor.EmployeeId.Equals(userEmployeeId));
            var homeViewModel = new HomeViewModel
            {
                MentorData = new List<MentorData>()
            };

            var employeesMentors = (await bll.EmployeesMentors.GetAllAsync()).Where(mentorship => mentorship.MentorId.Equals(userMentor!.First().Id));
            var employeeMentorships = (await bll.EmployeeMentorships.GetAllAsync()).ToList();
            var employees = (await bll.Employees.GetAllAsync()).ToList();

            
            foreach (var mentor in employeesMentors)
            {
                foreach (var employeeMentorship in employeeMentorships)
                {
                    if (mentor.EmployeeMentorshipId.Equals(employeeMentorship.Id))
                    {
                        var mentorsData = new MentorData
                        {
                            MentorFromDate = mentor.FromDate,
                            MentorUntilDate = mentor.UntilDate,
                            MentorTotalHours = mentor.TotalHours,
                            IsOnSickLeave = employeeMentorship.CurrentlyOnSickLeave,
                        };
                        foreach (var employee in employees)
                        {
                            if (employeeMentorship.EmployeeId.Equals(employee.Id))
                            {
                                mentorsData.MenteeFullName = $"{employee.FirstName} {employee.LastName}";
                                mentorsData.MenteeType = $"Employee / {employee.EmployeeType}";
                            }
                        }
                        homeViewModel.MentorData!.Add(mentorsData);
                    }
                }
            }

            var internsMentors = (await bll.InternsMentors.GetAllAsync()).Where(mentorship => mentorship.MentorId.Equals(userMentor!.First().Id));
            var internMentorships = (await bll.InternMentorships.GetAllAsync()).ToList();
            var interns = (await bll.Interns.GetAllAsync()).ToList();
            
            foreach (var mentor in internsMentors)
            {
                foreach (var internMentorship in internMentorships)
                {
                    if (mentor.InternMentorshipId.Equals(internMentorship.Id))
                    {
                        var mentorsData = new MentorData
                        {
                            MentorFromDate = mentor.FromDate,
                            MentorUntilDate = mentor.UntilDate,
                            MentorTotalHours = mentor.TotalHours,
                            IsOnSickLeave = internMentorship.CurrentlyOnSickLeave
                        };
                        foreach (var intern in interns)
                        {
                            if (internMentorship.InternId.Equals(intern.Id))
                            {
                                mentorsData.MenteeFullName = $"{intern.FirstName} {intern.LastName}";
                                mentorsData.MenteeType = $"Intern / {intern.InternType}";
                            }
                        }
                        homeViewModel.MentorData!.Add(mentorsData);
                    }
                }
            }
            
            return View(homeViewModel);
        }

        return View();
    }
    
}