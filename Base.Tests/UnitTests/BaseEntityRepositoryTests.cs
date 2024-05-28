using Base.DAL.EF;
using Base.Tests.Utils;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Base.Tests.UnitTests;

public class BaseEntityRepositoryTests
    {
        private DbContextOptions<TestDbContext> _dbContextOptions;

        public BaseEntityRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public void DerivedRepository_Add_ShouldAddEntity()
        {
            using var context = new TestDbContext(_dbContextOptions);
            var mapper = new TestDalMapper();
            var repository = new BaseEntityRepository<TestEntity, TestDalEntity, TestDbContext>(context, mapper);

            var dalEntity = new TestDalEntity { Id = Guid.NewGuid() };

            var result = repository.Add(dalEntity);
            context.SaveChanges();

            result.Should().NotBeNull();
            context.TestEntities.Should().ContainSingle(e => e.Id == dalEntity.Id);
        }
        
        [Fact]
        public void Add_ShouldAddEntity()
        {
            // Arrange
            using var context = new TestDbContext(_dbContextOptions);
            var mapper = new TestDalMapper();
            var repository = new BaseEntityRepository<Guid, TestEntity, TestDalEntity, TestDbContext>(context, mapper);

            // Act
            var dalEntity = new TestDalEntity { Id = Guid.NewGuid() };
            var result = repository.Add(dalEntity);
            context.SaveChanges();

            // Assert
            result.Should().NotBeNull();
            context.TestEntities.Should().ContainSingle(e => e.Id == dalEntity.Id);
        }


        [Fact]
        public void Update_ShouldUpdateEntity()
        {
            var entity = new TestEntity { Id = Guid.NewGuid() };
            using (var context = new TestDbContext(_dbContextOptions))
            {
                context.TestEntities.Add(entity);
                context.SaveChanges();
            }

            using (var context = new TestDbContext(_dbContextOptions))
            {
                var mapper = new TestDalMapper();
                var repository = new BaseEntityRepository<Guid, TestEntity, TestDalEntity, TestDbContext>(context, mapper);

                var dalEntity = new TestDalEntity { Id = entity.Id };
                var result = repository.Update(dalEntity);

                result.Should().NotBeNull();
                context.TestEntities.Should().ContainSingle(e => e.Id == dalEntity.Id);
            }
        }


        [Fact]
        public void Remove_ShouldRemoveEntity()
        {
            Guid entityId;

            using (var context = new TestDbContext(_dbContextOptions))
            {
                var entity = new TestEntity { Id = Guid.NewGuid() };
                entityId = entity.Id;
                context.TestEntities.Add(entity);
                context.SaveChanges();
            }

            using (var context = new TestDbContext(_dbContextOptions))
            {
                var mapper = new TestDalMapper();
                var repository = new BaseEntityRepository<Guid, TestEntity, TestDalEntity, TestDbContext>(context, mapper);

                var dalEntity = new TestDalEntity { Id = entityId };

                var result = repository.Remove(dalEntity);

                result.Should().Be(1);
                context.TestEntities.Should().BeEmpty();
            }
        }


        [Fact]
        public void GetAll_ShouldReturnAllEntities()
        {
            using var context = new TestDbContext(_dbContextOptions);
            var mapper = new TestDalMapper();
            var repository = new BaseEntityRepository<Guid, TestEntity, TestDalEntity, TestDbContext>(context, mapper);

            context.TestEntities.Add(new TestEntity { Id = Guid.NewGuid() });
            context.TestEntities.Add(new TestEntity { Id = Guid.NewGuid() });
            context.SaveChanges();

            var result = repository.GetAll();

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllEntities()
        {
            using var context = new TestDbContext(_dbContextOptions);
            var mapper = new TestDalMapper();
            var repository = new BaseEntityRepository<Guid, TestEntity, TestDalEntity, TestDbContext>(context, mapper);

            context.TestEntities.Add(new TestEntity { Id = Guid.NewGuid() });
            context.TestEntities.Add(new TestEntity { Id = Guid.NewGuid() });
            await context.SaveChangesAsync();

            var result = await repository.GetAllAsync();

            result.Should().HaveCount(2);
        }

        [Fact]
        public void FirstOrDefault_ShouldReturnEntity()
        {
            using var context = new TestDbContext(_dbContextOptions);
            var mapper = new TestDalMapper();
            var repository = new BaseEntityRepository<Guid, TestEntity, TestDalEntity, TestDbContext>(context, mapper);

            var entity = new TestEntity { Id = Guid.NewGuid() };
            context.TestEntities.Add(entity);
            context.SaveChanges();

            var result = repository.FirstOrDefault(entity.Id);

            result!.Should().NotBeNull();
            result!.Id.Should().Be(entity.Id);
        }

        [Fact]
        public async Task FirstOrDefaultAsync_ShouldReturnEntity()
        {
            using var context = new TestDbContext(_dbContextOptions);
            var mapper = new TestDalMapper();
            var repository = new BaseEntityRepository<Guid, TestEntity, TestDalEntity, TestDbContext>(context, mapper);

            var entity = new TestEntity { Id = Guid.NewGuid() };
            context.TestEntities.Add(entity);
            await context.SaveChangesAsync();

            var result = await repository.FirstOrDefaultAsync(entity.Id);

            result!.Should().NotBeNull();
            result!.Id.Should().Be(entity.Id);
        }
    }