using EzRepositories.Extensions;
using EzRepositories.Tests.Fixture;
using EzRepositories.Tests.Helpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzRepositories.Tests
{
    public class DependencyInjectionExtensionsTests
    {
        [Fact]
        public void AddEzRepository_ShouldAddRepositoryWithCorrectLifeTime()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddEzRepository<User>();

            // Assert
            services.Count.Should().Be(1);
            var descriptor = services.First();
            descriptor.Lifetime.Should().Be(ServiceLifetime.Scoped);
            descriptor.ServiceType.Should().Be(typeof(IRepository<User>));
            descriptor.ImplementationType.Should().Be(typeof(Repository<User>));

            
        }

        [Fact]
        public async Task AddEzRepository_ShouldInjectRepositoryPointingToSpecifiedDbContext()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddDbContext<TestDbContext>(options =>
            {
                // Configure your database provider here
                _ = options.UseInMemoryDatabase(databaseName: $"TestDatabase-{Guid.NewGuid()}")
                    .Options;
            });
            services.AddEzRepository<User, TestDbContext>(ServiceLifetime.Transient);

            // Assert
            var repositoryDescriptor = services.Last();
            var dbContextDescriptor = services.FirstOrDefault(s => s.ImplementationType == typeof(TestDbContext));

            dbContextDescriptor.Should().NotBeNull();
            repositoryDescriptor.Should().NotBeNull();

            repositoryDescriptor.Lifetime.Should().Be(ServiceLifetime.Transient);
            repositoryDescriptor.ServiceType.Should().Be(typeof(IRepository<User>));
        }

        [Fact]
        public async Task AddEzRepository_ShouldInjectChildRepository()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddDbContext<TestDbContext>(options =>
            {
                // Configure your database provider here
                _ = options.UseInMemoryDatabase(databaseName: $"TestDatabase-{Guid.NewGuid()}")
                    .Options;
            });

            services.AddCustomEzRepository<User, UserRepository>(ServiceLifetime.Transient);

            // Assert
            var repositoryDescriptor = services.Last();
            var dbContextDescriptor = services.FirstOrDefault(s => s.ImplementationType == typeof(TestDbContext));

            dbContextDescriptor.Should().NotBeNull();
            repositoryDescriptor.Should().NotBeNull();

            repositoryDescriptor.Lifetime.Should().Be(ServiceLifetime.Transient);
            repositoryDescriptor.ServiceType.Should().Be(typeof(IRepository<User>));
        }

    }
}
