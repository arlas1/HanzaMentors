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
    IInternsMentorRepository InternsMentors { get; }
    
    IEmployeeRepository Employees { get; }
    IEmployeeMentorshipRepository EmployeeMentorships { get; }
    IFactorySupervisorRepository FactorySupervisors { get; }
    IEmployeeMentorshipDocumentRepository EmployeeMentorshipDocuments { get; }
    IEmployeesMentorRepository EmployeesMentors { get; }
    
    IMentorRepository Mentors { get; }
    IMenteeSickLeaveRepository MenteeSickLeaves { get; }
}