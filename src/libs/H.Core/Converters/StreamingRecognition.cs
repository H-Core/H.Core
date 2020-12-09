using System;
using System.Threading;
using System.Threading.Tasks;
using H.Core.Utilities;

namespace H.Core.Converters
{
    public abstract class StreamingRecognition : DisposableObject, IStreamingRecognition
    {
        #region Events

        public event EventHandler<string>? AfterPartialResults;
        public event EventHandler<string>? AfterFinalResults;

        protected void OnAfterPartialResults(string value)
        {
            AfterPartialResults?.Invoke(this, value);
        }

        protected void OnAfterFinalResults(string value)
        {
            AfterFinalResults?.Invoke(this, value);
        }

        #endregion

        #region Public methods

        public abstract Task WriteAsync(byte[] bytes, CancellationToken cancellationToken = default);
        public abstract Task StopAsync(CancellationToken cancellationToken = default);

        #endregion
    }
}
