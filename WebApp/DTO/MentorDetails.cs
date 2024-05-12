namespace WebApp.DTO;

public class MentorDetails
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    
    // Dict of all active mentor's mentorship processes,
    // list[0] - mentor id,
    // list[1] - mentee id,
    // string - mentee name
    public Dictionary<List<Guid>, string> MentorMentorships { get; set; }
}