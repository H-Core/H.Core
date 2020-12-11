using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using H.Core.Recorders;

namespace H.Core.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public static class StreamRecognitionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recognition"></param>
        /// <param name="recorder"></param>
        /// <param name="writeWavHeader"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static async Task BindRecorderAsync(this IStreamingRecognition recognition, IRecorder recorder, bool writeWavHeader = false, CancellationToken cancellationToken = default)
        {
            recognition = recognition ?? throw new ArgumentNullException(nameof(recognition));
            recorder = recorder ?? throw new ArgumentNullException(nameof(recorder));

            if (writeWavHeader)
            {
                if (!recorder.WavHeader.Any())
                {
                    throw new InvalidOperationException("recorder.WavHeader is empty.");
                }

                await recognition.WriteAsync(recorder.WavHeader, cancellationToken).ConfigureAwait(false);
            }

            if (recorder.RawData.Any())
            {
                await recognition.WriteAsync(recorder.RawData, cancellationToken).ConfigureAwait(false);
            }

            async void OnRawDataReceived(object? _, byte[] bytes)
            {
                await recognition.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
            }

            void OnStopped(object? o, EventArgs eventArgs)
            {
                recorder.RawDataReceived -= OnRawDataReceived;
                recorder.Stopped -= OnStopped;
            }

            recorder.RawDataReceived += OnRawDataReceived;
            recorder.Stopped += OnStopped;
        }

        /// <summary>
        /// Dispose is required!.
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="recorder"></param>
        /// <param name="writeWavHeader"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static async Task<IStreamingRecognition> StartStreamingRecognitionAsync(
            this IConverter converter, 
            IRecorder recorder,
            bool writeWavHeader = false, 
            CancellationToken cancellationToken = default)
        {
            converter = converter ?? throw new ArgumentNullException(nameof(converter));
            recorder = recorder ?? throw new ArgumentNullException(nameof(recorder));

            await recorder.StartAsync(cancellationToken).ConfigureAwait(false);

            var recognition = await converter.StartStreamingRecognitionAsync(cancellationToken).ConfigureAwait(false);
            recognition.Stopping += async (_, _) =>
            {
                await recorder.StopAsync(cancellationToken).ConfigureAwait(false);
            };

            await recognition.BindRecorderAsync(recorder, writeWavHeader, cancellationToken).ConfigureAwait(false);

            return recognition;
        }
    }
}
