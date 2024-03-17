using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using App.DAL.EF.Repositories;
using Base.DAL.EF;

namespace App.DAL.EF;

public class UnitOfWork : BaseUnitOfWork<AppDbContext>, IUnitOfWork
{
    public UnitOfWork(AppDbContext dbContext) : base(dbContext)
    {
    }

    private IDocumentSampleRepository? _documentSamples;
    public IDocumentSampleRepository DocumentSamples =>
        _documentSamples ?? new DocumentSampleRepository(UowDbContext);
    
    private IInternRepository? _interns;
    public IInternRepository Interns =>
        _interns ?? new InternRepository(UowDbContext);
    
    private IInternMentorshipRepository? _internMentorships;
    public IInternMentorshipRepository InternMentorships =>
        _internMentorships ?? new InternMentorshipRepository(UowDbContext);
    
    private IInternSupervisorRepository? _internSupervisors;
    public IInternSupervisorRepository InternSupervisors =>
        _internSupervisors ?? new InternSupervisorRepository(UowDbContext);
    
    private IInternMentorshipDocumentRepository? _internMentorshipDocuments;
    public IInternMentorshipDocumentRepository InternMentorshipDocuments =>
        _internMentorshipDocuments ?? new InternMentorshipDocumentRepository(UowDbContext);
    
    private IInternMentorshipUntilDateRepository? _internMentorshipUntilDates;
    public IInternMentorshipUntilDateRepository InternMentorshipUntilDates =>
        _internMentorshipUntilDates ?? new InternMentorshipUntilDateRepository(UowDbContext);
    
    private IEmployeeRepository? _employees;
    public IEmployeeRepository Employees =>
        _employees ?? new EmployeeRepository(UowDbContext);
    
    private IEmployeeMentorshipRepository? _employeeMentorships;
    public IEmployeeMentorshipRepository EmployeeMentorships =>
        _employeeMentorships ?? new EmployeeMentorshipRepository(UowDbContext);
    
    private IFactorySupervisorRepository? _factorySupervisors;
    public IFactorySupervisorRepository FactorySupervisors =>
        _factorySupervisors ?? new FactorySupervisorRepository(UowDbContext);
    
    private IEmployeeMentorshipDocumentRepository? _employeeMentorshipDocuments;
    public IEmployeeMentorshipDocumentRepository EmployeeMentorshipDocuments =>
        _employeeMentorshipDocuments ?? new EmployeeMentorshipDocumentRepository(UowDbContext);
    
    private IEmployeeMentorshipUntilDateRepository? _employeeMentorshipUntilDates;
    public IEmployeeMentorshipUntilDateRepository EmployeeMentorshipUntilDates =>
        _employeeMentorshipUntilDates ?? new EmployeeMentorshipUntilDateRepository(UowDbContext);
    
    private IMentorRepository? _mentors;
    public IMentorRepository Mentors =>
        _mentors ?? new MentorRepository(UowDbContext);
    
    private IMenteeSickLeaveRepository? _menteeSickLeaves;
    public IMenteeSickLeaveRepository MenteeSickLeaves =>
        _menteeSickLeaves ?? new MenteeSickLeaveRepository(UowDbContext);
    
    private ISickLeaveTypeRepository? _sickLeaveTypes;
    public ISickLeaveTypeRepository SickLeaveTypes =>
        _sickLeaveTypes ?? new SickLeaveTypeRepository(UowDbContext);
    
}
