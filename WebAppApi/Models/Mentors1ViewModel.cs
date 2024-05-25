using App.BLL.DTO;

namespace WebAppApi.Models;

public class Mentors1ViewModel 
{ 
    public IEnumerable<Mentor> Mentors { get; set; }
    public Dictionary<Guid, List<string>> MentorMentees { get; set; }
    public string FilterType { get; set; }
    public string FilterRequest { get; set; }
}