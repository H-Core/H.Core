using System;
using System.Globalization;
using System.Linq;

namespace H.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class AudioSettings
    {
        #region Static methods

        /// <summary>
        /// Parse strings like: <br/>
        /// <value>(raw, 16000, 16, 1)</value> -> Format = Raw, Rate = 16000, Bits = 16, Channels = 1. <br/>
        /// <value>(wav)</value> -> Format = Wav, Rate = 8000, Bits = 16, Channels = 1. <br/>
        /// <value>raw</value> -> Format = Raw, Rate = 8000, Bits = 16, Channels = 1. <br/>
        /// <value>()</value> -> Format = Raw, Rate = 8000, Bits = 16, Channels = 1. <br/>
        /// <value></value> -> Format = Raw, Rate = 8000, Bits = 16, Channels = 1. <br/>
        /// </summary>
        /// <param name="text"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static AudioSettings Parse(string text)
        {
            text = text ?? throw new ArgumentNullException(nameof(text));

            var values = text
                .Trim('(', ')')
                .Split(',')
                .Select(value => value.Trim())
                .ToArray();

            var format = Enum.TryParse<AudioFormat>(values.ElementAtOrDefault(0) ?? "raw", true, out var formatResult)
                ? formatResult
                : AudioFormat.Raw;
            var rate = Convert.ToInt32(values.ElementAtOrDefault(1) ?? "8000", CultureInfo.InvariantCulture);
            var bits = Convert.ToInt32(values.ElementAtOrDefault(2) ?? "16", CultureInfo.InvariantCulture);
            var channels = Convert.ToInt32(values.ElementAtOrDefault(3) ?? "1", CultureInfo.InvariantCulture);

            return new AudioSettings(format, rate, bits, channels);
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public AudioFormat Format { get; }

        /// <summary>
        /// 
        /// </summary>
        public int Rate { get; }

        /// <summary>
        /// 
        /// </summary>
        public int Bits { get; }

        /// <summary>
        /// 
        /// </summary>
        public int Channels { get; }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan Delay { get; set; } = TimeSpan.FromMilliseconds(10);

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="rate"></param>
        /// <param name="bits"></param>
        /// <param name="channels"></param>
        public AudioSettings(AudioFormat format = AudioFormat.Raw, int rate = 8000, int bits = 16, int channels = 1)
        {
            Format = format;
            Rate = rate;
            Bits = bits;
            Channels = channels;
        }

        #endregion
    }
}
