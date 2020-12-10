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

            recorder.RawDataReceived += OnRawDataReceived;
            recorder.Stopped += (_, _) => recorder.RawDataReceived -= OnRawDataReceived;
        }
    }
}
