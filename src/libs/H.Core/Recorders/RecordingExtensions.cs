using System;
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
    }
}
