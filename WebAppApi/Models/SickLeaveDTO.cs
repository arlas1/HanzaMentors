namespace WebAppApi.Models;

public class SickLeaveDTO
{
    public string MenteeId { get; set; }
    
    public DateOnly? SickLeaveFromDate { get; set; }
    public DateOnly? SickLeaveUntilDate { get; set; }
    public string? SickLeaveReason { get; set; }
}