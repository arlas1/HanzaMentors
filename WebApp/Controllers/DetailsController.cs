using App.BLL.Contracts;
using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class DetailsController(IAppBLL bll) : Controller
{
    [HttpGet]
    public IActionResult Mentor(Guid mentorId)
    {
        var mentor = bll.Mentors.GetAll().ToList().FirstOrDefault(me => me.Id.Equals(mentorId));
        var documentSamples = bll.DocumentSamples.GetAll()
            .ToDictionary(sample => sample.Id, sample => sample.Title);
        
        var details = new DetailsViewModel
        {
            InitialMentorId = mentor!.Id,
            MentorFirstName = mentor!.FirstName,
            MentorLastName = mentor.LastName,
            DocumentSamples = new Dictionary<Guid, string?>()
        };

        details.DocumentSamples = documentSamples;
        
        return View(details);
    }
    
    [HttpPost]
    public IActionResult Mentor(DetailsViewModel mentorDetails)
    {
        var mentor = bll.Mentors.GetAll().ToList().FirstOrDefault(me => me.Id.Equals(mentorDetails.InitialMentorId));
        mentor!.FirstName = mentorDetails.MentorFirstName;
        mentor.LastName = mentorDetails.MentorLastName;
        
        var documentSamples = bll.DocumentSamples.GetAll()
            .ToDictionary(sample => sample.Id, sample => sample.Title);
        
        bll.Mentors.Update(mentor);
        bll.SaveChangesAsync();
        
        var details = new DetailsViewModel
        {
            InitialMentorId = mentor.Id,
            MentorFirstName = mentor!.FirstName,
            MentorLastName = mentor.LastName,
            DocumentSamples = new Dictionary<Guid, string?>()
        };
        details.DocumentSamples = documentSamples;
        
        return View(details);
    }

    [HttpGet]
    public IActionResult EmployeeMentee(Guid employeeId)
    {
        var employee = bll.Employees.GetAll().ToList().FirstOrDefault(me => me.Id.Equals(employeeId));
        var employeeMentorship = bll.EmployeeMentorships.GetAll().ToList().FirstOrDefault(ms => ms.EmployeeId.Equals(employeeId));
        var employeesMentor = bll.EmployeesMentors
            .GetAll()
            .ToList()
            .FirstOrDefault(employeesMentor => 
                employeesMentor.EmployeeMentorshipId == employeeMentorship!.Id && employeesMentor.IsCurrentlyActive);   
        var mentor = bll.Mentors.GetAll().ToList().FirstOrDefault(me => me.Id.Equals(employeesMentor!.MentorId)); 
        
        var details = new DetailsViewModel
        {
            Mentors = bll.Mentors.GetAll().ToList(),
            InitialMentorId = mentor!.Id,
            NewMentorId = mentor.Id,
            MentorFirstName = mentor!.FirstName,
            MentorLastName = mentor.LastName,
            EmployeeId = employee!.Id,
            EmployeeFirstName = employee.FirstName,
            EmployeeLastName = employee.LastName,
            EmployeeProfession = employee.Profession,
            EmployeeFromDate = employeeMentorship!.FromDate,
            EmployeeUntilDate = employeeMentorship.UntilDate,
            EmployeeTotalHours = employeeMentorship.TotalHours,
            EmployeeMentorFromDate = employeesMentor!.FromDate,
            EmployeeMentorUntilDate = employeesMentor.UntilDate,
            EmployeeMentorTotalHours = employeesMentor.TotalHours,

        };
        
        return View(details);
    }

    [HttpPost]
    public async Task<IActionResult> EmployeeMentee(DetailsViewModel menteeDetails, Guid employeeId, Guid initialMentorId)
    {
        var allMentors = (await bll.Mentors.GetAllAsync()).ToList();
        
        var employee = (await bll.Employees.GetAllAsync()).ToList().FirstOrDefault(me => me.Id.Equals(employeeId));
        var employeeMentorship = (await bll.EmployeeMentorships.GetAllAsync()).ToList().FirstOrDefault(ms => ms.EmployeeId.Equals(employee!.Id));
        var employeesMentor = (await bll.EmployeesMentors
                .GetAllAsync())
            .ToList()
            .FirstOrDefault(employeesMentor => 
                employeesMentor.EmployeeMentorshipId == employeeMentorship!.Id && employeesMentor.IsCurrentlyActive);
        var mentor = allMentors.FirstOrDefault(me => me.Id.Equals(employeesMentor!.MentorId)); 

        var newDetails = new DetailsViewModel();
        
        employee!.FirstName = menteeDetails.EmployeeFirstName;
        employee.LastName = menteeDetails.EmployeeLastName;
        employee.Profession = menteeDetails.EmployeeProfession;
        employeeMentorship!.FromDate = menteeDetails.EmployeeFromDate;
        employeeMentorship.UntilDate = menteeDetails.EmployeeUntilDate;
        employeeMentorship.TotalHours = menteeDetails.EmployeeTotalHours;
        
        bll.Employees.Update(employee);
        bll.EmployeeMentorships.Update(employeeMentorship);
        await bll.SaveChangesAsync();
        
        if (mentor!.Id.Equals(menteeDetails.NewMentorId))
        {
            employeesMentor!.FromDate = menteeDetails.EmployeeMentorFromDate;
            employeesMentor.UntilDate = menteeDetails.EmployeeMentorUntilDate;
            employeesMentor.TotalHours = menteeDetails.EmployeeMentorTotalHours;
            
            bll.EmployeesMentors.Update(employeesMentor);
            await bll.SaveChangesAsync();

            newDetails.Mentors = allMentors;
            newDetails.InitialMentorId = initialMentorId;
            newDetails.NewMentorId = initialMentorId;
            newDetails.MentorFirstName = mentor.FirstName;
            newDetails.MentorLastName = mentor.LastName;
            newDetails.EmployeeId = employee.Id;
            newDetails.EmployeeFirstName = employee.FirstName;
            newDetails.EmployeeLastName = employee.LastName;
            newDetails.EmployeeProfession = employee.Profession;
            newDetails.EmployeeFromDate = employeeMentorship.FromDate;
            newDetails.EmployeeUntilDate = employeeMentorship.UntilDate;
            newDetails.EmployeeTotalHours = employeeMentorship.TotalHours;
            newDetails.EmployeeMentorFromDate = employeesMentor.FromDate;
            newDetails.EmployeeMentorUntilDate = employeesMentor.UntilDate;
            newDetails.EmployeeMentorTotalHours = employeesMentor.TotalHours;

        } 
        else if (!mentor.Id.Equals(menteeDetails.NewMentorId))
        {
            employeesMentor!.IsCurrentlyActive = false;
            employeesMentor.FromDate = menteeDetails.EmployeeMentorFromDate;
            employeesMentor.UntilDate = menteeDetails.EmployeeMentorUntilDate;
            employeesMentor.TotalHours = menteeDetails.EmployeeMentorTotalHours;
            employeesMentor.ChangeReason = menteeDetails.ChangeReason;
            
            bll.EmployeesMentors.Update(employeesMentor);
            await bll.SaveChangesAsync();
        
            var newEmployeesMentor = new EmployeesMentor
            {
                Id = Guid.NewGuid(),
                MentorId = menteeDetails.NewMentorId,
                EmployeeMentorshipId = employeeMentorship.Id,
                FromDate = menteeDetails.NewMentorFromDate,
                UntilDate = menteeDetails.NewMentorUntilDate,
                TotalHours = menteeDetails.NewMentorTotalHours,
                IsCurrentlyActive = true,
                ChangeReason = null
            };
        
            bll.EmployeesMentors.Add(newEmployeesMentor);
            await bll.SaveChangesAsync();

            var newMentor = allMentors.FirstOrDefault(me => me.Id.Equals(menteeDetails.NewMentorId));
            
            newDetails.Mentors = allMentors;
            newDetails.InitialMentorId = menteeDetails.NewMentorId;
            newDetails.NewMentorId = menteeDetails.NewMentorId;
            newDetails.MentorFirstName = newMentor!.FirstName; // 
            newDetails.MentorLastName = newMentor.LastName; //
            newDetails.EmployeeId = employee.Id;
            newDetails.EmployeeFirstName = employee.FirstName;
            newDetails.EmployeeLastName = employee.LastName;
            newDetails.EmployeeProfession = employee.Profession;
            newDetails.EmployeeFromDate = employeeMentorship.FromDate;
            newDetails.EmployeeUntilDate = employeeMentorship.UntilDate;
            newDetails.EmployeeTotalHours = employeeMentorship.TotalHours;
            newDetails.EmployeeMentorFromDate = menteeDetails.NewMentorFromDate;
            newDetails.EmployeeMentorUntilDate = menteeDetails.NewMentorUntilDate;
            newDetails.EmployeeMentorTotalHours = menteeDetails.NewMentorTotalHours;
        }
        
        return View(newDetails);
    }

    [HttpGet]
    public IActionResult InternMentee(Guid internId)
    {
        var intern = bll.Interns.GetAll().ToList().FirstOrDefault(me => me.Id.Equals(internId));
        var internMentorship = bll.InternMentorships.GetAll().ToList().FirstOrDefault(ms => ms.InternId.Equals(internId));
        var internsMentor = bll.InternsMentors
            .GetAll()
            .ToList()
            .FirstOrDefault(internsMentor => 
                internsMentor.InternMentorshipId == internMentorship!.Id && internsMentor.IsCurrentlyActive);   
        var mentor = bll.Mentors.GetAll().ToList().FirstOrDefault(me => me.Id.Equals(internsMentor!.MentorId)); 
        
        var details = new DetailsViewModel
        {
            Mentors = bll.Mentors.GetAll().ToList(),
            InitialMentorId = mentor!.Id,
            NewMentorId = mentor.Id,
            MentorFirstName = mentor!.FirstName,
            MentorLastName = mentor.LastName,
            EmployeeId = intern!.Id,
            EmployeeFirstName = intern.FirstName,
            EmployeeLastName = intern.LastName,
            EmployeeProfession = intern.StudyProgram,
            EmployeeFromDate = internMentorship!.FromDate,
            EmployeeUntilDate = internMentorship.UntilDate,
            EmployeeTotalHours = internMentorship.TotalHours,
            EmployeeMentorFromDate = internsMentor!.FromDate,
            EmployeeMentorUntilDate = internsMentor.UntilDate,
            EmployeeMentorTotalHours = internsMentor.TotalHours,

        };
        
        return View(details);
    }

    [HttpPost]
    public async Task<IActionResult> InternMentee(DetailsViewModel menteeDetails, Guid internId, Guid initialMentorId)
    {
        var allMentors = (await bll.Mentors.GetAllAsync()).ToList();
        
        var intern = (await bll.Interns.GetAllAsync()).ToList().FirstOrDefault(me => me.Id.Equals(internId));
        var internMentorship = (await bll.InternMentorships.GetAllAsync()).ToList().FirstOrDefault(ms => ms.InternId.Equals(intern!.Id));
        var internsMentor = (await bll.InternsMentors
                .GetAllAsync())
            .ToList()
            .FirstOrDefault(internsMentor => 
                internsMentor.InternMentorshipId == internMentorship!.Id && internsMentor.IsCurrentlyActive);
        var mentor = allMentors.FirstOrDefault(me => me.Id.Equals(internsMentor!.MentorId)); 

        var newDetails = new DetailsViewModel();
        
        intern!.FirstName = menteeDetails.EmployeeFirstName;
        intern.LastName = menteeDetails.EmployeeLastName;
        intern.StudyProgram = menteeDetails.EmployeeProfession;
        internMentorship!.FromDate = menteeDetails.EmployeeFromDate;
        internMentorship.UntilDate = menteeDetails.EmployeeUntilDate;
        internMentorship.TotalHours = menteeDetails.EmployeeTotalHours;
        
        bll.Interns.Update(intern);
        bll.InternMentorships.Update(internMentorship);
        await bll.SaveChangesAsync();
        
        if (mentor!.Id.Equals(menteeDetails.NewMentorId))
        {
            internsMentor!.FromDate = menteeDetails.EmployeeMentorFromDate;
            internsMentor.UntilDate = menteeDetails.EmployeeMentorUntilDate;
            internsMentor.TotalHours = menteeDetails.EmployeeMentorTotalHours;
            
            bll.InternsMentors.Update(internsMentor);
            await bll.SaveChangesAsync();

            newDetails.Mentors = allMentors;
            newDetails.InitialMentorId = initialMentorId;
            newDetails.NewMentorId = initialMentorId;
            newDetails.MentorFirstName = mentor.FirstName;
            newDetails.MentorLastName = mentor.LastName;
            newDetails.EmployeeId = intern.Id;
            newDetails.EmployeeFirstName = intern.FirstName;
            newDetails.EmployeeLastName = intern.LastName;
            newDetails.EmployeeProfession = intern.StudyProgram;
            newDetails.EmployeeFromDate = internMentorship.FromDate;
            newDetails.EmployeeUntilDate = internMentorship.UntilDate;
            newDetails.EmployeeTotalHours = internMentorship.TotalHours;
            newDetails.EmployeeMentorFromDate = internsMentor.FromDate;
            newDetails.EmployeeMentorUntilDate = internsMentor.UntilDate;
            newDetails.EmployeeMentorTotalHours = internsMentor.TotalHours;

        } 
        else if (!mentor.Id.Equals(menteeDetails.NewMentorId))
        {
            internsMentor!.IsCurrentlyActive = false;
            internsMentor.FromDate = menteeDetails.EmployeeMentorFromDate;
            internsMentor.UntilDate = menteeDetails.EmployeeMentorUntilDate;
            internsMentor.TotalHours = menteeDetails.EmployeeMentorTotalHours;
            internsMentor.ChangeReason = menteeDetails.ChangeReason;
            
            bll.InternsMentors.Update(internsMentor);
            await bll.SaveChangesAsync();
        
            var newInternsMentor = new InternsMentor()
            {
                Id = Guid.NewGuid(),
                MentorId = menteeDetails.NewMentorId,
                InternMentorshipId = internMentorship.Id,
                FromDate = menteeDetails.NewMentorFromDate,
                UntilDate = menteeDetails.NewMentorUntilDate,
                TotalHours = menteeDetails.NewMentorTotalHours,
                IsCurrentlyActive = true,
                ChangeReason = null
            };
        
            bll.InternsMentors.Add(newInternsMentor);
            await bll.SaveChangesAsync();

            var newMentor = allMentors.FirstOrDefault(me => me.Id.Equals(menteeDetails.NewMentorId));
            
            newDetails.Mentors = allMentors;
            newDetails.InitialMentorId = menteeDetails.NewMentorId;
            newDetails.NewMentorId = menteeDetails.NewMentorId;
            newDetails.MentorFirstName = newMentor!.FirstName;
            newDetails.MentorLastName = newMentor.LastName;
            newDetails.EmployeeId = intern.Id;
            newDetails.EmployeeFirstName = intern.FirstName;
            newDetails.EmployeeLastName = intern.LastName;
            newDetails.EmployeeProfession = intern.StudyProgram;
            newDetails.EmployeeFromDate = internMentorship.FromDate;
            newDetails.EmployeeUntilDate = internMentorship.UntilDate;
            newDetails.EmployeeTotalHours = internMentorship.TotalHours;
            newDetails.EmployeeMentorFromDate = menteeDetails.NewMentorFromDate;
            newDetails.EmployeeMentorUntilDate = menteeDetails.NewMentorUntilDate;
            newDetails.EmployeeMentorTotalHours = menteeDetails.NewMentorTotalHours;
        }
        
        return View(newDetails);
    }
    
    public IActionResult DownloadSample(Guid documentId)
    {
        var sample = bll.DocumentSamples.GetAll().ToList().FirstOrDefault(me => me.Id.Equals(documentId));
        byte[] fileBytes = Convert.FromBase64String(sample!.Base64Code!);
        
        return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", sample.Title + ".docx");
    }
    
    public IActionResult DownloadInternDocument(Guid documentId)
    {
        var pdfDoc = bll.InternMentorshipDocuments.GetAll().ToList().FirstOrDefault(me => me.Id.Equals(documentId));
        byte[] fileBytes = Convert.FromBase64String(pdfDoc!.Base64Code!);
        
        return File(fileBytes, "application/pdf", pdfDoc.Id + ".docx");
    }
    
    public IActionResult DownloadEmployeeDocument(Guid documentId)
    {
        var pdfDoc = bll.EmployeeMentorshipDocuments.GetAll().ToList().FirstOrDefault(me => me.Id.Equals(documentId));
        byte[] fileBytes = Convert.FromBase64String(pdfDoc!.Base64Code!);
        
        return File(fileBytes, "application/pdf", pdfDoc.Id + ".docx");
    }

}