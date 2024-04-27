using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using App.DAL.EF.Repositories;
using AutoMapper;
using Base.DAL.EF;

namespace App.DAL.EF;

public class UnitOfWork : BaseUnitOfWork<AppDbContext>, IUnitOfWork
{
    private readonly IMapper _mapper;
    public UnitOfWork(AppDbContext dbContext, IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }
    
    private IAppUserRepository? _users;
    public IAppUserRepository Users => 
        _users ?? new AppUserRepository(UowDbContext, _mapper);

    private IDocumentSampleRepository? _documentSamples;
    public IDocumentSampleRepository DocumentSamples =>
        _documentSamples ?? new DocumentSampleRepository(UowDbContext, _mapper);

    private IDoucmentSigningTimeRepository? _doucmentSigningTimes;
    public IDoucmentSigningTimeRepository DoucmentSigningTimes =>
        _doucmentSigningTimes ?? new DoucmentSigningTimeRepository(UowDbContext, _mapper);
    
    private IInternRepository? _interns;
    public IInternRepository Interns =>
        _interns ?? new InternRepository(UowDbContext, _mapper);
    
    private IInternMentorshipRepository? _internMentorships;
    public IInternMentorshipRepository InternMentorships =>
        _internMentorships ?? new InternMentorshipRepository(UowDbContext, _mapper);
    
    private IInternSupervisorRepository? _internSupervisors;
    public IInternSupervisorRepository InternSupervisors =>
        _internSupervisors ?? new InternSupervisorRepository(UowDbContext, _mapper);
    
    private IInternMentorshipDocumentRepository? _internMentorshipDocuments;
    public IInternMentorshipDocumentRepository InternMentorshipDocuments =>
        _internMentorshipDocuments ?? new InternMentorshipDocumentRepository(UowDbContext, _mapper);
    
    private IInternMentorshipUntilDateRepository? _internMentorshipUntilDates;
    public IInternMentorshipUntilDateRepository InternMentorshipUntilDates =>
        _internMentorshipUntilDates ?? new InternMentorshipUntilDateRepository(UowDbContext, _mapper);
    
    private IEmployeeRepository? _employees;
    public IEmployeeRepository Employees =>
        _employees ?? new EmployeeRepository(UowDbContext, _mapper);
    
    private IEmployeeMentorshipRepository? _employeeMentorships;
    public IEmployeeMentorshipRepository EmployeeMentorships =>
        _employeeMentorships ?? new EmployeeMentorshipRepository(UowDbContext, _mapper);
    
    private IFactorySupervisorRepository? _factorySupervisors;
    public IFactorySupervisorRepository FactorySupervisors =>
        _factorySupervisors ?? new FactorySupervisorRepository(UowDbContext, _mapper);
    
    private IEmployeeMentorshipDocumentRepository? _employeeMentorshipDocuments;
    public IEmployeeMentorshipDocumentRepository EmployeeMentorshipDocuments =>
        _employeeMentorshipDocuments ?? new EmployeeMentorshipDocumentRepository(UowDbContext, _mapper);
    
    private IEmployeeMentorshipUntilDateRepository? _employeeMentorshipUntilDates;
    public IEmployeeMentorshipUntilDateRepository EmployeeMentorshipUntilDates =>
        _employeeMentorshipUntilDates ?? new EmployeeMentorshipUntilDateRepository(UowDbContext, _mapper);
    
    private IMentorRepository? _mentors;
    public IMentorRepository Mentors =>
        _mentors ?? new MentorRepository(UowDbContext, _mapper);
    
    private IMenteeSickLeaveRepository? _menteeSickLeaves;
    public IMenteeSickLeaveRepository MenteeSickLeaves =>
        _menteeSickLeaves ?? new MenteeSickLeaveRepository(UowDbContext, _mapper);
    
    private ISickLeaveTypeRepository? _sickLeaveTypes;
    public ISickLeaveTypeRepository SickLeaveTypes =>
        _sickLeaveTypes ?? new SickLeaveTypeRepository(UowDbContext, _mapper);
    
}
