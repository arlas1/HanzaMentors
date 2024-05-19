using Base.Domain.Contracts;

namespace App.Public.DTO.v1;

public class DocumentSample : IBaseEntityId
{
    public Guid Id { get; set; }
    
    public string? Title { get; set; }
    public string? Base64Code { get; set; }
}