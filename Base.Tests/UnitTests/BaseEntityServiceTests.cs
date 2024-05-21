using Base.BLL.Contracts;
using Base.DAL.Contracts;
using Base.Tests.Utils;
using FluentAssertions;
using Moq;

namespace Base.Tests.UnitTests;

public class BaseEntityServiceTests
{
    private readonly Mock<IBaseUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IBaseEntityRepository<TestDalEntity, Guid>> _repositoryMock;
    private readonly Mock<IBLLMapper<TestDalEntity, TestEntity>> _mapperMock;
    private readonly TestEntityService _service;

    public BaseEntityServiceTests()
    {
        _unitOfWorkMock = new Mock<IBaseUnitOfWork>();
        _repositoryMock = new Mock<IBaseEntityRepository<TestDalEntity, Guid>>();
        _mapperMock = new Mock<IBLLMapper<TestDalEntity, TestEntity>>();
        _service = new TestEntityService(_unitOfWorkMock.Object, _repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public void Add_ShouldAddEntity()
    {
        // Arrange
        var entity = new TestEntity { Id = Guid.NewGuid() };
        var dalEntity = new TestDalEntity { Id = entity.Id };
        _mapperMock.Setup(m => m.Map(entity)).Returns(dalEntity);
        _mapperMock.Setup(m => m.Map(dalEntity)).Returns(entity);
        _repositoryMock.Setup(r => r.Add(dalEntity)).Returns(dalEntity);

        // Act
        var result = _service.Add(entity);

        // Assert
        result.Should().Be(entity);
        _repositoryMock.Verify(r => r.Add(dalEntity), Times.Once);
    }

    [Fact]
    public void Update_ShouldUpdateEntity()
    {
        // Arrange
        var entity = new TestEntity { Id = Guid.NewGuid() };
        var dalEntity = new TestDalEntity { Id = entity.Id };
        _mapperMock.Setup(m => m.Map(entity)).Returns(dalEntity);
        _mapperMock.Setup(m => m.Map(dalEntity)).Returns(entity);
        _repositoryMock.Setup(r => r.Update(dalEntity)).Returns(dalEntity);

        // Act
        var result = _service.Update(entity);

        // Assert
        result.Should().Be(entity);
        _repositoryMock.Verify(r => r.Update(dalEntity), Times.Once);
    }

    [Fact]
    public void Remove_ShouldRemoveEntity()
    {
        // Arrange
        var entity = new TestEntity { Id = Guid.NewGuid() };
        var dalEntity = new TestDalEntity { Id = entity.Id };
        _mapperMock.Setup(m => m.Map(entity)).Returns(dalEntity);
        _repositoryMock.Setup(r => r.Remove(dalEntity, It.IsAny<Guid>())).Returns(1);

        // Act
        var result = _service.Remove(entity);

        // Assert
        result.Should().Be(1);
        _repositoryMock.Verify(r => r.Remove(dalEntity, It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public void FirstOrDefault_ShouldReturnEntity()
    {
        // Arrange
        var entity = new TestEntity { Id = Guid.NewGuid() };
        var dalEntity = new TestDalEntity { Id = entity.Id };
        _repositoryMock.Setup(r => r.FirstOrDefault(entity.Id, It.IsAny<Guid>(), true)).Returns(dalEntity);
        _mapperMock.Setup(m => m.Map(dalEntity)).Returns(entity);

        // Act
        var result = _service.FirstOrDefault(entity.Id);

        // Assert
        result.Should().Be(entity);
        _repositoryMock.Verify(r => r.FirstOrDefault(entity.Id, It.IsAny<Guid>(), true), Times.Once);
    }

[Fact]
    public async Task FirstOrDefaultAsync_ShouldReturnEntityAsync()
    {
        // Arrange
        var entity = new TestEntity { Id = Guid.NewGuid() };
        var dalEntity = new TestDalEntity { Id = entity.Id };
        _repositoryMock.Setup(r => r.FirstOrDefaultAsync(entity.Id, It.IsAny<Guid>(), true)).ReturnsAsync(dalEntity);
        _mapperMock.Setup(m => m.Map(dalEntity)).Returns(entity);

        // Act
        var result = await _service.FirstOrDefaultAsync(entity.Id);

        // Assert
        result.Should().Be(entity);
        _repositoryMock.Verify(r => r.FirstOrDefaultAsync(entity.Id, It.IsAny<Guid>(), true), Times.Once);
    }

    [Fact]
    public void GetAll_ShouldReturnAllEntities()
    {
        // Arrange
        var entities = new List<TestEntity>
        {
            new TestEntity { Id = Guid.NewGuid() },
            new TestEntity { Id = Guid.NewGuid() }
        };
        var dalEntities = entities.Select(e => new TestDalEntity { Id = e.Id }).ToList();
        _repositoryMock.Setup(r => r.GetAll(It.IsAny<Guid>(), true)).Returns(dalEntities);
        _mapperMock.Setup(m => m.Map(It.IsAny<TestDalEntity>())).Returns((TestDalEntity dalEntity) => entities.First(e => e.Id == dalEntity.Id));

        // Act
        var result = _service.GetAll();

        // Assert
        result.Should().BeEquivalentTo(entities);
        _repositoryMock.Verify(r => r.GetAll(It.IsAny<Guid>(), true), Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEntitiesAsync()
    {
        // Arrange
        var entities = new List<TestEntity>
        {
            new TestEntity { Id = Guid.NewGuid() },
            new TestEntity { Id = Guid.NewGuid() }
        };
        var dalEntities = entities.Select(e => new TestDalEntity { Id = e.Id }).ToList();
        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<Guid>(), true)).ReturnsAsync(dalEntities);
        _mapperMock.Setup(m => m.Map(It.IsAny<TestDalEntity>())).Returns((TestDalEntity dalEntity) => entities.First(e => e.Id == dalEntity.Id));

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(entities);
        _repositoryMock.Verify(r => r.GetAllAsync(It.IsAny<Guid>(), true), Times.Once);
    }
}