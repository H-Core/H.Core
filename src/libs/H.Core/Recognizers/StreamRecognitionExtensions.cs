using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using H.Core.Recorders;
using H.Core.Utilities;

namespace H.Core.Recognizers
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
        /// <param name="recording"></param>
        /// <param name="writeWavHeader"></param>
        /// <param name="exceptionsBag"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static async Task BindRecordingAsync(
            this IStreamingRecognition recognition, 
            IRecording recording, 
            bool writeWavHeader = false,
            ExceptionsBag? exceptionsBag = null,
            CancellationToken cancellationToken = default)
        {
            recognition = recognition ?? throw new ArgumentNullException(nameof(recognition));
            recording = recording ?? throw new ArgumentNullException(nameof(recording));

            if (writeWavHeader)
            {
                if (!recording.WavHeader.Any())
                {
                    throw new InvalidOperationException("recorder.WavHeader is empty.");
                }

                await recognition.WriteAsync(recording.WavHeader, cancellationToken).ConfigureAwait(false);
            }

            if (recording.Data.Any())
            {
                await recognition.WriteAsync(recording.Data, cancellationToken).ConfigureAwait(false);
            }

            async void OnDataReceived(object? _, byte[] bytes)
            {
                try
                {
                    await recognition.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    exceptionsBag?.OnOccurred(exception);
                }
            }

            void OnStopped(object? o, EventArgs eventArgs)
            {
                try
                {
                    recording.DataReceived -= OnDataReceived;
                    recording.Stopped -= OnStopped;
                }
                catch (Exception exception)
                {
                    exceptionsBag?.OnOccurred(exception);
                }
            }

            recording.DataReceived += OnDataReceived;
            recording.Stopped += OnStopped;
        }

        /// <summary>
        /// Dispose is required!.
        /// </summary>
        /// <param name="recognizer"></param>
        /// <param name="recorder"></param>
        /// <param name="writeWavHeader"></param>
        /// <param name="exceptionsBag"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static async Task<IStreamingRecognition> StartStreamingRecognitionAsync(
            this IRecognizer recognizer, 
            IRecorder recorder,
            bool writeWavHeader = false,
            ExceptionsBag? exceptionsBag = null,
            CancellationToken cancellationToken = default)
        {
            recognizer = recognizer ?? throw new ArgumentNullException(nameof(recognizer));
            recorder = recorder ?? throw new ArgumentNullException(nameof(recorder));

            var recording = await recorder.StartAsync(cancellationToken).ConfigureAwait(false);

            var recognition = await recognizer.StartStreamingRecognitionAsync(cancellationToken).ConfigureAwait(false);
            recognition.Stopping += async (_, _) =>
            {
                try
                {
                    await recording.StopAsync(cancellationToken).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    exceptionsBag?.OnOccurred(exception);
                }
                finally
                {
                    recording.Dispose();
                }
            };

            await recognition.BindRecordingAsync(recording, writeWavHeader, exceptionsBag, cancellationToken).ConfigureAwait(false);

            return recognition;
        }
    }
}
