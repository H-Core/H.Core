using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using H.Core.Players;

namespace H.Core.Synthesizers
{
    /// <summary>
    /// 
    /// </summary>
    public static class SynthesizerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="synthesizer"></param>
        /// <param name="player"></param>
        /// <param name="text"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static async Task PlayAsync(
            this ISynthesizer synthesizer,
            IPlayer player,
            string text,
            CancellationToken cancellationToken = default)
        {
            synthesizer = synthesizer ?? throw new ArgumentNullException(nameof(synthesizer));
            player = player ?? throw new ArgumentNullException(nameof(player));
            text = text ?? throw new ArgumentNullException(nameof(text));

            var settings = synthesizer.SupportedSettings.First();

            var bytes = await synthesizer.ConvertAsync(text, settings, cancellationToken)
                .ConfigureAwait(false);

            await player.PlayAsync(bytes, settings, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
