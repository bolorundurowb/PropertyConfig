# PropertyConfig

[![Build, Test & Cover](https://github.com/bolorundurowb/PropertyConfig/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/bolorundurowb/PropertyConfig/actions/workflows/build-and-test.yml)
[![Coverage Status](https://coveralls.io/repos/github/bolorundurowb/PropertyConfig/badge.svg?branch=master)](https://coveralls.io/github/bolorundurowb/PropertyConfig?branch=master) [![Method Parity](https://img.shields.io/badge/method----parity-6%20%2F%2015-yellowgreen.svg)]() [![NuGet Badge](https://buildstats.info/nuget/propertyconfig.dll)](https://www.nuget.org/packages/PropertyConfig.dll/) 


## Description

This library is developed to provide similar functionality to Java's `java.util.Properties` class. It allows for the flat storing of name-value pairs in XML.

## Usage
Get the library from Nuget by using your package manager or by running;

```bash
Install-Package PropertyConfig.dll
```

Add the directive to your class file:
```csharp
using PropertyConfig;
```

Instantiate the class (preferably in the main entry class and storing it in a static variable)
```csharp
public static Properties configuration = new Properties();
```

Add whatever properties necessary, for example
```csharp
configuration["Hello"] = "World";
//or 
configuration.Add("Hello", "World");
```

After including every property, save the configuration
```csharp
configuration.StoreToXml(); //saves to the default 'config.xml'
//or
configuration.StoreToXml("my-config.xml"); //saves to specified xml file
//or
configuration.StoreToXml("my-config.xml", "Do not modify manually"); // saves to specified file with additional comment
```

this produces an output that looks like
```xml
<config>
  <!--Do not modify manually-->
  <Hello>World</Hello>
</config>
```

To load a config file
```csharp
configuration.LoadFromXml() //loads from default 'config.xml'
//or
configuration.LoadFromXml("my-config.xml"); //loads config key-value pairs from the specified file
```
