using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using H.Core.Runners;

namespace H.Core.Synthesizers
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Synthesizer : Runner, ISynthesizer
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public bool UseCache { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        protected Synthesizer()
        {
            Add(new AsyncAction("synthesize", async (command, cancellationToken) =>
            {
                var text = command.Value.Arguments.First();
                var format = Enum.TryParse<AudioFormat>(
                    command.Value.Arguments.ElementAtOrDefault(1), true, out var result)
                    ? result
                    : AudioFormat.Raw;

                var bytes = await ConvertAsync(text, format, cancellationToken).ConfigureAwait(false);

                return new Value(bytes);
            }, "text"));
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="format"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract Task<byte[]> ConvertAsync(
            string text, 
            AudioFormat format = AudioFormat.Raw, 
            CancellationToken cancellationToken = default);

        #endregion
    }
}
