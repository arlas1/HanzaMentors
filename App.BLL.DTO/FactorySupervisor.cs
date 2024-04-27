using Base.Domain.Contracts;

namespace App.BLL.DTO;

public class FactorySupervisor : IBaseEntityId
{
    public Guid Id { get; set; }
    
    public string? FullName { get; set; }
}