using App.BLL.Contracts.Services;
using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface IAppBLL : IBLL
{
    IMentorService Mentors { get; }
    IAppUserService Users { get; }
    IDocumentSampleService DocumentSamples { get; }
    IDoucmentSigningTimeService DocumentSigningTimes { get;}
    IEmployeeService Employees { get;}
    IEmployeeMentorshipService EmployeeMentorships { get; }
    IEmployeeMentorshipDocumentService EmployeeMentorshipDocuments { get; }
    IEmployeesMentorService EmployeesMentors { get; }
    IFactorySupervisorService FactorySupervisors { get; }
    IInternService Interns { get; }
    IInternMentorshipService InternMentorships { get; }
    IInternMentorshipDocumentService InternMentorshipDocuments  { get; }
    IInternsMentorService InternsMentors  { get; }
    IInternSupervisorService InternSupervisors { get; }
    IMenteeSickLeaveService MenteeSickLeaves { get; }
}