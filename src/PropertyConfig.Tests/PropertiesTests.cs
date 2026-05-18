using System.IO;
using NUnit.Framework;
using OmniAssert;

namespace PropertyConfig.Tests;

[TestFixture]
public class PropertiesTests
{
    [Test]
    public void GetProperty_WhenPropertyDoesNotExist_ReturnsNull()
    {
        var properties = new Properties();
        
        properties.GetProperty("Hello").Verify().ToBeNull();
    }

    [Test]
    public void GetProperty_WhenPropertyDoesNotExistAndDefaultProvided_ReturnsDefaultValue()
    {
        var properties = new Properties();
        
        properties.GetProperty("Hello", "xxxx").Verify().ToBe("xxxx");
    }

    [Test]
    public void GetProperty_WhenPropertyExistsAndDefaultProvided_ReturnsActualValue()
    {
        var properties = new Properties();
        properties.SetProperty("Hello", "World");
        
        properties.GetProperty("Hello", "xxxx").Verify().ToBe("World");
    }

    [Test]
    public void PropertyNames_WhenSinglePropertySet_ReturnsOneKey()
    {
        var properties = new Properties();
        properties.SetProperty("Hello", "World");
        
        var keys = properties.PropertyNames();
        
        keys.Verify().HasCount(1);
    }

    [Test]
    public void StoreToXml_WhenCalledWithIndexerAndNoPath_DoesNotThrow()
    {
        var properties = new Properties();
        
        var act = () =>
        {
            properties["Hello"] = "World";
            properties.StoreToXml();
        };
        act.NotThrow();
    }

    [Test]
    public void StoreToXml_WhenNoPathSpecified_CreatesDefaultConfigFile()
    {
        var properties = new Properties();
        properties["Hello"] = "World";
        
        properties.StoreToXml();
        
        "config.xml".FileExists();
    }

    [Test]
    public void StoreToXml_WhenPathSpecified_CreatesFileAtSpecifiedPath()
    {
        var properties = new Properties();
        properties["Marco"] = "Polo";
        
        properties.StoreToXml("./../special.xml");
        
        "./../special.xml".FileExists();
    }

    [Test]
    public void LoadFromXml_WhenLoadingFromDefaultPath_RestoresStoredProperties()
    {
        var firstConfig = new Properties
        {
            ["Hello"] = "World"
        };
        firstConfig.StoreToXml();

        var properties = new Properties();
        
        properties.LoadFromXml();
        
        properties["Hello"].Verify().ToBe("World");
    }

    [Test]
    public void LoadFromXml_WhenFileDoesNotExist_ThrowsFileNotFoundException()
    {
        var properties = new Properties();
        
        var act = () => properties.LoadFromXml("does-not-exist.xml");
        act.Throws<FileNotFoundException>();
    }

    [Test]
    public void LoadFromXml_WhenLoadingFromSpecifiedFile_RestoresStoredProperties()
    {
        var properties = new Properties();
        
        properties.LoadFromXml("test-config.xml");
        
        properties["Hello"].Verify().ToBe("Mum");
    }
}
