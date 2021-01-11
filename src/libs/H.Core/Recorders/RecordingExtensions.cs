using System;
using System.Threading;
using System.Threading.Tasks;
using H.Core.Utilities;

namespace H.Core.Recorders
{
    /// <summary>
    /// 
    /// </summary>
    public static class RecordingExtensions
    {
        /// <summary>
        /// Stop when values for <paramref name="silenceInMilliseconds"></paramref> of
        /// <paramref name="bufferInMilliseconds"/> will be lower than <paramref name="threshold"></paramref>. <br/>
        /// NAudioRecorder produces 100 DataReceived events per seconds. <br/>
        /// It can be set up by NAudioRecorder.Delay property. <br/>
        /// </summary>
        /// <param name="recording"></param>
        /// <param name="bufferInMilliseconds"></param>
        /// <param name="silenceInMilliseconds"></param>
        /// <param name="threshold"></param>
        /// <param name="exceptions"></param>
        public static IRecording StopWhenSilence(
            this IRecording recording, 
            int bufferInMilliseconds = 3000, 
            int silenceInMilliseconds = 2500,
            double threshold = 0.02,
            ExceptionsBag? exceptions = null)
        {
            recording = recording ?? throw new ArgumentNullException(nameof(recording));

            var delay = (int)recording.Settings.Delay.TotalMilliseconds;
            var bufferSize = bufferInMilliseconds / delay;
            var requiredCount = silenceInMilliseconds / delay;

            var detector = new SilenceDetector(recording.Settings, bufferSize, requiredCount, threshold);
            detector.Detected += async (_, _) =>
            {
                try
                {
                    await recording.StopAsync().ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    exceptions?.OnOccurred(exception);
                }
            };
            recording.DataReceived += (_, bytes) =>
            {
                try
                {
                    detector.Write(bytes);
                }
                catch (Exception exception)
                {
                    exceptions?.OnOccurred(exception);
                }
            };

            return recording;
        }

        /// <summary>
        /// Waits <seealso cref="IRecording.Stopped"/> events. <br/>
        /// Can be used with <seealso cref="StopWhenSilence"/> extension.
        /// </summary>
        /// <param name="recording"></param>
        /// <param name="cancellationToken"></param>
        public static async Task WaitStopAsync(
            this IRecording recording,
            CancellationToken cancellationToken = default)
        {
            recording = recording ?? throw new ArgumentNullException(nameof(recording));

            var source = new TaskCompletionSource<bool>();
            using var registration = cancellationToken.Register(() => source.TrySetCanceled());
            recording.Stopped += (_, _) =>
            {
                source.TrySetResult(true);
            };

            await source.Task.ConfigureAwait(false);
        }
    }
}
