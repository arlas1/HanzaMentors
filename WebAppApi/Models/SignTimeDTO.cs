namespace WebAppApi.Models;

public class SignTimeDTO
{
    public string? DocumentId { get; set; }
    
    public List<string>? AvailableTimes { get; set; }
    public string? ChosenTime { get; set; }
    public string? ChosenWay { get; set; }
}