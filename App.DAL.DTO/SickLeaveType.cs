using Base.Domain.Contracts;

namespace App.DAL.DTO;

public class SickLeaveType : IBaseEntityId
{
    public Guid Id { get; set; }
    
    public string? Type { get; set; }
}