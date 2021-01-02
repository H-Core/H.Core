using System;
using System.Collections.Generic;
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
        public ICollection<AudioSettings> SupportedSettings { get; } = new List<AudioSettings>();

        /// <summary>
        /// 
        /// </summary>
        public ICollection<AudioSettings> SupportedStreamingSettings { get; } = new List<AudioSettings>();

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="settings"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<string> ConvertAsync(
            byte[] bytes, 
            AudioSettings? settings = null, 
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<IStreamingRecognition> StartStreamingRecognitionAsync(
            AudioSettings? settings = null, 
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
