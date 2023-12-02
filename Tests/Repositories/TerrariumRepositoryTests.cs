using System.Collections.Generic;
using System.Threading.Tasks;
using CrealutionServer.Models.Dtos.Terrariums;
using CrealutionServer.Infrastructure.Database;
using CrealutionServer.Infrastructure.Exceptions;
using CrealutionServer.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using CrealutionServer.Models.Dtos.Accounts;
using CrealutionServer.Configurations.Authentication;

namespace CrealutionServer.Tests.Repositories
{
    public class TerrariumRepositoryTests
    {
        [Fact]
        public async Task GetAll_ShouldReturnAllEntities()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CrealutionDb>()
               .UseInMemoryDatabase("fakeDbGetAll")
               .Options;
            var entities = new List<TerrariumCreateDto>
            {
                new TerrariumCreateDto
                {
                    Name = "name_1",
                    AccountId = 1
                },
                new TerrariumCreateDto
                {
                    Name = "name_2",
                    AccountId = 1
                }
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new TerrariumRepository(
                    TestMapper.Mapper,
                    fakeDb);

                await AddTestAccount(fakeDb);
                await repository.Create(entities[0]);
                await repository.Create(entities[1]);

                var result = await repository.GetAll();

                // Assert
                Assert.NotNull(result);
                Assert.Equal(
                    entities.Count,
                    result.Terrariums.Count);
            }
        }

        [Fact]
        public async Task GetById_WithExistingId_ShouldReturnEntity()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CrealutionDb>()
              .UseInMemoryDatabase("fakeDbGetById")
              .Options;
            var entity = new TerrariumCreateDto
            { 
                Name = "name_1",
                AccountId = 1
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new TerrariumRepository(
                    TestMapper.Mapper,
                    fakeDb);

                await AddTestAccount(fakeDb);

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
                var repository = new TerrariumRepository(
                    TestMapper.Mapper,
                    fakeDb);

                await AddTestAccount(fakeDb);
                await repository.Create(new TerrariumCreateDto 
                { 
                    Name = "name_1",
                    AccountId = 1
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
            var createDto = new TerrariumCreateDto
            {
                Name = "New terrarium",
                AccountId = 1
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new TerrariumRepository(
                    TestMapper.Mapper,
                    fakeDb);

                await AddTestAccount(fakeDb);

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
            var createDto = new TerrariumCreateDto
            {
                Name = "New terrarium",
                AccountId = 1
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new TerrariumRepository(
                    TestMapper.Mapper,
                    fakeDb);

                await AddTestAccount(fakeDb);

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
            var createDto1 = new TerrariumCreateDto
            {
                Name = "Name terrarium 1",
                AccountId = 1
            };
            var createDto2 = new TerrariumCreateDto
            {
                Name = "Name terrarium 2",
                AccountId = 1
            };
            var updateDto = new TerrariumUpdateDto
            {
                Id = 1, 
                Name = "Name terrarium 2",
                AccountId = 1
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new TerrariumRepository(
                    TestMapper.Mapper,
                    fakeDb);

                await AddTestAccount(fakeDb);
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
            var createDto = new TerrariumCreateDto
            {
                Name = "Name terrarium",
                AccountId = 1
            };
            var updateDto = new TerrariumUpdateDto
            {
                Id = 1,
                Name = "New name terrarium",
                AccountId = 1
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new TerrariumRepository(
                    TestMapper.Mapper,
                    fakeDb);

                await AddTestAccount(fakeDb);
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
            var createDto = new TerrariumCreateDto
            {
                Name = "Name terrarium",
                AccountId = 1
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new TerrariumRepository(
                    TestMapper.Mapper,
                    fakeDb);

                await AddTestAccount(fakeDb);
                await repository.Create(createDto);
                await repository.Delete(1);

                // Assert
                await Assert.ThrowsAsync<CrealutionEntityNotFound>(() => repository.GetById(1));
            }
        }

        private async Task AddTestAccount(CrealutionDb db)
        {
            var authenticationOptions = new AuthenticationOptions(
                issuer: "TestIssuer",
                audience: "TestAudience",
                secretKey: "SecretKey82746791826738191283236929886364910076427",
                lifeTime: 1);
            var repository = new AccountRepository(
                    TestMapper.Mapper,
                    authenticationOptions,
                    db);

            await repository.Create(new AccountCreateDto
            {
                Name = "name",
                DisplayName = "display_name",
                Password = "pass123",
                RoleIds = new List<long>()
            });
        }
    }
}