using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Recorders
{
    /// <summary>
    /// 
    /// </summary>
    public class ParentRecorder : Recorder
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public IRecorder? Recorder { get; protected set; }

        #endregion

        #region Constructors

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            if (Recorder == null || Recorder.IsInitialized)
            {
                return;
            }

            await Recorder.InitializeAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();

            Recorder?.Dispose();
            Recorder = null;
        }

        #endregion
    }
}
