---
title: Installation
---

# Installation

EzRepositories can be easily added to your .NET projects using NuGet Package Manager or the .NET CLI.

## NuGet Package Mangeer Console {nuget-package-manager}

```bash
Install-Package EzInvent.EzRepositories
```

## .NET CLI {dotnet-cli}
```bash
dotnet add package EzInvent.EzRepositories
```

Make sure to run the appropriate command in the root directory of your project.

## Verison Compactibility

EzRepositories is designed to work with .NET Standard and .NET Core. Ensure your project is using a compatible version.

## Dependencies

EzRepositories relies on the Entity Framework Core library. If your project doesn't already include it, you can install it separately:
```bash
dotnet add package Microsoft.EntityFrameworkCore
```


For more detailed information on getting started, proceed to the [Quick Start](./02-Quick-Start.md) guide.
0