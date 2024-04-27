using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, AppUserRole,
    IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>

{
    public DbSet<DocumentSample> DocumentSamples { get; set; } = default!;
    public DbSet<DoucmentSigningTime> DoucmentSigningTimes { get; set; } = default!;

    public DbSet<Intern> Interns { get; set; } = default!;
    public DbSet<InternMentorship> InternMentorships { get; set; } = default!;
    public DbSet<InternSupervisor> InternSupervisors { get; set; } = default!;
    public DbSet<InternMentorshipDocument> InternMentorshipDocuments { get; set; } = default!;
    public DbSet<InternMentorshipUntilDate> InternMentorshipUntilDates { get; set; } = default!;

    public DbSet<Employee> Employees { get; set; } = default!;
    public DbSet<EmployeeMentorship> EmployeeMentorships { get; set; } = default!;
    public DbSet<FactorySupervisor> FactorySupervisors { get; set; } = default!;
    public DbSet<EmployeeMentorshipDocument> EmployeeMentorshipDocuments { get; set; } = default!;
    public DbSet<EmployeeMentorshipUntilDate> EmployeeMentorshipUntilDates { get; set; } = default!;

    public DbSet<Mentor> Mentors { get; set; } = default!;
    public DbSet<MenteeSickLeave> MenteeSickLeaves { get; set; } = default!;
    public DbSet<SickLeaveType> SickLeaveTypes { get; set; } = default!;
    
    public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;


    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}

//
    // public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    // {
    //     foreach (var entity in ChangeTracker.Entries().Where(e => e.State != EntityState.Deleted))
    //     {
    //         foreach (var prop in entity
    //                      .Properties
    //                      .Where(x => x.Metadata.ClrType == typeof(DateTime)))
    //         {
    //             Console.WriteLine(prop);
    //             prop.CurrentValue = ((DateTime) prop.CurrentValue).ToUniversalTime();
    //         }
    //     }
    //
    //     return base.SaveChangesAsync(cancellationToken);
    // }
