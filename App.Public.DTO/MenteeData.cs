using App.BLL.DTO;

namespace App.Public.DTO;

public class MenteeData
{
    public Dictionary<InternsMentor, string>? InternsMentors { get; set; }
    public Dictionary<EmployeesMentor, string>? EmployeesMentors { get; set; }
    
    public Dictionary<InternMentorshipDocument, List<DoucmentSigningTime>>? InternDocuments { get; set; }
    public Dictionary<EmployeeMentorshipDocument, List<DoucmentSigningTime>>? EmployeeDocuments { get; set; }
}