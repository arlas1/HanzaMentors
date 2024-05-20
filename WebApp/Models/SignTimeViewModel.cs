namespace WebApp.Models;

public class SignTimeViewModel
{
    public List<string>? AvailableTimes { get; set; }
    public string? ChosenTime { get; set; }
    public string? ChosenWay { get; set; }
    public Guid DocumentId { get; set; }
    public Guid MenteeId { get; set; }
}