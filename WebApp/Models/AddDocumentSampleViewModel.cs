namespace WebApp.Models;

public class AddDocumentSampleViewModel
{
    public Guid DocumentId { get; set; }
    public string Title { get; set; }
    public IFormFile  File { get; set; }
}