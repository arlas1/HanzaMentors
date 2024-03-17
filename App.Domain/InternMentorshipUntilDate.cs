using Base.Domain;

namespace App.Domain;

public class InternMentorshipUntilDate : BaseEntityId
{
    public Guid InternMentorshipId { get; set; }
    public InternMentorship? InternMentorship { get; set; }
    
    public DateOnly UntilDate { get; set; }
}