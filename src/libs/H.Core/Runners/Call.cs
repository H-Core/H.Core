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
        public IAction Action { get; }

        /// <summary>
        /// 
        /// </summary>
        public string[] Arguments { get; }

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
        /// <param name="action"></param>
        /// <param name="arguments"></param>
        public Call(IAction action, string[] arguments)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
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
            
            await Action.RunAsync(Arguments, cancellationToken).ConfigureAwait(false);
            
            OnRan();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Action.Name} {string.Join(" ", Arguments)}";
        }

        #endregion
    }
}
