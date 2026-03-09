using System.IO;
using System.Linq;
using AwesomeAssertions;
using NUnit.Framework;

namespace PropertyConfig.Tests;

[TestFixture]
public class PropertiesTests
{
    [Test]
    public void GetProperty_WhenPropertyDoesNotExist_ReturnsNull()
    {
        var properties = new Properties();
        
        properties.GetProperty("Hello").Should().BeNull();
    }

    [Test]
    public void GetProperty_WhenPropertyDoesNotExistAndDefaultProvided_ReturnsDefaultValue()
    {
        var properties = new Properties();
        
        properties.GetProperty("Hello", "xxxx").Should().Be("xxxx");
    }

    [Test]
    public void GetProperty_WhenPropertyExistsAndDefaultProvided_ReturnsActualValue()
    {
        var properties = new Properties();
        properties.SetProperty("Hello", "World");
        
        properties.GetProperty("Hello", "xxxx").Should().Be("World");
    }

    [Test]
    public void PropertyNames_WhenSinglePropertySet_ReturnsOneKey()
    {
        var properties = new Properties();
        properties.SetProperty("Hello", "World");
        
        var keys = properties.PropertyNames();
        
        keys.Count().Should().Be(1);
    }

    [Test]
    public void StoreToXml_WhenCalledWithIndexerAndNoPath_DoesNotThrow()
    {
        var properties = new Properties();
        
        properties.Invoking(p =>
        {
            p["Hello"] = "World";
            p.StoreToXml();
        }).Should().NotThrow();
    }

    [Test]
    public void StoreToXml_WhenNoPathSpecified_CreatesDefaultConfigFile()
    {
        var properties = new Properties();
        properties["Hello"] = "World";
        
        properties.StoreToXml();
        
        File.Exists("config.xml").Should().BeTrue();
    }

    [Test]
    public void StoreToXml_WhenPathSpecified_CreatesFileAtSpecifiedPath()
    {
        var properties = new Properties();
        properties["Marco"] = "Polo";
        
        properties.StoreToXml("./../special.xml");
        
        File.Exists("./../special.xml").Should().BeTrue();
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
        
        properties["Hello"].Should().Be("World");
    }

    [Test]
    public void LoadFromXml_WhenFileDoesNotExist_ThrowsFileNotFoundException()
    {
        var properties = new Properties();
        
        properties.Invoking(p => p.LoadFromXml("does-not-exist.xml"))
            .Should().ThrowExactly<FileNotFoundException>();
    }

    [Test]
    public void LoadFromXml_WhenLoadingFromSpecifiedFile_RestoresStoredProperties()
    {
        var properties = new Properties();
        
        properties.LoadFromXml("test-config.xml");
        
        properties["Hello"].Should().Be("Mum");
    }
}