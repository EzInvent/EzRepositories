---
title: Getting Started
---

# Quick Start Guide

This guide will walk you through the basic steps to integrate EzRepositories into your .NET project.

## Step 1: Adding EzRepositories to Your Project {adding-ezrepositories-to-your-project}

Ensure that you have successfully installed EzRepositories by following the [Installation](./01-Installation.md) guide.

## Step 2: Setting Up Your `DbContext` {setting-up-your-dbcontext}

Create a class that inherits form `DbContext` to represent your data context. Here's an example:

```csharp

public class YourDbContext : DbContext
{
	public YourDbContext(DbContextOptions<YourDbContext> options): base(options)
	{
	}

	// Define your DbSet properties here
	public DbSet<YourEntity> YourEntities { get; set; }
}
```

## Step 3: Creating an Enity Class {creating-an-entity-class}

Define your entity class that corresponds to your database table. Ensure it inherits form `IEntity` and the primary key of your entity has the `[Key]` attribute:

```csharp
public class YourEntity : IEntity
{
	// Define your entity properties here
	[Key]
	public int Id { get; set; }

	// Additional Properties...
}
```

## Step 4: Registering EzRepositories with Dependency Injection {dependency-injection-registration}

In this step, you'll register EzRepositories with your `DbContext` in your dependency injection (DI) container. This step assumes that your project is configured to use DI.

### Option 1: Registering in Startup.cs {registering-in-startup-cs}
In your `Startup.cs` file, find the `ConfigureServices` method, and add the following code to register EzRepositories with your `DbContext`:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Other configurations...

    services.AddEzRepository<YourEntity, YourDbContext>();

    // Additional configurations...
}

```

### Option 2: Register in Program.cs {registering-in-program-cs}

If you are using the new host model locate your `Program.cs` file add the following code to register EzRepositories with your `DbContext`:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Other configurations...

builder.Services.AddEzRepository<YourEntity, YourDbContext>();

// Additional configurations...

var app = builder.Build();
app.Run();
```

> [!NOTE]
> This step is only necessary on ly if your project implements the Dependency Injection (DI) container.

# Step 5: Using EzRepositories {using-ezrepositories}

Now you can inject `IRepository<YourEntity>` into your services or controllers and start using CRUD operations

```csharp
public class YourService
{
    private readonly IRepository<YourEntity> _repository;

    public YourService(IRepository<YourEntity> repository)
    {
        _repository = repository;
    }

    public async Task YourMethod()
    {
        // Example: Create operation
        var newEntity = new YourEntity { /* Set properties */ };
        var createdEntity = await _repository.CreateAsync(newEntity);

        // Additional operations...
    }
}
```

---

With these basic steps, you've successfully integrated EzRepositories into your project. For more advanced usage and customization options, refer to the [Advanced Usage](../02-Usage/01-Advanced-Usage.md) guide.