using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Recorders
{
    public static class RecorderExtensions
    {
        public static async Task StartWithTimeoutAsync(this IRecorder recorder, TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            recorder = recorder ?? throw new ArgumentNullException(nameof(recorder));
            
            if (!recorder.IsInitialized)
            {
                await recorder.InitializeAsync(cancellationToken).ConfigureAwait(false);
            }

            await recorder.StartAsync(cancellationToken).ConfigureAwait(false);

            await Task.Delay(timeout, cancellationToken).ConfigureAwait(false);

            await recorder.StopAsync(cancellationToken).ConfigureAwait(false);
        }

        public static async Task ChangeWithTimeoutAsync(this IRecorder recorder, TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            recorder = recorder ?? throw new ArgumentNullException(nameof(recorder));

            if (!recorder.IsInitialized)
            {
                await recorder.InitializeAsync(cancellationToken).ConfigureAwait(false);
            }

            if (!recorder.IsStarted)
            {
                await recorder.StartWithTimeoutAsync(timeout, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                await recorder.StopAsync(cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
