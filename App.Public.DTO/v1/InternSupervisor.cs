using Base.Domain.Contracts;

namespace App.Public.DTO.v1;

public class InternSupervisor : IBaseEntityId
{
    public Guid Id { get; set; }

    public string? FullName { get; set; }
}