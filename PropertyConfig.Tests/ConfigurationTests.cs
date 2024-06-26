using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using System.IO;

namespace PropertyConfig.Tests
{
    [TestFixture]
    public class ConfigurationTests
    {
        [Test]
        public void RetrieveNonExistentProperty()
        {
            var configuration = new Configuration();
            configuration.GetProperty("Hello").Should().BeNull();
        }

        [Test]
        public void RetrieveNonExistingPropertyWithDefaultTest()
        {
            var configuration = new Configuration();
            configuration.GetProperty("Hello", "xxxx").Should().Be("xxxx");
        }

        [Test]
        public void RetrieveExistingPropertyWithDefaultTest()
        {
            var configuration = new Configuration();
            configuration.SetProperty("Hello", "World");
            configuration.GetProperty("Hello", "xxxx").Should().Be("World");
        }

        [Test]
        public void RetrieveAllKeysTest()
        {
            var configuration = new Configuration();
            configuration.SetProperty("Hello", "World");
            var keys = configuration.PropertyNames();
            keys.Count().Should().Be(1);
        }

        [Test]
        public void LibraryIsStable()
        {
            var configuration = new Configuration();
            configuration.Invoking(c => 
            {
                c["Hello"] = "World";
                c.StoreToXml();
            }).Should().NotThrow();
        }

        [Test]
        public void StorePropertiesWithDefaultPath()
        {
            var configuration = new Configuration();
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
            var configuration = new Configuration();
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
            var configuration = new Configuration();
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
            var configuration = new Configuration();
            configuration.Invoking(c => 
            {
                c["marco"] = "polo x ";
                c.StoreToXml("file1.xml");
                c.LoadFromXml("file1.xml");
            }).Should().NotThrow();

            configuration["marco"].Should().Be("polo x ");
        }
    }
}
