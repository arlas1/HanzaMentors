using App.BLL.DTO;

namespace WebApp.Models;

public class DocumentsViewModel
{
    public Guid DocumentId { get; set; }
    public string Title { get; set; }
    public IFormFile  File { get; set; }
    public IEnumerable<DocumentSample> DocumentSamples { get; set; }
    public IEnumerable<InternMentorshipDocument> InternMentorshipDocuments { get; set; }
    public IEnumerable<EmployeeMentorshipDocument> EmployeeMentorshipDocuments { get; set; }

}