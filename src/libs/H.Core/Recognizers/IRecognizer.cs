using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Recognizers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRecognizer : IModule
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        ICollection<AudioSettings> SupportedSettings { get; }

        /// <summary>
        /// 
        /// </summary>
        ICollection<AudioSettings> SupportedStreamingSettings { get; }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="settings"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> ConvertAsync(
            byte[] bytes, 
            AudioSettings? settings = null, 
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IStreamingRecognition> StartStreamingRecognitionAsync(
            AudioSettings? settings = null, 
            CancellationToken cancellationToken = default);

        #endregion
    }
}
