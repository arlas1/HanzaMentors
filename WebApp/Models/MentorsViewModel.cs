using App.BLL.DTO;

namespace WebApp.Models;

public class MentorsViewModel 
{ 
    public IEnumerable<Mentor> Mentors { get; set; }
    public Dictionary<List<Guid>, string> MentorMentees { get; set; }

    public string FilterType { get; set; }
    public string FilterRequest { get; set; }
}