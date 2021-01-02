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
    public static class StreamingRecognitionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recognition"></param>
        /// <param name="recording"></param>
        /// <param name="exceptionsBag"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static async Task BindRecordingAsync(
            this IStreamingRecognition recognition,
            IRecording recording,
            ExceptionsBag? exceptionsBag = null,
            CancellationToken cancellationToken = default)
        {
            recognition = recognition ?? throw new ArgumentNullException(nameof(recognition));
            recording = recording ?? throw new ArgumentNullException(nameof(recording));

            if (recording.Settings.Format is not AudioFormat.Raw)
            {
                if (!recording.Header.Any())
                {
                    throw new ArgumentException("recording.Header is empty.");
                }

                await recognition.WriteAsync(recording.Header, cancellationToken).ConfigureAwait(false);
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

            void OnStopped(object? o, byte[] bytes)
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
        /// <param name="exceptionsBag"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static async Task<IStreamingRecognition> StartStreamingRecognitionAsync(
            this IRecognizer recognizer,
            IRecorder recorder,
            ExceptionsBag? exceptionsBag = null,
            CancellationToken cancellationToken = default)
        {
            recognizer = recognizer ?? throw new ArgumentNullException(nameof(recognizer));
            recorder = recorder ?? throw new ArgumentNullException(nameof(recorder));

            if (!recognizer.SupportedStreamingSettings.Any())
            {
                throw new ArgumentException("Recognizer does not support streaming recognition.");
            }

            var settings = recognizer.SupportedStreamingSettings.First();
            var recording = await recorder.StartAsync(settings, cancellationToken)
                .ConfigureAwait(false);
            var recognition = await recognizer.StartStreamingRecognitionAsync(settings, cancellationToken)
                .ConfigureAwait(false);
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

            await recognition.BindRecordingAsync(recording, exceptionsBag, cancellationToken).ConfigureAwait(false);

            return recognition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recognizer"></param>
        /// <param name="recorder"></param>
        /// <param name="process"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static Task<IProcess> StartStreamingRecognitionAsync(
            this IRecognizer recognizer,
            IRecorder recorder,
            Process? process = null,
            CancellationToken cancellationToken = default)
        {
            recognizer = recognizer ?? throw new ArgumentNullException(nameof(recognizer));
            recorder = recorder ?? throw new ArgumentNullException(nameof(recorder));
            if (!recognizer.SupportedStreamingSettings.Any())
            {
                throw new ArgumentException("Recognizer does not support streaming recognition.");
            }

            process ??= new Process();
            process.Initialize(async () =>
            {
                var settings = recognizer.SupportedStreamingSettings.First();
                using var recording = await recorder.StartAsync(settings, cancellationToken)
                    .ConfigureAwait(false);
                using var recognition = await recognizer.StartStreamingRecognitionAsync(settings, cancellationToken)
                    .ConfigureAwait(false);

                await recognition.BindRecordingAsync(recording, process.Exceptions, cancellationToken).ConfigureAwait(false);

                await process.WaitAsync(cancellationToken).ConfigureAwait(false);

                await recording.StopAsync(cancellationToken).ConfigureAwait(false);
                await recognition.StopAsync(cancellationToken).ConfigureAwait(false);
            }, cancellationToken);

            return Task.FromResult<IProcess>(process);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recognizer"></param>
        /// <param name="bytes"></param>
        /// <param name="settings"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<string> ConvertOverStreamingRecognition(
            this IRecognizer recognizer,
            byte[] bytes,
            AudioSettings? settings = null,
            CancellationToken cancellationToken = default)
        {
            recognizer = recognizer ?? throw new ArgumentNullException(nameof(recognizer));
            bytes = bytes ?? throw new ArgumentNullException(nameof(bytes));
            
            using var recognition = await recognizer.StartStreamingRecognitionAsync(settings, cancellationToken)
                .ConfigureAwait(false);

            await recognition.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
            
            return await recognition.StopAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
