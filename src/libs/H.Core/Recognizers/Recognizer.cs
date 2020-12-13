using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Recognizers
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Recognizer : Module, IRecognizer
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public bool IsStreamingRecognitionSupported => false;

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract Task<string> ConvertAsync(byte[] bytes, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<IStreamingRecognition> StartStreamingRecognitionAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async Task<string> ConvertOverStreamingRecognition(byte[] bytes, CancellationToken cancellationToken = default)
        {
            using var recognition = await StartStreamingRecognitionAsync(cancellationToken).ConfigureAwait(false);
            var response = string.Empty;
            recognition.FinalResultsReceived += (_, value) => response = value;

            await recognition.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
            await recognition.StopAsync(cancellationToken).ConfigureAwait(false);

            return response;
        }

        #endregion
    }
}
