using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Players
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPlayer : IModule
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        ICollection<AudioSettings> SupportedSettings { get; }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="settings"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task PlayAsync(byte[] bytes, AudioSettings? settings = null, CancellationToken cancellationToken = default);

        #endregion
    }
}
