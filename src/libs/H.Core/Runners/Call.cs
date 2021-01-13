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
        public ICommand Command { get; }

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
        /// <param name="command"></param>
        public Call(IAction action, ICommand command)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
            Command = command ?? throw new ArgumentNullException(nameof(command));
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ICommand> RunAsync(CancellationToken cancellationToken = default)
        {
            OnRunning();
            
            var output = await Action.RunAsync(Command, cancellationToken).ConfigureAwait(false);
            
            OnRan();

            return Command.WithOutput(output);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="process"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ICommand> RunAsync(IProcess<ICommand> process, CancellationToken cancellationToken = default)
        {
            if (Action is not IProcessAction processAction)
            {
                throw new InvalidOperationException("Action is not ProcessAction.");
            }
            
            OnRunning();

            var output = await processAction.RunAsync(process, Command, cancellationToken).ConfigureAwait(false);

            OnRan();

            return Command.WithOutput(output);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Command}";
        }

        #endregion
    }
}
