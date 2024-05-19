
using App.BLL.DTO;
using App.Public.DTO;

namespace WebApp.Models;

public class HomeViewModel
{
    public int TotalMentorsAmount { get; set; }
    public int ActiveMentorshipsAmount { get; set; }
    public int ActiveMenteesAmount { get; set; }
    public int MenteesOnSickLeaveAmount { get; set; }
    public int FactorySupervisorsAmount { get; set; }
    
    public List<MentorData>? MentorData { get; set; }
}