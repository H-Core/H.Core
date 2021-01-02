using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace H.Core.UnitTests
{
    [TestClass]
    public class AudioSettingsTests
    {
        [TestMethod]
        public void ParseTest()
        {
            var settings = AudioSettings.Parse("mp3");

            Assert.AreEqual(settings.Format, AudioFormat.Mp3);
        }
    }
}
