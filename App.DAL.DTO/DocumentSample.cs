using Base.Domain.Contracts;

namespace App.DAL.DTO;

public class DocumentSample : IBaseEntityId
{
    public Guid Id { get; set; }
    
    public string? Title { get; set; }
    public string? Base64Code { get; set; }
}