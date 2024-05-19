namespace App.Public.DTO;

public class MentorData
{
    public DateOnly? MentorFromDate { get; set; }
    public DateOnly? MentorUntilDate { get; set; }
    public int? MentorTotalHours { get; set; }
    public string? MenteeFullName { get; set; }
    public string? MenteeType { get; set; }
    public bool IsOnSickLeave { get; set; }
}