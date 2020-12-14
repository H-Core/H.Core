using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Runners
{
    /// <summary>
    /// 
    /// </summary>
    public class Call : ICall
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public ICommand Command { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Arguments { get; }

        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? Running;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? Ran;

        private void OnRunning()
        {
            Running?.Invoke(this, EventArgs.Empty);
        }
        
        private void OnRan()
        {
            Ran?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="arguments"></param>
        public Call(ICommand command, string arguments)
        {
            Command = command ?? throw new ArgumentNullException(nameof(command));
            Arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task RunAsync(CancellationToken cancellationToken = default)
        {
            OnRunning();
            
            await Command.RunAsync(Arguments, cancellationToken).ConfigureAwait(false);
            
            OnRan();
        }

        #endregion
    }
}
