using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Recorders
{
    public class ParentRecorder : Recorder
    {
        #region Properties

        public IRecorder? Recorder { get; protected set; }

        #endregion

        #region Constructors

        #endregion

        #region Public methods

        public override async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            if (Recorder == null || Recorder.IsInitialized)
            {
                return;
            }

            await Recorder.InitializeAsync(cancellationToken).ConfigureAwait(false);
        }

        public override async Task StartAsync(CancellationToken cancellationToken = default)
        {
            if (IsStarted)
            {
                return;
            }

            if (Recorder == null)
            {
                OnLogReceived("Recorder is not found");
                return;
            }

            if (!Recorder.IsInitialized)
            {
                await Recorder.InitializeAsync(cancellationToken).ConfigureAwait(false);
            }

            await Recorder.StartAsync(cancellationToken).ConfigureAwait(false);
            await base.StartAsync(cancellationToken).ConfigureAwait(false);
        }

        public override async Task StopAsync(CancellationToken cancellationToken = default)
        {
            if (!IsStarted)
            {
                return;
            }

            if (Recorder == null)
            {
                OnLogReceived("Recorder is not found");
                return;
            }

            await Recorder.StopAsync(cancellationToken).ConfigureAwait(false);

            RawData = Recorder.RawData;
            WavData = Recorder.WavData;

            await base.StopAsync(cancellationToken).ConfigureAwait(false);
        }

        #endregion

        #region IDisposable

        public override void Dispose()
        {
            base.Dispose();

            Recorder?.Dispose();
            Recorder = null;
        }

        #endregion
    }
}
