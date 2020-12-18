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
        public RecordingFormat Format { get; }

        /// <summary>
        /// 
        /// </summary>
        public RecordingFormat StreamingFormat { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="streamingFormat"></param>
        protected Recognizer(RecordingFormat format, RecordingFormat streamingFormat = RecordingFormat.None)
        {
            Format = format;
            StreamingFormat = streamingFormat;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<string> ConvertAsync(byte[] bytes, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<IStreamingRecognition> StartStreamingRecognitionAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
