﻿
using App.BLL.DTO;
using App.Public.DTO;
using Spire.Additions.Xps.Schema;

namespace WebApp.Models;

public class HomeViewModel
{
    public string Type { get; set; }
    public int TotalMentorsAmount { get; set; }
    public int ActiveMentorshipsAmount { get; set; }
    public int ActiveMenteesAmount { get; set; }
    public int MenteesOnSickLeaveAmount { get; set; }
    public int FactorySupervisorsAmount { get; set; }
    public List<MentorData>? MentorData { get; set; }
    public Guid MenteeId { get; set; }
    public string? MenteeType { get; set; }
    public DateOnly? MenteeFromDate { get; set; }
    public DateOnly? MenteeUntilDate { get; set; }
    public int? MenteeTotalHours { get; set; }
    public bool IsOnSickLeave { get; set; }
    public MenteeData? MenteeData { get; set; }
    
    public List<string>? InternsMentors { get; set; }
    public List<InternMentorshipDocument>? InternDocuments { get; set; }
    
    public List<string>? EmployeesMentors { get; set; }
    public List<EmployeeMentorshipDocument>? EmployeeDocuments { get; set; }
}