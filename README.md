# PropertyConfig

[![Build Status](https://travis-ci.org/bolorundurowb/PropertyConfig.svg?branch=develop)](https://travis-ci.org/bolorundurowb/PropertyConfig) [![Coverage Status](https://coveralls.io/repos/github/bolorundurowb/PropertyConfig/badge.svg?branch=master)](https://coveralls.io/github/bolorundurowb/PropertyConfig?branch=master) [![Method Parity](https://img.shields.io/badge/method----parity-6%20%2F%2015-yellowgreen.svg)]()


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

Instantiate the class (preferrably in the main entry class and storing it in a static variable)
```csharp
public static Configuration configuration = new Configuration();
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
configuration.StoreToXml(/*path to custom xml file*/); //saves to specified xml file
//or
configuration.StoreToXml(/*path to custom xml file*/, /*comment to add to config files*/);
```

this produces an output that looks like
```xml
<config>
  <!--Created by Property Config-->
  <Hello>World</Hello>
</config>
```

To load a config file
```csharp
configuration.LoadFromXml() //loads from default 'config.xml'
//or
configuration.LoadFromXml(/*path to custom xml file*/); //saves to specified xml file
```
