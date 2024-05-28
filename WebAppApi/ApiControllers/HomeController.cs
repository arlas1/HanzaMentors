using System.Net;
using App.BLL.Contracts;
using App.BLL.DTO;
using App.Public.DTO;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Spire.Additions.Xps.Schema;
using WebApp.DTO;
using WebApp.Models;
using AppUser = App.Domain.Identity.AppUser;

namespace WebAppApi.ApiControllers;

/// <summary>
/// Api controller related to loading home pages for admin, employee/intern mentees and mentors
/// </summary>
/// <param name="userManager"></param>
/// <param name="bll"></param>
[ApiVersion( "1.0" )]
[ApiController]
[Route("/api/v{version:apiVersion}/[controller]/[action]")]
public class HomeController(UserManager<AppUser> userManager, IAppBLL bll, IConfiguration configuration) : ControllerBase
{
    /// <summary>
    /// Api action loading home pages for admin, employee/intern mentees and mentors
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType<HomeViewModel>((int) HttpStatusCode.OK)]
    [ProducesResponseType<RestApiErrorResponse>((int) HttpStatusCode.BadRequest)]

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
                Type = "Admin"
            };
            
            return Ok(homeViewModel);
        }
        if (User.Identity!.IsAuthenticated && User.IsInRole("Mentee"))
        {
            var userId = Guid.Parse(userManager.GetUserId(User)!);
            var userEmployee = (await bll.Employees.GetAllAsync()).Where(employee => employee.AppUserId.Equals(userId));
            var userIntern = (await bll.Interns.GetAllAsync()).Where(employee => employee.AppUserId.Equals(userId));
            
            if (userEmployee.IsNullOrEmpty()) // user is intern
            {
                var homeViewModel = new HomeViewModel
                {
                    InternsMentors = new List<string>(),
                    InternDocuments = new List<InternMentorshipDocument>(),
                    Type = "Mentee-Intern"
                };
                
                var internship = (await bll.InternMentorships.GetAllAsync()) // one internship
                    .Where(mentorship => mentorship.InternId.Equals(userIntern.First().Id));
                var internMentors = (await bll.InternsMentors.GetAllAsync()) // n amount of mentors
                    .Where(mentor => mentor.InternMentorshipId.Equals(internship.First().Id));
                var allMentors = await bll.Mentors.GetAllAsync();

                homeViewModel.MenteeId = userIntern.First().Id;
                homeViewModel.MenteeType = "Intern";
                homeViewModel.MenteeFromDate = internship.First().FromDate;
                homeViewModel.MenteeUntilDate = internship.First().UntilDate;
                homeViewModel.MenteeTotalHours = internship.First().TotalHours;
                homeViewModel.IsOnSickLeave = internship.First().CurrentlyOnSickLeave;
                
                
                foreach (var internsMentor in internMentors)
                {
                    foreach (var mentor in allMentors)    
                    {
                        if (internsMentor.MentorId.Equals(mentor.Id))
                        {
                            homeViewModel.InternsMentors.Add($"{mentor.FirstName} {mentor.LastName} / From {internsMentor.FromDate} -  Until {internsMentor.UntilDate}");
                        }
                    }
                }

                var internsDocuments = (await bll.InternMentorshipDocuments.GetAllAsync())
                    .Where(doc => doc.InternMentorshipId.Equals(internship.First().Id)); // n amount of docs per internship

                foreach (var document in internsDocuments)
                {
                    homeViewModel.InternDocuments.Add(document);
                }

                return Ok(homeViewModel);
            }
            if (userIntern.IsNullOrEmpty()) // user is employee
            {
                var homeViewModel = new HomeViewModel
                {
                    EmployeesMentors = new List<string>(),
                    EmployeeDocuments = new List<EmployeeMentorshipDocument>(),
                    Type = "Mentee-Employee"
                };
                
                
                var mentorship = (await bll.EmployeeMentorships.GetAllAsync())
                    .Where(mentorship => mentorship.EmployeeId.Equals(userEmployee.First().Id));
                var employeeMentors = (await bll.EmployeesMentors.GetAllAsync())
                    .Where(mentor => mentor.EmployeeMentorshipId.Equals(mentorship.First().Id));
                var allMentors = await bll.Mentors.GetAllAsync();
                
                homeViewModel.MenteeId = userEmployee.First().Id;
                homeViewModel.MenteeType = "Employee";
                homeViewModel.MenteeFromDate = mentorship.First().FromDate;
                homeViewModel.MenteeUntilDate = mentorship.First().UntilDate;
                homeViewModel.MenteeTotalHours = mentorship.First().TotalHours;
                homeViewModel.IsOnSickLeave = mentorship.First().CurrentlyOnSickLeave;
                
                
                foreach (var employeesMentor in employeeMentors)
                {
                    foreach (var mentor in allMentors)    
                    {
                        if (employeesMentor.MentorId.Equals(mentor.Id))
                        {
                            homeViewModel.EmployeesMentors.Add($"{mentor.FirstName} {mentor.LastName} / From {employeesMentor.FromDate} -  Until {employeesMentor.UntilDate}");
                        }
                    }
                }

                var employeesDocuments = (await bll.EmployeeMentorshipDocuments.GetAllAsync())
                    .Where(doc => doc.EmployeeMentorshipId.Equals(mentorship.First().Id));

                foreach (var document in employeesDocuments)
                {
                    homeViewModel.EmployeeDocuments.Add(document);
                }

                return Ok(homeViewModel);
            }

            return Ok();
        }
        if (User.Identity!.IsAuthenticated && User.IsInRole("Mentor"))
        {
            var userId = Guid.Parse(userManager.GetUserId(User)!);
            var userEmployee = (await bll.Employees.GetAllAsync()).Where(employee => employee.AppUserId.Equals(userId));
            var userEmployeeId = userEmployee!.First().Id;
            var userMentor = (await bll.Mentors.GetAllAsync()).Where(mentor => mentor.EmployeeId.Equals(userEmployeeId));
            var homeViewModel = new HomeViewModel
            {
                MentorData = new List<MentorData>(),
                Type = "Mentor"
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
            
            return Ok(homeViewModel);
        }
        return Ok();
    }
}