using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Recorders
{
    /// <summary>
    /// 
    /// </summary>
    public static class RecorderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recorder"></param>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<byte[]> StartWithTimeoutAsync(this IRecorder recorder, TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            recorder = recorder ?? throw new ArgumentNullException(nameof(recorder));
            
            if (!recorder.IsInitialized)
            {
                await recorder.InitializeAsync(cancellationToken).ConfigureAwait(false);
            }

            using var recording = await recorder.StartAsync(cancellationToken).ConfigureAwait(false);

            await Task.Delay(timeout, cancellationToken).ConfigureAwait(false);

            await recording.StopAsync(cancellationToken).ConfigureAwait(false);

            return recording.Data;
        }
    }
}
