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
        AudioFormat Format { get; }

        /// <summary>
        /// 
        /// </summary>
        AudioFormat StreamingFormat { get; }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> ConvertAsync(byte[] bytes, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IStreamingRecognition> StartStreamingRecognitionAsync(CancellationToken cancellationToken = default);

        #endregion
    }
}
