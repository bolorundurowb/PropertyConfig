using NUnit.Framework;

namespace PropertyConfig.Tests
{
    [TestFixture]
    public class ConfigurationTests
    {
        [Test]
        public void DummyTest1()
        {
            Assert.True(true);
        }

		[Test]
		public void LibraryIsStable()
		{
			Assert.DoesNotThrow(delegate {
				Configuration configuration = new Configuration();
				configuration["Hello"] = "World";
				configuration.StoreToXml();
			});
		}
    }
}
