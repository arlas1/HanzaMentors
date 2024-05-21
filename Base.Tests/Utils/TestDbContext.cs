using App.Domain;
using Microsoft.EntityFrameworkCore;

namespace Base.Tests.Utils;

public class TestDbContext : DbContext
{
    public DbSet<TestEntity> TestEntities { get; set; }

    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
    {
        
    }
}