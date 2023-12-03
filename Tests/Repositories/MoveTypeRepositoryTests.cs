using System.Collections.Generic;
using System.Threading.Tasks;
using CrealutionServer.Models.Dtos.MoveTypes;
using CrealutionServer.Infrastructure.Database;
using CrealutionServer.Infrastructure.Exceptions;
using CrealutionServer.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CrealutionServer.Tests.Repositories
{
    public class MoveTypeRepositoryTests
    {
        [Fact]
        public async Task GetAll_ShouldReturnAllEntities()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CrealutionDb>()
               .UseInMemoryDatabase("fakeDbGetAll")
               .Options;
            var entities = new List<MoveTypeCreateDto>
            {
                new MoveTypeCreateDto
                {
                    Name = "name_1" 
                },
                new MoveTypeCreateDto
                {
                    Name = "name_2"
                }
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new MoveTypeRepository(
                    TestMapper.Mapper,
                    TestCacheService.GetTestCacheService(),
                    fakeDb);

                await repository.Create(entities[0]);
                await repository.Create(entities[1]);

                var result = await repository.GetAll();

                // Assert
                Assert.NotNull(result);
                Assert.Equal(
                    entities.Count,
                    result.MoveTypes.Count);
            }
        }

        [Fact]
        public async Task GetById_WithExistingId_ShouldReturnEntity()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CrealutionDb>()
              .UseInMemoryDatabase("fakeDbGetById")
              .Options;
            var entity = new MoveTypeCreateDto
            { 
                Name = "name_1" 
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new MoveTypeRepository(
                    TestMapper.Mapper,
                    TestCacheService.GetTestCacheService(),
                    fakeDb);
                var result = await repository.Create(entity);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(
                    entity.Name,
                    result.Name);
            }
        }

        [Fact]
        public async Task GetById_WithNonExistingId_ShouldThrowEntityNotFoundException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CrealutionDb>()
               .UseInMemoryDatabase("fakeDbGetByIdWithNonExistingId")
               .Options;

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new MoveTypeRepository(
                    TestMapper.Mapper,
                    TestCacheService.GetTestCacheService(),
                    fakeDb);

                await repository.Create(new MoveTypeCreateDto 
                { 
                    Name = "name_1" 
                });

                // Assert
                await Assert.ThrowsAsync<CrealutionEntityNotFound>(() => repository.GetById(999));
            }
        }

        [Fact]
        public async Task Create_ShouldCreateEntity()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CrealutionDb>()
                .UseInMemoryDatabase("fakeDbCreate")
                .Options;         
            var createDto = new MoveTypeCreateDto
            {
                Name = "New move type"
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new MoveTypeRepository(
                    TestMapper.Mapper,
                    TestCacheService.GetTestCacheService(),
                    fakeDb);
                var result = await repository.Create(createDto);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(
                    createDto.Name,
                    result.Name);
            }
        }

        [Fact]
        public async Task Create_DuplicateName_ShouldThrowCrealutionEntityValidateException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CrealutionDb>()
                .UseInMemoryDatabase("fakeDbCreateDuplicateName")
                .Options;
            var createDto = new MoveTypeCreateDto
            {
                Name = "New move type"
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new MoveTypeRepository(
                    TestMapper.Mapper,
                    TestCacheService.GetTestCacheService(),
                    fakeDb);
                var result = await repository.Create(createDto);

                // Assert
                await Assert.ThrowsAsync<CrealutionEntityValidateException>(() => repository.Create(createDto));
            }
        }

        [Fact]
        public async Task Update_DuplicateName_ShouldThrowCrealutionEntityValidateException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CrealutionDb>()
                .UseInMemoryDatabase("fakeDbUpdateDuplicateName")
                .Options;
            var createDto1 = new MoveTypeCreateDto
            {
                Name = "Name move type 1"
            };
            var createDto2 = new MoveTypeCreateDto
            {
                Name = "Name move type 2"
            };
            var updateDto = new MoveTypeUpdateDto
            {
                Id = 1, 
                Name = "Name move type 2"
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new MoveTypeRepository(
                    TestMapper.Mapper,
                    TestCacheService.GetTestCacheService(),
                    fakeDb);

                await repository.Create(createDto1);
                await repository.Create(createDto2);

                // Assert
                await Assert.ThrowsAsync<CrealutionEntityValidateException>(() => repository.Update(updateDto));
            }
        }

        [Fact]
        public async Task Update_ShouldUpdateEntity()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CrealutionDb>()
                .UseInMemoryDatabase("fakeDbUpdate")
                .Options;
            var createDto = new MoveTypeCreateDto
            {
                Name = "Name move type"
            };
            var updateDto = new MoveTypeUpdateDto
            {
                Id = 1,
                Name = "New name move type"
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new MoveTypeRepository(
                    TestMapper.Mapper,
                    TestCacheService.GetTestCacheService(),
                    fakeDb);

                await repository.Create(createDto);

                var result = await repository.Update(updateDto);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(
                    updateDto.Name,
                    result.Name);
            }
        }

        [Fact]
        public async Task Delete_ShouldDeleteEntity()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CrealutionDb>()
                .UseInMemoryDatabase("fakeDbDelete")
                .Options;
            var createDto = new MoveTypeCreateDto
            {
                Name = "Name move type"
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new MoveTypeRepository(
                    TestMapper.Mapper,
                    TestCacheService.GetTestCacheService(),
                    fakeDb);

                await repository.Create(createDto);
                await repository.Delete(1);

                // Assert
                await Assert.ThrowsAsync<CrealutionEntityNotFound>(() => repository.GetById(1));
            }
        }
    }
}