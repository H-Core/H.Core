using System.Collections.Generic;
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

        /// <summary>
        /// 
        /// </summary>
        public ICollection<AudioSettings> SupportedSettings { get; } = new List<AudioSettings>();

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        protected Synthesizer()
        {
            Add(new AsyncAction("synthesize", async (command, cancellationToken) =>
            {
                var text = command.Input.Arguments.ElementAt(0);
                var settings = command.Input.Arguments.Length > 1
                    ? AudioSettings.Parse(command.Input.Arguments.ElementAt(1))
                    : SupportedSettings.First();

                var bytes = await ConvertAsync(text, settings, cancellationToken).ConfigureAwait(false);

                return new Value(bytes, $"{settings}");
            }, "Arguments: text, audioSettings?"));
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="settings"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract Task<byte[]> ConvertAsync(
            string text, 
            AudioSettings? settings = null, 
            CancellationToken cancellationToken = default);

        #endregion
    }
}
