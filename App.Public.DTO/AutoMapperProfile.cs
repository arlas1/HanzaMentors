using AutoMapper;

namespace App.Public.DTO;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.BLL.DTO.AppUser, App.Public.DTO.v1.AppUser>().ReverseMap();

        CreateMap<App.BLL.DTO.Mentor, App.Public.DTO.v1.Mentor>().ReverseMap();
        CreateMap<App.BLL.DTO.Intern, App.Public.DTO.v1.Intern>().ReverseMap();

        CreateMap<App.BLL.DTO.Employee, App.Public.DTO.v1.Employee>().ReverseMap();
        // CreateMap<App.Public.DTO.Employee, App.BLL.DTO.Employee>();
        // CreateMap<App.Public.DTO.Employee, App.BLL.DTO.Employee>().ReverseMap();


        CreateMap<App.BLL.DTO.InternMentorship, App.Public.DTO.v1.InternMentorship>().ReverseMap();
        CreateMap<App.BLL.DTO.DocumentSample, App.Public.DTO.v1.DocumentSample>().ReverseMap();
        CreateMap<App.BLL.DTO.DoucmentSigningTime, App.Public.DTO.v1.DoucmentSigningTime>().ReverseMap();
        CreateMap<App.BLL.DTO.EmployeeMentorship, App.Public.DTO.v1.EmployeeMentorship>().ReverseMap();
        CreateMap<App.BLL.DTO.EmployeeMentorshipDocument, App.Public.DTO.v1.EmployeeMentorshipDocument>().ReverseMap();
        CreateMap<App.BLL.DTO.EmployeesMentor, App.Public.DTO.v1.EmployeesMentor>().ReverseMap();
        CreateMap<App.BLL.DTO.FactorySupervisor, App.Public.DTO.v1.FactorySupervisor>().ReverseMap();
        CreateMap<App.BLL.DTO.InternMentorshipDocument, App.Public.DTO.v1.InternMentorshipDocument>().ReverseMap();
        CreateMap<App.BLL.DTO.InternsMentor, App.Public.DTO.v1.InternsMentor>().ReverseMap();
        CreateMap<App.BLL.DTO.InternSupervisor, App.Public.DTO.v1.InternSupervisor>().ReverseMap();
        CreateMap<App.BLL.DTO.MenteeSickLeave, App.Public.DTO.v1.MenteeSickLeave>().ReverseMap();
    }
}