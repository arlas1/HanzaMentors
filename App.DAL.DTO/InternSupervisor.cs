using Base.Domain.Contracts;

namespace App.DAL.DTO;

public class InternSupervisor : IBaseEntityId
{
    public Guid Id { get; set; }

    public string? FullName { get; set; }
}