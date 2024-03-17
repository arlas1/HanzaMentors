using Base.Domain;

namespace App.Domain;

public class SickLeaveType : BaseEntityId
{
    public string? Type { get; set; }
}