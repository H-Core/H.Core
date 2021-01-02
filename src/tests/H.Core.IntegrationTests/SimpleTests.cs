using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using H.Core.Recorders;
using H.Core.Utilities;
using H.Recorders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace H.Core.IntegrationTests
{
    [TestClass]
    public class SimpleTests
    {
        private static void CheckDevices()
        {
            var devices = NAudioRecorder.GetAvailableDevices().ToList();
            if (!devices.Any())
            {
                Assert.Inconclusive("No available devices for NAudioRecorder.");
            }

            Console.WriteLine("Available devices:");
            foreach (var device in devices)
            {
                Console.WriteLine($" - Name: {device.ProductName}, Channels: {device.Channels}");
            }
        }

        [TestMethod]
        public async Task NoiseDetectionTest()
        {
            using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            var cancellationToken = cancellationTokenSource.Token;

            CheckDevices();

            var source = new TaskCompletionSource<bool>();
            using var exceptions = new ExceptionsBag();
            using var registration = cancellationToken.Register(() => source.TrySetCanceled(cancellationToken));

            using var recorder = new NAudioRecorder();
            using var recording = await recorder.StartAsync(AudioFormat.Raw, cancellationToken);
            recording.Stopped += (_, _) => source.TrySetResult(true);
            recording.StopWhen(exceptions: exceptions);

            await source.Task;
        }
    }
}
