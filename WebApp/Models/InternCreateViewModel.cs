using Microsoft.AspNetCore.Mvc.Rendering;
using BLLDTO = App.BLL.DTO;

namespace WebApp.Models;

public class InternCreateEditViewModel
{
    public BLLDTO.Intern Intern { get; set; } = default!;
    public SelectList? AppUserSelectList { get; set; }
}