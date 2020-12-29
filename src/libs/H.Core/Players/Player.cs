using System;
using System.Threading;
using System.Threading.Tasks;
using H.Core.Runners;

namespace H.Core.Players
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Player : Runner, IPlayer
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        protected Player()
        {
            Add(new AsyncAction("play", async (command, cancellationToken) =>
            {
                var format = Enum.TryParse<AudioFormat>(
                    command.Input.Argument, true, out var result) 
                    ? result
                    : AudioFormat.Raw;
                var bytes = command.Input.Data;

                await PlayAsync(bytes, format, cancellationToken).ConfigureAwait(false);

                return Value.Empty;
            }, "Data: bytes, Arguments: audioFormat"));
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="format"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract Task PlayAsync(
            byte[] bytes,
            AudioFormat format = AudioFormat.Raw,
            CancellationToken cancellationToken = default);

        #endregion
    }
}
