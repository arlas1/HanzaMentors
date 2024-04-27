using Base.Domain.Contracts;

namespace App.BLL.DTO;

public class SickLeaveType : IBaseEntityId
{
    public Guid Id { get; set; }
    
    public string? Type { get; set; }
}