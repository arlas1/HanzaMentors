using App.BLL.Contracts;
using App.BLL.Contracts.Services;
using App.BLL.Services;
using App.DAL.Contracts;
using App.DAL.EF;
using AutoMapper;
using Base.BLL;

namespace App.BLL;

public class AppBLL : BaseBLL<AppDbContext>, IAppBLL
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;
    
    public AppBLL(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
    {
        _mapper = mapper;
        _uow = unitOfWork;
    }

    private IMentorService? _mentors;
    public IMentorService Mentors => _mentors ?? new MentorService(_uow, _uow.Mentors, _mapper);
    
    private IAppUserService? _users;
    public IAppUserService Users => _users ?? new AppUserService(_uow, _uow.Users, _mapper);
    
    private IDocumentSampleService? _documentSamples;
    public IDocumentSampleService DocumentSamples => _documentSamples ?? new DocumentSampleService(_uow, _uow.DocumentSamples, _mapper);
    
    private IDoucmentSigningTimeService? _doucmentSigningTimes;
    public IDoucmentSigningTimeService DocumentSigningTimes => _doucmentSigningTimes ?? new DoucmentSigningTimeService(_uow, _uow.DoucmentSigningTimes, _mapper);
    
    private IEmployeeService? _employees;
    public IEmployeeService Employees => _employees ?? new EmployeeService(_uow, _uow.Employees, _mapper);
    
    private IEmployeeMentorshipService? _employeeMentorships;
    public IEmployeeMentorshipService EmployeeMentorships => _employeeMentorships ?? new EmployeeMentorshipService(_uow, _uow.EmployeeMentorships, _mapper);
    
    private IEmployeeMentorshipDocumentService? _employeeMentorshipDocuments;
    public IEmployeeMentorshipDocumentService EmployeeMentorshipDocuments => _employeeMentorshipDocuments ?? new EmployeeMentorshipDocumentService(_uow, _uow.EmployeeMentorshipDocuments, _mapper);
    
    private IFactorySupervisorService? _factorySupervisors;
    public IFactorySupervisorService FactorySupervisors => _factorySupervisors ?? new FactorySupervisorService(_uow, _uow.FactorySupervisors, _mapper);

    private IInternService? _interns;
    public IInternService Interns => _interns ?? new InternService(_uow, _uow.Interns, _mapper);

    private IInternMentorshipService? _internMentorships;
    public IInternMentorshipService InternMentorships => _internMentorships ?? new InternMentorshipService(_uow, _uow.InternMentorships, _mapper);

    private IInternMentorshipDocumentService? _internMentorshipDocuments;
    public IInternMentorshipDocumentService InternMentorshipDocuments => _internMentorshipDocuments ?? new InternMentorshipDocumentService(_uow, _uow.InternMentorshipDocuments, _mapper);
    
    private IInternSupervisorService? _internSupervisors;
    public IInternSupervisorService InternSupervisors => _internSupervisors ?? new InternSupervisorService(_uow, _uow.InternSupervisors, _mapper);

    private IMenteeSickLeaveService? _menteeSickLeaves;
    public IMenteeSickLeaveService MenteeSickLeaves => _menteeSickLeaves ?? new MenteeSickLeaveService(_uow, _uow.MenteeSickLeaves, _mapper);

    private IInternsMentorService? _internsMentors;
    public IInternsMentorService InternsMentors => _internsMentors ?? new InternsMentorService(_uow, _uow.InternsMentors, _mapper);
    
    private IEmployeesMentorService? _employeesMentors;
    public IEmployeesMentorService EmployeesMentors => _employeesMentors ?? new EmployeesMentorService(_uow, _uow.EmployeesMentors, _mapper);
}