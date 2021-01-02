using System.Collections.Generic;
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
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public ICollection<AudioSettings> SupportedSettings { get; } = new List<AudioSettings>();

        #endregion


        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        protected Player()
        {
            Add(new AsyncAction("play", async (command, cancellationToken) =>
            {
                var format = AudioSettings.Parse(command.Input.Argument);
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
        /// <param name="settings"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract Task PlayAsync(
            byte[] bytes,
            AudioSettings? settings = null,
            CancellationToken cancellationToken = default);

        #endregion
    }
}
