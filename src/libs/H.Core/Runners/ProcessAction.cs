using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Runners
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessAction : ActionBase, IProcessAction
    {
        #region Properties

        private Func<IProcess<IValue>, ICommand, CancellationToken, Task<IValue>> Action { get; }
        
        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ProcessAction(
            string name, 
            Func<IProcess<IValue>, ICommand, CancellationToken, Task<IValue>> action,
            string? description = null,
            bool isInternal = false) : 
            base(name)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
            Description = description ?? string.Empty;
            IsInternal = isInternal;

            IsCancellable = true;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="process"></param>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public async Task<IValue> RunAsync(IProcess<IValue> process, ICommand command, CancellationToken cancellationToken = default)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));

            OnRunning(command);

            var output = IsCancellable
                ? await Action(process, command, cancellationToken)
                    .ConfigureAwait(false)
                : await Task.Run(() => Action(process, command, cancellationToken), cancellationToken)
                    .ConfigureAwait(false);
            
            OnRan(command);

            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="process"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public ICall PrepareCall(IProcess<IValue> process, ICommand command)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));

            return new Call(this, command);
        }

        #endregion
    }
}
