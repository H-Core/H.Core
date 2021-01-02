using System;
using System.Collections.Concurrent;
using System.Linq;
using H.Core.Utilities;

namespace H.Core.Recorders
{
    /// <summary>
    /// 
    /// </summary>
    public static class RecordingExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="bits"></param>
        /// <returns></returns>
        private static double GetMaxLevel(this byte[] bytes, int bits)
        {
            bytes = bytes ?? throw new ArgumentNullException(nameof(bytes));

            var max = 0.0;
            for (var index = 0; index < bytes.Length; index += bits / 8)
            {
                var level = Math.Abs(bits switch
                {
                    8 => 1.0 * bytes[index] / byte.MaxValue,
                    16 => 1.0 * (short)((bytes[index + 1] << 8) | bytes[index]) / short.MaxValue,
                    32 => 1.0 * ((bytes[index + 3] << 24) | (bytes[index + 2] << 16) | (bytes[index + 1] << 8) | bytes[index]) / int.MaxValue,
                    _ => throw new NotImplementedException($"Bits: {bits} are not supported.")
                });
                if (level > max)
                {
                    max = level;
                }
            }

            return max;
        }

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

            var last = new ConcurrentQueue<double>();
            recording.DataReceived += async (_, bytes) =>
            {
                try
                {
                    var max = bytes.GetMaxLevel(recording.Settings.Bits);

                    last.Enqueue(max);
                    if (last.Count <= bufferSize)
                    {
                        return;
                    }

                    last.TryDequeue(out var _);
                    if (last.ToArray().Count(value => value < threshold) >= requiredCount)
                    {
                        await recording.StopAsync().ConfigureAwait(false);
                    }
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
