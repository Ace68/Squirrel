using NUnit.Framework;
using Windows.Storage;

namespace Squirrel.Config.Test
{
    [TestFixture]
    public class SquirrelConfigTest
    {
        private const string SquirrelContainerName = "SquirrelContainer";

        [Test]
        public void CantRunWithoutZWaveAddress()
        {
            var squirrelSettings = ApplicationData.Current.LocalSettings;

            if (!squirrelSettings.Containers.ContainsKey(SquirrelContainerName))
            {
                squirrelSettings.CreateContainer(SquirrelContainerName, ApplicationDataCreateDisposition.Always);
            }

            Assert.AreNotEqual(string.Empty, squirrelSettings.Containers[SquirrelContainerName].Values.ContainsKey("ZWaveServerAddress"));
        }
    }
}
