using BLLDTO = App.BLL.DTO;

namespace WebApp.Models;

public class HomeViewModel
{
    public List<BLLDTO.Intern>? Interns { get; set; }
}