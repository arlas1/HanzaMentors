using Base.Domain.Contracts;

namespace App.BLL.DTO;

public class InternSupervisor : IBaseEntityId
{
    public Guid Id { get; set; }

    public string? FullName { get; set; }
    public int Type { get; set; }
}