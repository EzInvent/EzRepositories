using EzRepositories.Tests.Fixture;
using EzRepositories.Tests.Helpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;

namespace EzRepositories.Tests
{
    public partial class RepositoryTests
    {
        private TestDbContext _db;
        private List<User> _userTestData = UsersFixtures.GetTestData();

        public RepositoryTests() { 
            var dbContext = DbContextHelper.CreateInMemoryDbContext();
            _db = dbContext;
            
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllUsers()
        {
            // Arrange
            var expectedResposne = _userTestData;
            AddUserTestData();
            var repo = new Repository<User>(_db);

            // Act
            var result = await repo.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(expectedResposne.Count);
        }

        [Fact]
        public async Task GetAllAsync_EmptyTable_ShouldReturnEmptyList()
        {
            // Arrange
            var repo = new Repository<User>(_db);

            // Act
            var result = await repo.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(0);
        }

        [Fact]
        public async Task GetAllAsync_WithFilter_ShouldReturnListOfUsers()
        {
            // Arrange
            var repo = new Repository<User>(_db);
            AddUserTestData();
            var expectedResponse = _userTestData.Where(user => user.Id > 2);

            // Act
            var result = await repo.GetAllAsync(user => user.Id > 2);

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(expectedResponse.Count());
        }

        [Fact]
        public async Task GetAllAsync_WithFilter_InvalidName_ShouldReturnEmptyList()
        {
            // Arrange
            var repo = new Repository<User>(_db);

            // Act
            var result = await repo.GetAllAsync(user => user.Name.ToLower().Contains("o"));

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(0);
        }

        [Fact]
        public async Task GetAsync_ById_ValidEntity_ShouldReturnUserWithDetail()
        {
            // Arrange
            var repo = new Repository<User>(_db);
            AddUserTestData();
            var idToGet = 2;

            // Act
            var result = await repo.GetAsync(idToGet);
            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAsync_ById_DifferentIdType_ShouldThrowArgumentException()
        {
            // Arrange
            var repo = new Repository<User>(_db);
            AddUserTestData();
            var username = "Somename";

            // Act
            Func<Task> func = async () => { await repo.GetAsync(username); };

            // Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("*Int32*")
                .WithMessage("*String*");
        }

        [Fact]
        public async Task GetAsync_ById_EntityWithNoId_ShouldInvalidOperationException()
        {
            // Arrange
            var repo = new Repository<EntityWithNoId>(_db);
            var testData = EntityWithNoIdFixtures.GetTestData();
            var name = "name";

            // Act
            Func<Task> func = async () => { await repo.GetAsync(name); };
            //Assert
            await func.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task GetAsync_ByFilter_ValidData_ShouldReturnResult()
        {
            // Arrange
            var repo = new Repository<User>(_db);
            var testData = UsersFixtures.GetTestData();
            AddUserTestData();

            // Act
            var result = await repo.GetAsync(e => e.Name.Length > 4);
            //Assert
            result.Should().NotBeNull();
            result.Name.Length.Should().BeGreaterThan(4);
        }

        [Fact]
        public async Task CreateAsync_ValidEntity_ShouldReturnCreatedEntity()
        {
            // Arrange 
            var repo = new Repository<User>(_db);
            var UserToAdd = new User
            {
                Name = "James Johnson"
            };

            // Act
            var result = await repo.CreateAsync(UserToAdd);

            //Assert
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Name.Should().Be(UserToAdd.Name);
        }

        [Fact]
        public async Task UpdateAsync_ValidEntity_ShouldReturnUpdatedEntity()
        {
            var repo = new Repository<User>(_db);
            AddUserTestData();
            var userToUpdate = (await repo.GetAllAsync()).First();

            userToUpdate.Name = "James Johnson";

            // Act 
            var result = await repo.UpdateAsync(userToUpdate);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(userToUpdate.Id);
            result.Name.Should().Be(userToUpdate.Name);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_ShouldBeTrue()
        {
            var repo = new Repository<User>(_db);
            AddUserTestData();
            var currentDataCount = (await repo.GetAllAsync()).Count();
            var IdToDelete = (await repo.GetAllAsync()).First().Id;

            // Act 
            var result = await repo.DeleteAsync(IdToDelete);

            // Assert
            result.Should().BeTrue();
            var newDataCount = (await repo.GetAllAsync()).Count();
            newDataCount.Should().Be(currentDataCount - 1);
        }

        [Fact]
        public async Task DeleteAsync_ById_IdArgumentofDifferentType_ShouldThrowArgumentException()
        {
            // Arrange
            var repo = new Repository<User>(_db);
            AddUserTestData();
            var currentDataCount = (await repo.GetAllAsync()).Count();
            var IdToDelete = "idToDelete";

            // Act 
            Func<Task> func = async () => await repo.DeleteAsync(IdToDelete);

            // Assert
            await func.Should().ThrowAsync<ArgumentException>();
        }
        [Fact]
        public async Task DeleteAsync_ByFilter_ValidValues_ShouldBeTrue()
        {
            // Arrange 
            var repo = new Repository<User>(_db);
            AddUserTestData();

            // Act
            var result = await repo.DeleteAsync(e => e.Id > 4);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsync_EntityNotFound_ShouldReturnFalse()
        {
            // Arrange 
            var repo = new Repository<User>(_db);
            AddUserTestData();

            // Act
            var result = await repo.DeleteAsync(e => e.Id == -1);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteAsync_ByEntity_ShouldReturnTrue()
        {// Arrange 
            var repo = new Repository<User>(_db);
            AddUserTestData();
            var entityToDelete = (await repo.GetAllAsync()).First();

            // Act
            var result = await repo.DeleteAsync(entityToDelete);

            // Assert
            result.Should().BeTrue();

        }

        private void AddUserTestData()
        {
            if (_db.Users.Any())
            {
                return;
            }
            _db.Users.AddRange(_userTestData);
            _db.SaveChanges();
        }
    }
}