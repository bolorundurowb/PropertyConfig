# PropertyConfig

[![CircleCI](https://circleci.com/gh/bolorundurowb/PropertyConfig.svg?style=svg)](https://circleci.com/gh/bolorundurowb/PropertyConfig)

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

If you already have a configuration XML file, you can load it like:
```csharp
PropertyConfig propertyCfg = new PropertyConfig();
propertyCfg.LoadFromXml(/*path to config file*/);
```
The properties can be accessed via a `NameValueCollection`:
```csharp
string propertyValue = propertyCfg["propertyName"];
```

Properties can also be set using the `NameValueCollection` like:
```csharp
propertyCfg["propertyName"] = propertyValue;
```

When you are done setting all configuration properties, you can save your configuration to an XML file via:
```csharp
propertyCfg.SaveToXml();
```
