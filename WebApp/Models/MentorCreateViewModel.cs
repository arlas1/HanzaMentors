using Microsoft.AspNetCore.Mvc.Rendering;
using BLLDTO = App.BLL.DTO;

namespace WebApp.Models;

public class MentorCreateEditViewModel
{
    public BLLDTO.Mentor Mentor { get; set; } = default!;
    
    public SelectList? EmployeeSelectList { get; set; }
    public SelectList? InternMentorshipSelectList { get; set; }
    public SelectList? EmployeeMentorshipSelectList { get; set; }
}