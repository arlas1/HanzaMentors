using Base.Domain.Contracts;

namespace App.Public.DTO.v1;

public class FactorySupervisor : IBaseEntityId
{
    public Guid Id { get; set; }
    
    public string? FullName { get; set; }
}