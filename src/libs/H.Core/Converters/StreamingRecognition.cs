using System;
using System.Threading;
using System.Threading.Tasks;
using H.Core.Utilities;

namespace H.Core.Converters
{
    public abstract class StreamingRecognition : DisposableObject, IStreamingRecognition
    {
        #region Events

        public event EventHandler<string>? PartialResultsReceived;
        public event EventHandler<string>? FinalResultsReceived;

        protected void OnPartialResultsReceived(string value)
        {
            PartialResultsReceived?.Invoke(this, value);
        }

        protected void OnFinalResultsReceived(string value)
        {
            FinalResultsReceived?.Invoke(this, value);
        }

        #endregion

        #region Public methods

        public abstract Task WriteAsync(byte[] bytes, CancellationToken cancellationToken = default);
        public abstract Task StopAsync(CancellationToken cancellationToken = default);

        #endregion
    }
}
