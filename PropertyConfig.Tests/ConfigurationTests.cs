using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace PropertyConfig.Tests
{
    [TestFixture]
    public class ConfigurationTests
    {
        [Test]
		public void LibraryIsStable()
		{
			Assert.DoesNotThrow(delegate {
				Configuration configuration = new Configuration();
				configuration["Hello"] = "World";
				configuration.StoreToXml();
			});
		}

        [Test]
        public void StoreXMLTest()
        {
            Assert.DoesNotThrow(delegate {
                Configuration configuration = new Configuration();
                configuration["Hello"] = "World";
                configuration.StoreToXml();
            });
            FileAssert.Exists("config.xml");
        }

        [Test]
        public void LoadFromXMLTest()
        {
            FileAssert.Exists("config.xml");
            Configuration configuration = new Configuration();
            Assert.DoesNotThrow(delegate
            {
				configuration["Hello"] = "World";
				configuration.StoreToXml();
                configuration.LoadFromXml();
            });
            Assert.AreEqual(configuration["Hello"], "World");
        }

		[Test]
		public void DefaultConfigFileTest()
		{
			Configuration configuration = new Configuration();
			Assert.AreEqual(configuration.FilePath, "config.xml");
			Assert.DoesNotThrow(delegate {
				configuration.FilePath = "new_config.xml";
			});
			Assert.AreEqual(configuration.FilePath, "new_config.xml");
		}

		[Test]
		public void RetrievePropertyTest()
		{
			Configuration configuration = new Configuration();
			Assert.AreEqual(configuration.GetProperty("Hello"), null);
		}

		[Test]
		public void RetrieveNonExistingPropertyWithDefaultTest()
		{
			Configuration configuration = new Configuration();
			Assert.AreEqual(configuration.GetProperty("Hello", "World"), "World");
		}

		[Test]
		public void RetrieveExistingPropertyWithDefaultTest()
		{
			Configuration configuration = new Configuration();
			configuration.SetProperty("Hello", "World");
			Assert.AreEqual(configuration.GetProperty("Hello", "World"), "World");
		}

		[Test]
		public void RetrieveAllKeysTest()
		{
			Configuration configuration = new Configuration();
			configuration.SetProperty("Hello", "World");
			List<string> keys = configuration.PropertyNames().ToList();
			Assert.AreEqual(keys.Count, 1);
		}
    }
}
