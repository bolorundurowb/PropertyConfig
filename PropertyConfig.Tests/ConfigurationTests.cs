using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace PropertyConfig.Tests
{
    [TestFixture]
    public class ConfigurationTests
    {
        [Test]
        public void RetrieveNonExistentProperty()
        {
            var configuration = new Configuration();
            Assert.AreEqual(configuration.GetProperty("Hello"), null);
        }

        [Test]
        public void RetrieveNonExistingPropertyWithDefaultTest()
        {
            var configuration = new Configuration();
            Assert.AreEqual(configuration.GetProperty("Hello", "xxxx"), "xxxx");
        }

        [Test]
        public void RetrieveExistingPropertyWithDefaultTest()
        {
            var configuration = new Configuration();
            configuration.SetProperty("Hello", "World");
            Assert.AreEqual(configuration.GetProperty("Hello", "xxxx"), "World");
        }

        [Test]
        public void RetrieveAllKeysTest()
        {
            var configuration = new Configuration();
            configuration.SetProperty("Hello", "World");
            var keys = configuration.PropertyNames();
            Assert.AreEqual(keys.Count(), 1);
        }
        
        [Test]
		public void LibraryIsStable()
		{
			Assert.DoesNotThrow(delegate {
				var configuration = new Configuration();
				configuration["Hello"] = "World";
				configuration.StoreToXml();
			});
		}

        [Test]
        public void StorePropertiesWithDefaultPath()
        {
            Assert.DoesNotThrow(delegate {
                var configuration = new Configuration();
                configuration["Hello"] = "World";
                configuration.StoreToXml();
            });
            FileAssert.Exists("config.xml");
        }

        [Test]
        public void StorePropertiesWithSpecifiedPath()
        {
            Assert.DoesNotThrow(delegate {
                var configuration = new Configuration();
                configuration["Marco"] = "Polo";
                configuration.StoreToXml("./../special.xml");
            });
	        
            FileAssert.Exists("./../special.xml");
        }

        [Test]
        public void LoadConfigFromDefaultPath()
        {
            var configuration = new Configuration();
            Assert.DoesNotThrow(delegate
            {
				configuration["Hello"] = "World";
				configuration.StoreToXml();
                configuration.LoadFromXml();
            });
            Assert.AreEqual(configuration["Hello"], "World");
        }

        [Test]
        public void LoadConfigFromSpecifiedPath()
        {
            var configuration = new Configuration();
            Assert.DoesNotThrow(delegate
            {
				configuration["marco"] = "polo x ";
				configuration.StoreToXml("file1.xml");
                configuration.LoadFromXml("file1.xml");
            });
            Assert.AreEqual(configuration["marco"], "polo x ");
        }
    }
}
