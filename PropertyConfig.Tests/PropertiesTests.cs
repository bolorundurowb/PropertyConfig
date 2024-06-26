using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace PropertyConfig.Tests;

[TestFixture]
public class PropertiesTests
{
    [Test]
    public void RetrieveNonExistentProperty()
    {
        var configuration = new Properties();
        configuration.GetProperty("Hello").Should().BeNull();
    }

    [Test]
    public void RetrieveNonExistingPropertyWithDefaultTest()
    {
        var configuration = new Properties();
        configuration.GetProperty("Hello", "xxxx").Should().Be("xxxx");
    }

    [Test]
    public void RetrieveExistingPropertyWithDefaultTest()
    {
        var configuration = new Properties();
        configuration.SetProperty("Hello", "World");
        configuration.GetProperty("Hello", "xxxx").Should().Be("World");
    }

    [Test]
    public void RetrieveAllKeysTest()
    {
        var configuration = new Properties();
        configuration.SetProperty("Hello", "World");
        var keys = configuration.PropertyNames();
        keys.Count().Should().Be(1);
    }

    [Test]
    public void LibraryIsStable()
    {
        var configuration = new Properties();
        configuration.Invoking(c =>
        {
            c["Hello"] = "World";
            c.StoreToXml();
        }).Should().NotThrow();
    }

    [Test]
    public void StorePropertiesWithDefaultPath()
    {
        var configuration = new Properties();
        configuration.Invoking(c =>
        {
            c["Hello"] = "World";
            c.StoreToXml();
        }).Should().NotThrow();

        File.Exists("config.xml").Should().BeTrue();
    }

    [Test]
    public void StorePropertiesWithSpecifiedPath()
    {
        var configuration = new Properties();
        configuration.Invoking(c =>
        {
            c["Marco"] = "Polo";
            c.StoreToXml("./../special.xml");
        }).Should().NotThrow();

        File.Exists("./../special.xml").Should().BeTrue();
    }

    [Test]
    public void LoadConfigFromDefaultPath()
    {
        var configuration = new Properties();
        configuration.Invoking(c =>
        {
            c["Hello"] = "World";
            c.StoreToXml();
            c.LoadFromXml();
        }).Should().NotThrow();

        configuration["Hello"].Should().Be("World");
    }

    [Test]
    public void LoadConfigFromSpecifiedPath()
    {
        var configuration = new Properties();
        configuration.Invoking(c =>
        {
            c["marco"] = "polo x ";
            c.StoreToXml("file1.xml");
            c.LoadFromXml("file1.xml");
        }).Should().NotThrow();

        configuration["marco"].Should().Be("polo x ");
    }
}
