using App.BLL.DTO;

namespace WebApp.Models;

public class MenteeViewModel
{
    public Dictionary<Guid, string?> DocumentSamples { get; set; }
    public List<string> SigningTimes { get; set; }
    
    public IEnumerable<Mentor> Mentors { get; set; }
    public IEnumerable<Employee> EmployeeMentees { get; set; }
    public IEnumerable<Intern> InternMentees { get; set; }
    public Dictionary<Guid, List<Guid>> MenteesMentor { get; set; }
    
    
    public IEnumerable<InternMentorship> InternMentorships { get; set; }
    public IEnumerable<EmployeeMentorship> EmployeeMentorships { get; set; }

    
    public Guid MenteeId  { get; set; }
    public Guid SelectedMentorId  { get; set; }
    public string FilterType { get; set; }
    public string FilterRequest { get; set; }
}