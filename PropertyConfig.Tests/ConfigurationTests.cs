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
    }
}
