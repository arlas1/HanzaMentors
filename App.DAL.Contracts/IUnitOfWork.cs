using App.DAL.Contracts.Repositories;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IUnitOfWork : IBaseUnitOfWork
{
    IAppUserRepository Users { get; }
    IDocumentSampleRepository DocumentSamples { get; }
    IDoucmentSigningTimeRepository DoucmentSigningTimes { get; }
    
    IInternRepository Interns { get; }
    IInternMentorshipRepository InternMentorships { get; }
    IInternSupervisorRepository InternSupervisors { get; }
    IInternMentorshipDocumentRepository InternMentorshipDocuments { get; }
    IInternMentorshipUntilDateRepository InternMentorshipUntilDates { get; }
    
    IEmployeeRepository Employees { get; }
    IEmployeeMentorshipRepository EmployeeMentorships { get; }
    IFactorySupervisorRepository FactorySupervisors { get; }
    IEmployeeMentorshipDocumentRepository EmployeeMentorshipDocuments { get; }
    IEmployeeMentorshipUntilDateRepository EmployeeMentorshipUntilDates { get; }
    
    IMentorRepository Mentors { get; }
    IMenteeSickLeaveRepository MenteeSickLeaves { get; }
    ISickLeaveTypeRepository SickLeaveTypes { get; }
}