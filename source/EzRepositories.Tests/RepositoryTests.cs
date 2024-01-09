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


        public RepositoryTests() { 
            var dbContext = DbContextHelper.CreateInMemoryDbContext();
            _db = dbContext;
            
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllUsers()
        {
            // Arrange
            var expectedResposne = UsersFixtures.GetTestData();
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
            var expectedResponse = UsersFixtures.GetTestData().Where(user => user.Name.ToLower().Contains("o"));

            // Act
            var result = await repo.GetAllAsync(user => user.Name.ToLower().Contains("o"));

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(expectedResponse.Count());
        }

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

        private void AddUserTestData()
        {
            _db.Users.AddRange(UsersFixtures.GetTestData());
            _db.SaveChanges();
        }
    }
}