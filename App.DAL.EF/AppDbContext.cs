using System.Text.Json;
using App.Domain;
using App.Domain.Identity;
using Base.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
    public DbSet<InternsMentor> InternsMentors { get; set; } = default!;

    public DbSet<Employee> Employees { get; set; } = default!;
    public DbSet<EmployeeMentorship> EmployeeMentorships { get; set; } = default!;
    public DbSet<FactorySupervisor> FactorySupervisors { get; set; } = default!;
    public DbSet<EmployeeMentorshipDocument> EmployeeMentorshipDocuments { get; set; } = default!;
    public DbSet<EmployeesMentor> EmployeesMentors { get; set; } = default!;

    public DbSet<Mentor> Mentors { get; set; } = default!;
    public DbSet<MenteeSickLeave> MenteeSickLeaves { get; set; } = default!;
    
    public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply the LangStr converter to properties of type LangStr
        var langStrConverter = new LangStrConverter();

        modelBuilder.Entity<Employee>()
            .Property(e => e.FirstName)
            .HasConversion(langStrConverter!);

        modelBuilder.Entity<Employee>()
            .Property(e => e.LastName)
            .HasConversion(langStrConverter!);
        
        modelBuilder.Entity<Employee>()
            .Property(e => e.Profession)
            .HasConversion(langStrConverter);
        
        modelBuilder.Entity<Intern>()
            .Property(e => e.FirstName)
            .HasConversion(langStrConverter!);

        modelBuilder.Entity<Intern>()
            .Property(e => e.LastName)
            .HasConversion(langStrConverter!);
        
        modelBuilder.Entity<Intern>()
            .Property(e => e.StudyProgram)
            .HasConversion(langStrConverter!);
    }
}
public class LangStrConverter : ValueConverter<LangStr, string>
{
    public LangStrConverter() 
        : base(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            v => JsonSerializer.Deserialize<LangStr>(v, (JsonSerializerOptions)null))
    {
    }
}