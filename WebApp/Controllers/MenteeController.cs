using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using WebApp.Models;

namespace WebApp.Controllers;

public class MenteeController(IAppBLL bll, UserManager<AppUser> userManager) : Controller
{
    public IActionResult Mentees(MenteeViewModel viewModel)
    {
        var menteesViewModel = new MenteeViewModel
        {
            InternMentees = bll.Interns.GetAll().ToList(),
            Mentors = bll.Mentors.GetAll().ToList(),
            MenteesMentor = new Dictionary<Guid, List<Guid>>(),
            EmployeeMentorships = bll.EmployeeMentorships.GetAll().ToList(),
            InternMentorships = bll.InternMentorships.GetAll().ToList()
        };
        
        switch (viewModel.FilterType)
        {
            case "Mentee's name":
                menteesViewModel.EmployeeMentees = bll.Employees.GetAll().Where(employee =>
                    (employee.FirstName!.Contains(viewModel.FilterRequest, StringComparison.OrdinalIgnoreCase) ||
                     employee.LastName!.Contains(viewModel.FilterRequest, StringComparison.OrdinalIgnoreCase)) &&
                    !bll.Mentors.GetAll().Select(mentor => mentor.EmployeeId).Contains(employee.Id)
                ).ToList();
    
                menteesViewModel.InternMentees = bll.Interns.GetAll().Where(intern =>
                    (intern.FirstName!.Contains(viewModel.FilterRequest, StringComparison.OrdinalIgnoreCase) ||
                     intern.LastName!.Contains(viewModel.FilterRequest, StringComparison.OrdinalIgnoreCase))
                ).ToList();
                break;

            case "Profession":
                menteesViewModel.EmployeeMentees = bll.Employees.GetAll().Where(employee =>
                    employee.Profession!.Contains(viewModel.FilterRequest, StringComparison.OrdinalIgnoreCase) &&
                    !bll.Mentors.GetAll().Select(mentor => mentor.EmployeeId).Contains(employee.Id)
                ).ToList();
    
                menteesViewModel.InternMentees = bll.Interns.GetAll().Where(intern =>
                    intern.StudyProgram!.Contains(viewModel.FilterRequest, StringComparison.OrdinalIgnoreCase)
                ).ToList();
                break;

            case "Mentee's type":
                menteesViewModel.EmployeeMentees = bll.Employees.GetAll().Where(employee =>
                    employee.EmployeeType!.Contains(viewModel.FilterRequest, StringComparison.OrdinalIgnoreCase) &&
                    !bll.Mentors.GetAll().Select(mentor => mentor.EmployeeId).Contains(employee.Id)
                ).ToList();
    
                menteesViewModel.InternMentees = bll.Interns.GetAll().Where(intern =>
                    intern.InternType!.Contains(viewModel.FilterRequest, StringComparison.OrdinalIgnoreCase)
                ).ToList();
                break;
            default:
                menteesViewModel.EmployeeMentees = bll.Employees.GetAll()
                    .Where(employee => 
                        !bll.Mentors.GetAll().Select(mentor => mentor.EmployeeId).Contains(employee.Id)
                    ).ToList();
                menteesViewModel.InternMentees = bll.Interns.GetAll().ToList();
                break;
        }
        
        var employeesMentors = bll.EmployeesMentors.GetAll().ToList();
        var employees = bll.Employees.GetAll().ToList();
        
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
        
        
        var internsMentors = bll.InternsMentors.GetAll().ToList();
        var interns = bll.Interns.GetAll().ToList();
        
        foreach (var internMentorship in menteesViewModel.InternMentorships)
        {
            var menteeId = internMentorship.InternId;
            var mentee = interns.FirstOrDefault(e => e.Id == menteeId);
            
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
        
        return View(menteesViewModel);
    }
    
    public IActionResult EmployeeMentee(MenteeViewModel viewModel)
    {
        var documentSamples = bll.DocumentSamples.GetAll()
            .ToDictionary(sample => sample.Id, sample => sample.Title);
        
        var menteesViewModel = new MenteeViewModel
        {
            InternMentees = bll.Interns.GetAll().ToList(),
            Mentors = bll.Mentors.GetAll().ToList(),
            MenteesMentor = new Dictionary<Guid, List<Guid>>(),
            EmployeeMentorships = bll.EmployeeMentorships.GetAll().ToList(),
            DocumentSamples = new Dictionary<Guid, string?>()
        };

        menteesViewModel.DocumentSamples = documentSamples;
        
        switch (viewModel.FilterType)
        {
            case "Mentee's name":
                menteesViewModel.EmployeeMentees = bll.Employees.GetAll().Where(employee =>
                    (employee.FirstName!.Contains(viewModel.FilterRequest, StringComparison.OrdinalIgnoreCase) ||
                     employee.LastName!.Contains(viewModel.FilterRequest, StringComparison.OrdinalIgnoreCase)) &&
                    !bll.Mentors.GetAll().Select(mentor => mentor.EmployeeId).Contains(employee.Id)
                ).ToList();
                break;

            case "Profession":
                menteesViewModel.EmployeeMentees = bll.Employees.GetAll().Where(employee =>
                    employee.Profession!.Contains(viewModel.FilterRequest, StringComparison.OrdinalIgnoreCase) &&
                    !bll.Mentors.GetAll().Select(mentor => mentor.EmployeeId).Contains(employee.Id)
                ).ToList();
                break;

            case "Mentee's type":
                menteesViewModel.EmployeeMentees = bll.Employees.GetAll().Where(employee =>
                    employee.EmployeeType!.Contains(viewModel.FilterRequest, StringComparison.OrdinalIgnoreCase) &&
                    !bll.Mentors.GetAll().Select(mentor => mentor.EmployeeId).Contains(employee.Id)
                ).ToList();
                break;
            default:
                menteesViewModel.EmployeeMentees = bll.Employees.GetAll()
                    .Where(employee => 
                        !bll.Mentors.GetAll().Select(mentor => mentor.EmployeeId).Contains(employee.Id)
                    ).ToList();
                break;
        }
        
        var employeesMentors = bll.EmployeesMentors.GetAll().ToList();
        var employees = bll.Employees.GetAll().ToList();
        
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
        
        return View(menteesViewModel);
    }
    
    public IActionResult InternMentee(MenteeViewModel viewModel)
    {
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

        
        switch (viewModel.FilterType)
        {
            case "Mentee's name":
                menteesViewModel.InternMentees = bll.Interns.GetAll().Where(intern =>
                    (intern.FirstName!.Contains(viewModel.FilterRequest, StringComparison.OrdinalIgnoreCase) ||
                     intern.LastName!.Contains(viewModel.FilterRequest, StringComparison.OrdinalIgnoreCase))
                ).ToList();
                break;

            case "Profession":
                menteesViewModel.InternMentees = bll.Interns.GetAll().Where(intern =>
                    intern.StudyProgram!.Contains(viewModel.FilterRequest, StringComparison.OrdinalIgnoreCase)
                ).ToList();
                break;

            case "Mentee's type":
                menteesViewModel.InternMentees = bll.Interns.GetAll().Where(intern =>
                    intern.InternType!.Contains(viewModel.FilterRequest, StringComparison.OrdinalIgnoreCase)
                ).ToList();
                break;
            default:
                menteesViewModel.InternMentees = bll.Interns.GetAll().ToList();
                break;
        }
        
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
        
        return View(menteesViewModel);
    }
    
    
}