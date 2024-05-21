using App.DAL.EF;
using App.DAL.EF.Repositories;
using AutoMapper;
using Base.Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using DomainEntity = App.Domain;
using DALDTO = App.DAL.DTO;

namespace Base.Tests.UnitTests;

public class EmployeeRepositoryTests
{
    private readonly DbContextOptions<AppDbContext> _dbContextOptions;
    private IMapper Mapper { get; }

    public EmployeeRepositoryTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        Mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<DomainEntity.Employee, DALDTO.Employee>().ReverseMap();
        }).CreateMapper();
    }
    
    [Fact]
    public void Add_ShouldAddEmployee()
    {
        using var context = new AppDbContext(_dbContextOptions);
        var repository = new EmployeeRepository(context, Mapper);

        var employee = new DALDTO.Employee { Id = Guid.NewGuid(), FirstName = new LangStr("John"), LastName = new LangStr("Doe") };

        var result = repository.Add(employee);
        context.SaveChanges();

        result.Should().NotBeNull();
        context.Employees.Should().ContainSingle(e => e.Id == employee.Id);
    }

    [Fact]
    public void Update_ShouldUpdateEmployee()
    {
        var employee = new DomainEntity.Employee { Id = Guid.NewGuid(), FirstName = new LangStr("John"), LastName = new LangStr("Doe") };
   
        using (var addContext = new AppDbContext(_dbContextOptions))
        {
            addContext.Employees.Add(employee);
            addContext.SaveChanges();
        }

        using (var updateContext = new AppDbContext(_dbContextOptions))
        {
            var repository = new EmployeeRepository(updateContext, Mapper);

            var updatedEmployee = new DALDTO.Employee { Id = employee.Id, FirstName = new LangStr("Jane"), LastName = new LangStr("Smith") };

            var result = repository.Update(updatedEmployee);

            result.Should().NotBeNull();
            updateContext.Employees.Should().ContainSingle(e => e.FirstName.ToString() == updatedEmployee.FirstName.ToString() && e.LastName.ToString() == updatedEmployee.LastName.ToString());
        }
    }


    [Fact]
    public void Remove_ShouldRemoveEmployee()
    {
        var employee = new DomainEntity.Employee { Id = Guid.NewGuid(), FirstName = new LangStr("John"), LastName = new LangStr("Doe") };

        using (var addContext = new AppDbContext(_dbContextOptions))
        {
            addContext.Employees.Add(employee);
            addContext.SaveChanges();
        }

        using (var removeContext = new AppDbContext(_dbContextOptions))
        {
            var repository = new EmployeeRepository(removeContext, Mapper);

            var dalEmployee = new DALDTO.Employee { Id = employee.Id };

            var result = repository.Remove(dalEmployee);

            result.Should().Be(1);
            removeContext.Employees.Should().BeEmpty();
        }
    }



    [Fact]
    public void GetAll_ShouldReturnAllEmployees()
    {
        using var context = new AppDbContext(_dbContextOptions);
        var repository = new EmployeeRepository(context, Mapper);

        var employee1 = new DomainEntity.Employee { Id = Guid.NewGuid(), FirstName = new LangStr("John"), LastName = new LangStr("Doe") };
        var employee2 = new DomainEntity.Employee { Id = Guid.NewGuid(), FirstName = new LangStr("Jane"), LastName = new LangStr("Smith") };
        context.Employees.Add(employee1);
        context.Employees.Add(employee2);
        context.SaveChanges();

        var result = repository.GetAll();

        result.Should().HaveCount(2);
        result.Select(e => e.FirstName.ToString()).Should().Contain(new[] { "John", "Jane" });
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEmployeesAsync()
    {
        using var context = new AppDbContext(_dbContextOptions);
        var repository = new EmployeeRepository(context, Mapper);

        var employee1 = new DomainEntity.Employee { Id = Guid.NewGuid(), FirstName = new LangStr("John"), LastName = new LangStr("Doe") };
        var employee2 = new DomainEntity.Employee { Id = Guid.NewGuid(), FirstName = new LangStr("Jane"), LastName = new LangStr("Smith") };
        context.Employees.Add(employee1);
        context.Employees.Add(employee2);
        await context.SaveChangesAsync();

        var result = await repository.GetAllAsync();

        result.Should().HaveCount(2);
        result.Select(e => e.FirstName.ToString()).Should().Contain(new[] { "John", "Jane" });
    }

    [Fact]
    public void FirstOrDefault_ShouldReturnEmployee()
    {
        using var context = new AppDbContext(_dbContextOptions);
        var repository = new EmployeeRepository(context, Mapper);

        var employee = new DomainEntity.Employee { Id = Guid.NewGuid(), FirstName = new LangStr("John"), LastName = new LangStr("Doe") };
        context.Employees.Add(employee);
        context.SaveChanges();

        var result = repository.FirstOrDefault(employee.Id);

        result.Should().NotBeNull();
        result.FirstName.ToString().Should().Be(employee.FirstName.ToString());
    }

    [Fact]
    public async Task FirstOrDefaultAsync_ShouldReturnEmployeeAsync()
    {
        using var context = new AppDbContext(_dbContextOptions);
        var repository = new EmployeeRepository(context, Mapper);

        var employee = new DomainEntity.Employee { Id = Guid.NewGuid(), FirstName = new LangStr("John"), LastName = new LangStr("Doe") };
        context.Employees.Add(employee);
        await context.SaveChangesAsync();

        var result = await repository.FirstOrDefaultAsync(employee.Id);

        result.Should().NotBeNull();
        result.FirstName.ToString().Should().Be(employee.FirstName.ToString());
    }
}