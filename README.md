# PropertyConfig

[![Build, Test & Coverage](https://github.com/bolorundurowb/PropertyConfig/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/bolorundurowb/PropertyConfig/actions/workflows/build-and-test.yml)
[![codecov](https://codecov.io/gh/bolorundurowb/PropertyConfig/graph/badge.svg?token=u0KEsaYozN)](https://codecov.io/gh/bolorundurowb/PropertyConfig)
![NuGet Version](https://img.shields.io/nuget/v/PropertyConfig.dll)

`PropertyConfig` is a lightweight .NET library for storing and loading flat key-value configuration in XML, inspired by Java's `java.util.Properties`.

## For library consumers

### Install

```powershell
Install-Package PropertyConfig.dll
```

### Quick start

```csharp
using PropertyConfig;

var configuration = new Properties();

// Set values
configuration["Hello"] = "World";
configuration.Add("Environment", "Production");

// Save to XML
configuration.StoreToXml(); // default file: config.xml
configuration.StoreToXml("my-config.xml");
configuration.StoreToXml("my-config.xml", "Do not modify manually");

// Load from XML
configuration.LoadFromXml(); // default file: config.xml
configuration.LoadFromXml("my-config.xml");
```

### Example XML output

```xml
<properties>
  <!--Do not modify manually-->
  <Hello>World</Hello>
  <Environment>Production</Environment>
</properties>
```

### When to use this library

- You want a simple XML-backed key-value configuration format.
- You prefer an API similar to Java `Properties`.
- You need a minimal dependency for basic config persistence.

## For contributors

### Project layout

- `src\PropertyConfig` - library source code.
- `src\PropertyConfig.Tests` - unit tests.

### Build and test

Use your preferred .NET tooling (Rider, Visual Studio, or CLI) to build and run tests from the solution/projects in `src`.

### Contribution notes

- Keep changes focused and backward compatible where possible.
- Add or update tests for behavior changes.
- Open a PR with a clear description of the problem and solution.
