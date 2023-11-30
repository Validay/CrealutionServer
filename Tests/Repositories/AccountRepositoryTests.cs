using System.Collections.Generic;
using System.Threading.Tasks;
using CrealutionServer.Configurations.Authentication;
using CrealutionServer.Infrastructure.Database;
using CrealutionServer.Infrastructure.Exceptions;
using CrealutionServer.Infrastructure.Repositories;
using CrealutionServer.Models.Dtos.Accounts;
using CrealutionServer.Models.Dtos.Roles;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CrealutionServer.Tests.Repositories
{
    public class AccountRepositoryTests
    {
        private readonly AuthenticationOptions _authenticationOptions;

        public AccountRepositoryTests()
        {
            _authenticationOptions = new AuthenticationOptions(
                issuer: "TestIssuer", 
                audience: "TestAudience", 
                secretKey: "SecretKey82746791826738191283236929886364910076427", 
                lifeTime: 1);
        }

        [Fact]
        public async Task Login_WithExistingAccount_ShouldReturnEntityAndToken()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CrealutionDb>()
              .UseInMemoryDatabase("fakeDbLogin")
              .Options;
            var entity = new AccountCreateDto
            {
                Name = "name_1",
                DisplayName = "display_name_1",
                Password = "pass123",
                RoleIds = new List<long>()
            };
            var loginDto = new AccountLoginDto
            {
                Name = "name_1",
                Password = "pass123"
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new AccountRepository(
                    TestMapper.Mapper,
                    _authenticationOptions,
                    fakeDb);

                await repository.Create(entity);

                var result = await repository.Login(loginDto);

                // Assert
                Assert.NotNull(result);
                Assert.NotNull(result.Token);
                Assert.NotEmpty(result.Token);
                Assert.Equal(
                    entity.Name,
                    result.AccountInfo.Name);
            }
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllEntities()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CrealutionDb>()
               .UseInMemoryDatabase("fakeDbGetAll")
               .Options;
            var entities = new List<AccountCreateDto>
            {
                new AccountCreateDto
                {
                    Name = "name_1",
                    DisplayName = "display_name_1",
                    Password = "pass123",
                    RoleIds = new List<long>()
                },
                new AccountCreateDto
                {
                    Name = "name_2",
                    DisplayName = "display_name_2",
                    Password = "pass321",
                    RoleIds = new List<long>()
                }
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new AccountRepository(
                    TestMapper.Mapper,
                    _authenticationOptions,
                    fakeDb);

                await repository.Create(entities[0]);
                await repository.Create(entities[1]);

                var result = await repository.GetAll();

                // Assert
                Assert.NotNull(result);
                Assert.Equal(
                    entities.Count,
                    result.Accounts.Count);
            }
        }

        [Fact]
        public async Task GetById_WithExistingId_ShouldReturnEntity()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CrealutionDb>()
              .UseInMemoryDatabase("fakeDbGetById")
              .Options;
            var entity = new AccountCreateDto
            { 
                Name = "name_1",
                DisplayName = "display_name_1",
                Password = "pass123",
                RoleIds = new List<long>()
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new AccountRepository(
                    TestMapper.Mapper,
                    _authenticationOptions,
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
                var repository = new AccountRepository(
                    TestMapper.Mapper,
                    _authenticationOptions,
                    fakeDb);

                await repository.Create(new AccountCreateDto 
                { 
                    Name = "name_1",
                    DisplayName = "display_name_1",
                    Password = "pass123",
                    RoleIds = new List<long>()
                });

                // Assert
                await Assert.ThrowsAsync<CrealutionEntityNotFound>(() => repository.GetById(999));
            }
        }

        [Fact]
        public async Task Registration_ShouldCreateEntity()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CrealutionDb>()
                .UseInMemoryDatabase("fakeDbRegistration")
                .Options;
            var registrationDto = new AccountRegistrationDto
            {
                Name = "New name",
                DisplayName = "new display_name_1",
                Password = "new pass123"
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new AccountRepository(
                    TestMapper.Mapper,
                    _authenticationOptions,
                    fakeDb);
                var result = await repository.Registration(registrationDto);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(
                    registrationDto.Name,
                    result.Name);
            }
        }

        [Fact]
        public async Task Registration_DuplicateName_ShouldThrowCrealutionEntityValidateException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CrealutionDb>()
                .UseInMemoryDatabase("fakeDbRegistrationDuplicateName")
                .Options;
            var registrationDto = new AccountRegistrationDto
            {
                Name = "New name",
                DisplayName = "new display_name_1",
                Password = "new pass123"
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new AccountRepository(
                    TestMapper.Mapper,
                    _authenticationOptions,
                    fakeDb);
                var result = await repository.Registration(registrationDto);

                // Assert
                await Assert.ThrowsAsync<CrealutionEntityValidateException>(() => repository.Registration(registrationDto));
            }
        }

        [Fact]
        public async Task Create_ShouldCreateEntity()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CrealutionDb>()
                .UseInMemoryDatabase("fakeDbCreate")
                .Options;         
            var createDto = new AccountCreateDto
            {
                Name = "New name",
                DisplayName = "new display_name_1",
                Password = "new pass123",
                RoleIds = new List<long>()
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new AccountRepository(
                    TestMapper.Mapper,
                    _authenticationOptions,
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
            var createDto = new AccountCreateDto
            {
                Name = "New name",
                DisplayName = "new display_name_1",
                Password = "new pass123",
                RoleIds = new List<long>()
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new AccountRepository(
                    TestMapper.Mapper,
                    _authenticationOptions,
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
            var createDto1 = new AccountCreateDto
            {
                Name = "Name 1",
                DisplayName = "display_name_1",
                Password = "pass123",
                RoleIds = new List<long>()
            };
            var createDto2 = new AccountCreateDto
            {
                Name = "Name 2",
                DisplayName = "display_name_2",
                Password = "pass321",
                RoleIds = new List<long>()
            };
            var updateDto = new AccountUpdateDto
            {
                Id = 1, 
                Name = "Name 2",
                DisplayName = "display_name_2",
                Password = "pass321",
                RoleIds = new List<long>()
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new AccountRepository(
                    TestMapper.Mapper,
                    _authenticationOptions,
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
            var createRoleDto = new RoleCreateDto
            {
                Name = "Name role 1"
            };
            var createDto = new AccountCreateDto
            {
                Name = "Name",
                DisplayName = "display_name_1",
                Password = "pass123",
                RoleIds = new List<long>()
            };
            var updateDto = new AccountUpdateDto
            {
                Id = 1,
                Name = "New name",
                DisplayName = "display_name_2",
                Password = "pass1234",
                RoleIds = new List<long>(1)
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new AccountRepository(
                    TestMapper.Mapper,
                    _authenticationOptions,
                    fakeDb);
                var roleRepository = new RoleRepository(
                    TestMapper.Mapper,
                    fakeDb);

                await roleRepository.Create(createRoleDto);
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
        public async Task UpdateInfo_ShouldUpdateEntity()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CrealutionDb>()
                .UseInMemoryDatabase("fakeDbUpdateInfo")
                .Options;
            var createDto = new AccountCreateDto
            {
                Name = "Name",
                DisplayName = "display_name_1",
                Password = "pass123",
                RoleIds = new List<long>()
            };
            var updateInfoDto = new AccountUpdateInfoDto
            {
                Name = "Name",
                DisplayName = "display_name_2",
                Password = "pass1234"
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new AccountRepository(
                    TestMapper.Mapper,
                    _authenticationOptions,
                    fakeDb);
                await repository.Create(createDto);
                var result = await repository.UpdateInfo(updateInfoDto);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(
                    updateInfoDto.Name,
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
            var createDto = new AccountCreateDto
            {
                Name = "Name",
                DisplayName = "display_name_1",
                Password = "pass123",
                RoleIds = new List<long>()
            };

            // Act
            using (var fakeDb = new CrealutionDb(options))
            {
                var repository = new AccountRepository(
                    TestMapper.Mapper,
                    _authenticationOptions,
                    fakeDb);
                await repository.Create(createDto);
                await repository.Delete(1);

                // Assert
                await Assert.ThrowsAsync<CrealutionEntityNotFound>(() => repository.GetById(1));
            }
        }
    }
}