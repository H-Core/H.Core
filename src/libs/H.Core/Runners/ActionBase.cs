using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Runners
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ActionBase : IAction
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public bool IsCancellable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsInternal { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ICommand>? Running;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ICommand>? Ran;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        protected void OnRunning(ICommand value)
        {
            Running?.Invoke(this, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        protected void OnRan(ICommand value)
        {
            Ran?.Invoke(this, value);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        protected ActionBase(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract Task<IValue> RunAsync(ICommand command, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ICall PrepareCall(ICommand command)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));

            return new Call(this, command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name} {Description}";
        }

        #endregion
    }
}
