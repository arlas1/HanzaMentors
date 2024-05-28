using System.Net;
using App.BLL.Contracts;
using App.BLL.DTO;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
using WebApp.Models;
using WebAppApi.Models;

namespace WebAppApi.ApiControllers;

/// <summary>
/// Api controller with actions related to editing mentee and mentor
/// </summary>
/// <param name="bll"></param>
[ApiVersion( "1.0" )]
[ApiController]
[Route("/api/v{version:apiVersion}/[controller]/[action]")]
public class DetailsApiController(IAppBLL bll) : ControllerBase
{
    /// <summary>
    /// Api action for loading mentor edit page 
    /// </summary>
    /// <param name="request"></param>
    /// <returns>DetailsViewModel</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(DetailsViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult GetMentor([FromBody] MentorRequestDTO request)
    {
        var mentor = bll.Mentors.GetAll().ToList().FirstOrDefault(me => me.Id.Equals(Guid.Parse(request.MentorId)));
        var details = new DetailsViewModel
        {
            InitialMentorId = mentor!.Id,
            MentorFirstName = mentor!.FirstName,
            MentorLastName = mentor.LastName,
        };
        
        return Ok(details);
    }
    
    
    /// <summary>
    /// Api action for loading mentor edit page 
    /// </summary>
    /// <param name="request"></param>
    /// <returns>DetailsViewModel</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(DetailsViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult UpdateMentor([FromBody] MentorRequestDTO request)
    {
        var mentor = bll.Mentors.GetAll().ToList().FirstOrDefault(me => me.Id.Equals(Guid.Parse(request.MentorId)));
        mentor!.FirstName = request.FirstName;
        mentor.LastName = request.LastName;
        
        bll.Mentors.Update(mentor);
        bll.SaveChangesAsync();
        
        var details = new DetailsViewModel
        {
            InitialMentorId = mentor.Id,
            MentorFirstName = mentor.FirstName,
            MentorLastName = mentor.LastName,
        };
        
        return Ok(details);
    }

    
    /// <summary>
    /// Api action for loading employee mentee edit page
    /// </summary>
    /// <param name="request"></param>
    /// <returns>MenteeRequestDTO</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(MenteeRequestDTO), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult GetEmployeeMentee([FromBody] MenteeIdDTO request)
    {
        var employee = bll.Employees.GetAll().ToList().FirstOrDefault(me => me.Id.Equals(Guid.Parse(request.MenteeId)));
        var employeeMentorship = bll.EmployeeMentorships.GetAll().ToList().FirstOrDefault(ms => ms.EmployeeId.Equals(Guid.Parse(request.MenteeId)));
        var employeesMentor = bll.EmployeesMentors
            .GetAll()
            .ToList()
            .FirstOrDefault(employeesMentor => 
                employeesMentor.EmployeeMentorshipId == employeeMentorship!.Id && employeesMentor.IsCurrentlyActive);   
        var mentor = bll.Mentors.GetAll().ToList().FirstOrDefault(me => me.Id.Equals(employeesMentor!.MentorId)); 
        
        var details = new MenteeRequestDTO
        {
            Mentors = bll.Mentors.GetAll().ToList(),
            InitialMentorId = mentor!.Id.ToString(),
            NewMentorId = mentor.Id.ToString(),
            MentorFirstName = mentor!.FirstName,
            MentorLastName = mentor.LastName,
            MenteeId = employee!.Id.ToString(),
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
        
        return Ok(details);
    }
    
    
    /// <summary>
    /// Api action for editing employee mentee
    /// </summary>
    /// <param name="request"></param>
    /// <returns>DetailsViewModel</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(DetailsViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EmployeeMentee([FromBody] MenteeRequest1DTO request)
    {
        var allMentors = (await bll.Mentors.GetAllAsync()).ToList();
        
        var employee = (await bll.Employees.GetAllAsync())
            .ToList()
            .FirstOrDefault(me => me.Id.Equals(Guid.Parse(request.MenteeId)));
        var employeeMentorship = (await bll.EmployeeMentorships.GetAllAsync())
            .ToList()
            .FirstOrDefault(ms => ms.EmployeeId.Equals(employee!.Id));
        var employeesMentor = (await bll.EmployeesMentors
                .GetAllAsync())
            .ToList()
            .FirstOrDefault(employeesMentor => 
                employeesMentor.EmployeeMentorshipId == employeeMentorship!.Id && employeesMentor.IsCurrentlyActive);
        var mentor = allMentors.FirstOrDefault(me => me.Id.Equals(employeesMentor!.MentorId)); 

        var newDetails = new DetailsViewModel();
        
        employee!.FirstName!.SetTranslation(request.EmployeeFirstName!);
        employee.LastName!.SetTranslation(request.EmployeeLastName!);
        employee.Profession!.SetTranslation(request.EmployeeProfession!);
        employeeMentorship!.FromDate = request.EmployeeFromDate;
        employeeMentorship.UntilDate = request.EmployeeUntilDate;
        employeeMentorship.TotalHours = request.EmployeeTotalHours;
        
        bll.Employees.Update(employee);
        bll.EmployeeMentorships.Update(employeeMentorship);
        await bll.SaveChangesAsync();
        
        if (mentor!.Id.Equals(Guid.Parse(request.NewMentorId)))
        {
            employeesMentor!.FromDate = request.EmployeeMentorFromDate;
            employeesMentor.UntilDate = request.EmployeeMentorUntilDate;
            employeesMentor.TotalHours = request.EmployeeMentorTotalHours;
            
            bll.EmployeesMentors.Update(employeesMentor);
            await bll.SaveChangesAsync();

            newDetails.Mentors = allMentors;
            newDetails.InitialMentorId = Guid.Parse(request.InitialMentorId);
            newDetails.NewMentorId = Guid.Parse(request.InitialMentorId);
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
        else if (!mentor.Id.Equals(Guid.Parse(request.NewMentorId)))
        {
            employeesMentor!.IsCurrentlyActive = false;
            employeesMentor.FromDate = request.EmployeeMentorFromDate;
            employeesMentor.UntilDate = request.EmployeeMentorUntilDate;
            employeesMentor.TotalHours = request.EmployeeMentorTotalHours;
            employeesMentor.ChangeReason = request.ChangeReason;
            
            bll.EmployeesMentors.Update(employeesMentor);
            await bll.SaveChangesAsync();
        
            var newEmployeesMentor = new EmployeesMentor
            {
                Id = Guid.NewGuid(),
                MentorId = Guid.Parse(request.NewMentorId),
                EmployeeMentorshipId = employeeMentorship.Id,
                FromDate = request.NewMentorFromDate,
                UntilDate = request.NewMentorUntilDate,
                TotalHours = request.NewMentorTotalHours,
                IsCurrentlyActive = true,
                ChangeReason = null
            };
        
            bll.EmployeesMentors.Add(newEmployeesMentor);
            await bll.SaveChangesAsync();

            var newMentor = allMentors.FirstOrDefault(me => me.Id.Equals(Guid.Parse(request.NewMentorId)));
            
            newDetails.Mentors = allMentors;
            newDetails.InitialMentorId = Guid.Parse(request.NewMentorId);
            newDetails.NewMentorId = Guid.Parse(request.NewMentorId);
            newDetails.MentorFirstName = newMentor!.FirstName; // 
            newDetails.MentorLastName = newMentor.LastName; //
            newDetails.EmployeeId = employee.Id;
            newDetails.EmployeeFirstName = employee.FirstName;
            newDetails.EmployeeLastName = employee.LastName;
            newDetails.EmployeeProfession = employee.Profession;
            newDetails.EmployeeFromDate = employeeMentorship.FromDate;
            newDetails.EmployeeUntilDate = employeeMentorship.UntilDate;
            newDetails.EmployeeTotalHours = employeeMentorship.TotalHours;
            newDetails.EmployeeMentorFromDate = request.NewMentorFromDate;
            newDetails.EmployeeMentorUntilDate = request.NewMentorUntilDate;
            newDetails.EmployeeMentorTotalHours = request.NewMentorTotalHours;
        }
        
        return Ok(newDetails);
    }
    
    
    /// <summary>
    /// Api action for loading intern mentee edit page
    /// </summary>
    /// <param name="request"></param>
    /// <returns>MenteeRequestDTO</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(MenteeRequestDTO), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult GetInternMentee([FromBody] MenteeIdDTO request)
    {
        var employee = bll.Interns.GetAll().ToList().FirstOrDefault(me => me.Id.Equals(Guid.Parse(request.MenteeId)));
        var employeeMentorship = bll.InternMentorships.GetAll().ToList().FirstOrDefault(ms => ms.InternId.Equals(Guid.Parse(request.MenteeId)));
        var employeesMentor = bll.InternsMentors
            .GetAll()
            .ToList()
            .FirstOrDefault(employeesMentor => 
                employeesMentor.InternMentorshipId == employeeMentorship!.Id && employeesMentor.IsCurrentlyActive);   
        var mentor = bll.Mentors.GetAll().ToList().FirstOrDefault(me => me.Id.Equals(employeesMentor!.MentorId)); 
        
        var details = new MenteeRequestDTO
        {
            Mentors = bll.Mentors.GetAll().ToList(),
            InitialMentorId = mentor!.Id.ToString(),
            NewMentorId = mentor.Id.ToString(),
            MentorFirstName = mentor!.FirstName,
            MentorLastName = mentor.LastName,
            MenteeId = employee!.Id.ToString(),
            EmployeeFirstName = employee.FirstName,
            EmployeeLastName = employee.LastName,
            EmployeeProfession = employee.StudyProgram,
            EmployeeFromDate = employeeMentorship!.FromDate,
            EmployeeUntilDate = employeeMentorship.UntilDate,
            EmployeeTotalHours = employeeMentorship.TotalHours,
            EmployeeMentorFromDate = employeesMentor!.FromDate,
            EmployeeMentorUntilDate = employeesMentor.UntilDate,
            EmployeeMentorTotalHours = employeesMentor.TotalHours,

        };
        
        return Ok(details);
    }
    
    
    /// <summary>
    /// Api action for editing intern mentee
    /// </summary>
    /// <param name="request"></param>
    /// <returns>DetailsViewModel</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(DetailsViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> InternMentee([FromBody] MenteeRequest1DTO request)
    {
        var allMentors = (await bll.Mentors.GetAllAsync()).ToList();
        
        var employee = (await bll.Interns.GetAllAsync())
            .ToList()
            .FirstOrDefault(me => me.Id.Equals(Guid.Parse(request.MenteeId)));
        var employeeMentorship = (await bll.InternMentorships.GetAllAsync())
            .ToList()
            .FirstOrDefault(ms => ms.InternId.Equals(employee!.Id));
        var employeesMentor = (await bll.InternsMentors
                .GetAllAsync())
            .ToList()
            .FirstOrDefault(employeesMentor => 
                employeesMentor.InternMentorshipId == employeeMentorship!.Id && employeesMentor.IsCurrentlyActive);
        var mentor = allMentors.FirstOrDefault(me => me.Id.Equals(employeesMentor!.MentorId)); 

        var newDetails = new DetailsViewModel();
        
        employee!.FirstName!.SetTranslation(request.EmployeeFirstName!);
        employee.LastName!.SetTranslation(request.EmployeeLastName!);
        employee.StudyProgram!.SetTranslation(request.EmployeeProfession!);
        employeeMentorship!.FromDate = request.EmployeeFromDate;
        employeeMentorship.UntilDate = request.EmployeeUntilDate;
        employeeMentorship.TotalHours = request.EmployeeTotalHours;
        
        bll.Interns.Update(employee);
        bll.InternMentorships.Update(employeeMentorship);
        await bll.SaveChangesAsync();
        
        if (mentor!.Id.Equals(Guid.Parse(request.NewMentorId)))
        {
            employeesMentor!.FromDate = request.EmployeeMentorFromDate;
            employeesMentor.UntilDate = request.EmployeeMentorUntilDate;
            employeesMentor.TotalHours = request.EmployeeMentorTotalHours;
            
            bll.InternsMentors.Update(employeesMentor);
            await bll.SaveChangesAsync();

            newDetails.Mentors = allMentors;
            newDetails.InitialMentorId = Guid.Parse(request.InitialMentorId);
            newDetails.NewMentorId = Guid.Parse(request.InitialMentorId);
            newDetails.MentorFirstName = mentor.FirstName;
            newDetails.MentorLastName = mentor.LastName;
            newDetails.EmployeeId = employee.Id;
            newDetails.EmployeeFirstName = employee.FirstName;
            newDetails.EmployeeLastName = employee.LastName;
            newDetails.EmployeeProfession = employee.StudyProgram;
            newDetails.EmployeeFromDate = employeeMentorship.FromDate;
            newDetails.EmployeeUntilDate = employeeMentorship.UntilDate;
            newDetails.EmployeeTotalHours = employeeMentorship.TotalHours;
            newDetails.EmployeeMentorFromDate = employeesMentor.FromDate;
            newDetails.EmployeeMentorUntilDate = employeesMentor.UntilDate;
            newDetails.EmployeeMentorTotalHours = employeesMentor.TotalHours;

        } 
        else if (!mentor.Id.Equals(Guid.Parse(request.NewMentorId)))
        {
            employeesMentor!.IsCurrentlyActive = false;
            employeesMentor.FromDate = request.EmployeeMentorFromDate;
            employeesMentor.UntilDate = request.EmployeeMentorUntilDate;
            employeesMentor.TotalHours = request.EmployeeMentorTotalHours;
            employeesMentor.ChangeReason = request.ChangeReason;
            
            bll.InternsMentors.Update(employeesMentor);
            await bll.SaveChangesAsync();
        
            var newEmployeesMentor = new InternsMentor()
            {
                Id = Guid.NewGuid(),
                MentorId = Guid.Parse(request.NewMentorId),
                InternMentorshipId = employeeMentorship.Id,
                FromDate = request.NewMentorFromDate,
                UntilDate = request.NewMentorUntilDate,
                TotalHours = request.NewMentorTotalHours,
                IsCurrentlyActive = true,
                ChangeReason = null
            };
        
            bll.InternsMentors.Add(newEmployeesMentor);
            await bll.SaveChangesAsync();

            var newMentor = allMentors.FirstOrDefault(me => me.Id.Equals(Guid.Parse(request.NewMentorId)));
            
            newDetails.Mentors = allMentors;
            newDetails.InitialMentorId = Guid.Parse(request.NewMentorId);
            newDetails.NewMentorId = Guid.Parse(request.NewMentorId);
            newDetails.MentorFirstName = newMentor!.FirstName; // 
            newDetails.MentorLastName = newMentor.LastName; //
            newDetails.EmployeeId = employee.Id;
            newDetails.EmployeeFirstName = employee.FirstName;
            newDetails.EmployeeLastName = employee.LastName;
            newDetails.EmployeeProfession = employee.StudyProgram;
            newDetails.EmployeeFromDate = employeeMentorship.FromDate;
            newDetails.EmployeeUntilDate = employeeMentorship.UntilDate;
            newDetails.EmployeeTotalHours = employeeMentorship.TotalHours;
            newDetails.EmployeeMentorFromDate = request.NewMentorFromDate;
            newDetails.EmployeeMentorUntilDate = request.NewMentorUntilDate;
            newDetails.EmployeeMentorTotalHours = request.NewMentorTotalHours;
        }
        
        return Ok(newDetails);
    }
}