using Microsoft.AspNetCore.Mvc.Rendering;
using BLLDTO = App.BLL.DTO;

namespace WebApp.Models;

public class EmployeeCreateEditViewModel
{
    public BLLDTO.Employee Employee { get; set; } = default!;
    public SelectList? AppUserSelectList { get; set; }
}
