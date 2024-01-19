---
 title: Advanced Usage
---

# Advanced Usage

## Creating Custom Repository Classes

You have the flexibility to create custom repository classes that extend the base `Repository<TEntity>` class. This allows you to tailor the repository behavior to meet the specific requirements of your application.

### Example: Creating a Custom Repository for YourEntity

```csharp
public class CustomRepository : Repository<YourEntity>
{
    public CustomRepository(YourDbContext dbContext) : base(dbContext)
    {
    }

    // Override methods as needed...

    // Custom logic before creating a new entity
    protected override void OnBeforeCreating(YourEntity entity)
    {
        // Add your custom logic here...
        Console.WriteLine($"Custom logic before creating: {entity}");
    }

    // Custom logic before updating an existing entity
    protected override void OnBeforeUpdating(YourEntity entity)
    {
        // Add your custom logic here...
        Console.WriteLine($"Custom logic before updating: {entity}");
    }
}
```

## Registering with Dependency Injection

### Using the Base Format

When using only one DbContext, you can register the repository using the base format without explicitly defining what DbContext is requried. The scope of the repository can be adjusted according to your needs.

```csharp
services.AddEzRepository<YourEntity>();

// Registering an entity with a different life time
services.AddEzRepository<YourEntity2>(ServiceLifetime.Transient)
```

### Using Multiple DbContexts

If your application involves multiple DbContexts, you can register repositories for each DbContext.

```csharp
services.AddEzRepository<YourEntity1, YourDbContext1>();
services.AddEzRepository<YourEntity2, YourDbContext2>();
// Add more repositories as needed...
```

### Using a Custom EzRepository

If you have created a child class implementation (e.g., `CustomRepository`), register it with the DI container.

```csharp
services.AddCustomEzRepository<YourEntity, CustomRepository>();
```

Adjust the registration based on the specific requirements and scenarios in your application.

> [!NOTE]
> The default ServiceLifeTime used is Scoped

This concludes the advanced usage guide for EzRepositories. Explore and leverage these features to build robust and customized data access layers in your .NET applications.