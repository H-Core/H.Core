using System.Linq;
using H.Core.Recorders;
using H.IO.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace H.Core.IntegrationTests
{
    [TestClass]
    public class SimpleTests
    {
        [TestMethod]
        public void SilenceDetectorTest()
        {
            var bytes = ResourcesUtilities
                .ReadFileAsBytes("test_test_rus_8000.wav")
                .Skip(44)
                .ToArray();

            var isDetected = false;
            var detector = new SilenceDetector(requiredCount: 200);
            detector.Detected += (_, _) => isDetected = true;

            var partLength = bytes.Length / 500; // One part is 10 milliseconds.

            for (var i = 0; i < bytes.Length / partLength; i++)
            {
                var part = bytes.Skip(i * partLength).Take(partLength).ToArray();

                detector.Write(part);
            }

            Assert.IsTrue(isDetected);
        }
    }
}
