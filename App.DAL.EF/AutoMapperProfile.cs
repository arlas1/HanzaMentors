using AutoMapper;

namespace App.DAL.EF;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.DAL.DTO.AppUser, App.Domain.Identity.AppUser>().ReverseMap();

        CreateMap<App.DAL.DTO.Mentor, App.Domain.Mentor>().ReverseMap();
        CreateMap<App.DAL.DTO.Intern, App.Domain.Intern>().ReverseMap();
        
        CreateMap<App.DAL.DTO.Employee, App.Domain.Employee>().ReverseMap();
        CreateMap<App.Domain.Employee, App.DAL.DTO.Employee>();
        CreateMap<App.Domain.Employee, App.DAL.DTO.Employee>().ReverseMap();

        
        CreateMap<App.DAL.DTO.InternMentorship, App.Domain.InternMentorship>().ReverseMap();
        CreateMap<App.DAL.DTO.DocumentSample, App.Domain.DocumentSample>().ReverseMap(); ;
        CreateMap<App.DAL.DTO.DoucmentSigningTime, App.Domain.DoucmentSigningTime>().ReverseMap();
        CreateMap<App.DAL.DTO.EmployeeMentorship, App.Domain.EmployeeMentorship>().ReverseMap();
        CreateMap<App.DAL.DTO.EmployeeMentorshipDocument, App.Domain.EmployeeMentorshipDocument>().ReverseMap();
        CreateMap<App.DAL.DTO.EmployeesMentor, App.Domain.EmployeesMentor>().ReverseMap();
        CreateMap<App.DAL.DTO.FactorySupervisor, App.Domain.FactorySupervisor>().ReverseMap();
        CreateMap<App.DAL.DTO.InternMentorshipDocument, App.Domain.InternMentorshipDocument>().ReverseMap();
        CreateMap<App.DAL.DTO.InternsMentor, App.Domain.InternsMentor>().ReverseMap();
        CreateMap<App.DAL.DTO.InternSupervisor, App.Domain.InternSupervisor>().ReverseMap();
        CreateMap<App.DAL.DTO.MenteeSickLeave, App.Domain.MenteeSickLeave>().ReverseMap();
    }
}