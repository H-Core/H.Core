using System;
using System.Linq;
using System.Collections.Concurrent;

namespace H.Core.Recorders
{
    /// <summary>
    /// 
    /// </summary>
    public class SilenceDetector
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public AudioSettings Settings { get; }

        /// <summary>
        /// 
        /// </summary>
        public int BufferSize { get; }

        /// <summary>
        /// 
        /// </summary>
        public int RequiredCount { get; }

        /// <summary>
        /// 
        /// </summary>
        public double Threshold { get; }

        private ConcurrentQueue<double> Queue { get; } = new();

        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? Detected;

        private void OnDetected()
        {
            Detected?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="bufferSize"></param>
        /// <param name="requiredCount"></param>
        /// <param name="threshold"></param>
        public SilenceDetector(
            AudioSettings? settings = null,
            int bufferSize = 300,
            int requiredCount = 250,
            double threshold = 0.02)
        {
            Settings = settings ?? new AudioSettings();
            BufferSize = bufferSize;
            RequiredCount = requiredCount;
            Threshold = threshold;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        public void Write(byte[] bytes)
        {
            var max = GetMaxLevel(bytes, Settings.Bits);

            Queue.Enqueue(max);
            if (Queue.Count <= BufferSize)
            {
                return;
            }

            Queue.TryDequeue(out _);

            var count = Queue.ToArray().Count(value => value < Threshold);
            if (count >= RequiredCount)
            {
                OnDetected();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="bits"></param>
        /// <returns></returns>
        private static double GetMaxLevel(byte[] bytes, int bits)
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

        #endregion
    }
}
