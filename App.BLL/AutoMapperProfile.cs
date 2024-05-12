using AutoMapper;

namespace App.BLL;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.DAL.DTO.AppUser, App.BLL.DTO.AppUser>().ReverseMap();

        CreateMap<App.DAL.DTO.Mentor, App.BLL.DTO.Mentor>().ReverseMap();
        CreateMap<App.DAL.DTO.Intern, App.BLL.DTO.Intern>().ReverseMap();
        
        CreateMap<App.DAL.DTO.Employee, App.BLL.DTO.Employee>().ReverseMap();
        // CreateMap<App.BLL.DTO.Employee, App.DAL.DTO.Employee>();
        // CreateMap<App.BLL.DTO.Employee, App.DAL.DTO.Employee>().ReverseMap();

        
        CreateMap<App.DAL.DTO.InternMentorship, App.BLL.DTO.InternMentorship>().ReverseMap();
        CreateMap<App.DAL.DTO.DocumentSample, App.BLL.DTO.DocumentSample>().ReverseMap(); ;
        CreateMap<App.DAL.DTO.DoucmentSigningTime, App.BLL.DTO.DoucmentSigningTime>().ReverseMap();
        CreateMap<App.DAL.DTO.EmployeeMentorship, App.BLL.DTO.EmployeeMentorship>().ReverseMap();
        CreateMap<App.DAL.DTO.EmployeeMentorshipDocument, App.BLL.DTO.EmployeeMentorshipDocument>().ReverseMap();
        CreateMap<App.DAL.DTO.EmployeesMentor, App.BLL.DTO.EmployeesMentor>().ReverseMap();
        CreateMap<App.DAL.DTO.FactorySupervisor, App.BLL.DTO.FactorySupervisor>().ReverseMap();
        CreateMap<App.DAL.DTO.InternMentorshipDocument, App.BLL.DTO.InternMentorshipDocument>().ReverseMap();
        CreateMap<App.DAL.DTO.InternsMentor, App.BLL.DTO.InternsMentor>().ReverseMap();
        CreateMap<App.DAL.DTO.InternSupervisor, App.BLL.DTO.InternSupervisor>().ReverseMap();
        CreateMap<App.DAL.DTO.MenteeSickLeave, App.BLL.DTO.MenteeSickLeave>().ReverseMap();
    }
}