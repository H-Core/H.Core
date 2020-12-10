using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Converters
{
    public abstract class Converter : Module, IConverter
    {
        #region Properties

        public bool IsStreamingRecognitionSupported => false;

        #endregion

        #region Methods

        public abstract Task<string> ConvertAsync(byte[] bytes, CancellationToken cancellationToken = default);

        public virtual Task<IStreamingRecognition> StartStreamingRecognitionAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

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
