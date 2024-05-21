using App.BLL.Services;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using AutoMapper;
using Moq;
using FluentAssertions;
using DomainEntity = App.Domain;
using DALDTO = App.DAL.DTO;
using BLLDTO = App.BLL.DTO;

public class EmployeeServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly IMapper _mapper;
    private readonly EmployeeService _employeeService;

    public EmployeeServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _employeeRepositoryMock = new Mock<IEmployeeRepository>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<DomainEntity.Employee, DALDTO.Employee>().ReverseMap();
            cfg.CreateMap<DALDTO.Employee, BLLDTO.Employee>().ReverseMap();
        });

        _mapper = config.CreateMapper();

        _employeeService = new EmployeeService(_unitOfWorkMock.Object, _employeeRepositoryMock.Object, _mapper);
    }

    [Fact]
    public void Add_ShouldAddEmployee()
    {
        // Arrange
        var bllEmployee = new BLLDTO.Employee { Id = Guid.NewGuid(), FirstName = "test", LastName = "test", Profession = "test",
                                                EmployeeType = "FullTime", Email = "john.doe@example.com" };
        var dalEmployee = _mapper.Map<DALDTO.Employee>(bllEmployee);

        _employeeRepositoryMock.Setup(r => r.Add(It.IsAny<DALDTO.Employee>())).Returns(dalEmployee);

        // Act
        var result = _employeeService.Add(bllEmployee);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(bllEmployee);
        _employeeRepositoryMock.Verify(r => r.Add(It.Is<DALDTO.Employee>(e => e.Id == bllEmployee.Id)), Times.Once);
    }

    [Fact]
    public void Update_ShouldUpdateEmployee()
    {
        // Arrange
        var bllEmployee = new BLLDTO.Employee { Id = Guid.NewGuid(), FirstName = "test", LastName = "test", Profession = "test",
                                                EmployeeType = "FullTime", Email = "john.doe@example.com" };
        var dalEmployee = _mapper.Map<DALDTO.Employee>(bllEmployee);

        _employeeRepositoryMock.Setup(r => r.Update(It.IsAny<DALDTO.Employee>())).Returns(dalEmployee);

        // Act
        var result = _employeeService.Update(bllEmployee);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(bllEmployee);
        _employeeRepositoryMock.Verify(r => r.Update(It.Is<DALDTO.Employee>(e => e.Id == bllEmployee.Id)), Times.Once);
    }

    [Fact]
    public void Remove_ShouldRemoveEmployee()
    {
        // Arrange
        var bllEmployee = new BLLDTO.Employee { Id = Guid.NewGuid() };
    
        _employeeRepositoryMock.Setup(r => r.Remove(It.IsAny<DALDTO.Employee>(), Guid.Empty)).Returns(1);
    
        // Act
        var result = _employeeService.Remove(bllEmployee);
    
        // Assert
        result.Should().Be(1);
        _employeeRepositoryMock.Verify(r => r.Remove(It.Is<DALDTO.Employee>(e => e.Id == bllEmployee.Id), Guid.Empty), Times.Once);
    }
    
    [Fact]
    public void FirstOrDefault_ShouldReturnEmployee()
    {
        // Arrange
        var bllEmployee = new BLLDTO.Employee { Id = Guid.NewGuid(), FirstName = "test", LastName = "test", Profession = "test",
                                                EmployeeType = "FullTime", Email = "john.doe@example.com" };
        var dalEmployee = _mapper.Map<DALDTO.Employee>(bllEmployee);
    
        _employeeRepositoryMock.Setup(r => r.FirstOrDefault(bllEmployee.Id, Guid.Empty, true)).Returns(dalEmployee);
    
        // Act
        var result = _employeeService.FirstOrDefault(bllEmployee.Id);
    
        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(bllEmployee);
        _employeeRepositoryMock.Verify(r => r.FirstOrDefault(bllEmployee.Id, Guid.Empty, true), Times.Once);
    }
    
    [Fact]
    public async Task FirstOrDefaultAsync_ShouldReturnEmployeeAsync()
    {
        // Arrange
        var bllEmployee = new BLLDTO.Employee { Id = Guid.NewGuid(), FirstName = "test", LastName = "test", Profession = "test",
                                                EmployeeType = "FullTime", Email = "john.doe@example.com" };
        var dalEmployee = _mapper.Map<DALDTO.Employee>(bllEmployee);
    
        _employeeRepositoryMock.Setup(r => r.FirstOrDefaultAsync(bllEmployee.Id, Guid.Empty, true)).ReturnsAsync(dalEmployee);
    
        // Act
        var result = await _employeeService.FirstOrDefaultAsync(bllEmployee.Id);
    
        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(bllEmployee);
        _employeeRepositoryMock.Verify(r => r.FirstOrDefaultAsync(bllEmployee.Id, Guid.Empty, true), Times.Once);
    }
    
    [Fact]
    public void GetAll_ShouldReturnAllEmployees()
    {
        // Arrange
        var employees = new List<BLLDTO.Employee>
        {
            new BLLDTO.Employee { Id = Guid.NewGuid(),FirstName = "test", LastName = "test", Profession = "test",
                EmployeeType = "FullTime", Email = "john.doe@example.com" },
            new BLLDTO.Employee { Id = Guid.NewGuid(),FirstName = "test1", LastName = "test1", Profession = "test1",
                EmployeeType = "FullTime1", Email = "john.doe1@example.com" }
        };
        
        var dalEmployees = employees.Select(e => _mapper.Map<DALDTO.Employee>(e)).ToList();
    
        _employeeRepositoryMock.Setup(r => r.GetAll(Guid.Empty, true)).Returns(dalEmployees);
    
        // Act
        var result = _employeeService.GetAll();
    
        // Assert
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(employees);
        _employeeRepositoryMock.Verify(r => r.GetAll(Guid.Empty, true), Times.Once);
    }
    
    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEmployeesAsync()
    {
        // Arrange
        var employees = new List<BLLDTO.Employee>
        {
            new BLLDTO.Employee { Id = Guid.NewGuid(),FirstName = "test", LastName = "test", Profession = "test",
                EmployeeType = "FullTime", Email = "john.doe@example.com" },
            new BLLDTO.Employee { Id = Guid.NewGuid(),FirstName = "test1", LastName = "test1", Profession = "test1",
                EmployeeType = "FullTime1", Email = "john.doe1@example.com" }
        };
    
        var dalEmployees = employees.Select(e => _mapper.Map<DALDTO.Employee>(e)).ToList();
    
        _employeeRepositoryMock.Setup(r => r.GetAllAsync(Guid.Empty, true)).ReturnsAsync(dalEmployees);
    
        // Act
        var result = await _employeeService.GetAllAsync();
    
        // Assert
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(employees);
        _employeeRepositoryMock.Verify(r => r.GetAllAsync(Guid.Empty, true), Times.Once);
    }
}
