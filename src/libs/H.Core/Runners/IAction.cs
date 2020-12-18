using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Runners
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAction
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsCancellable { get; }
        
        /// <summary>
        /// 
        /// </summary>
        bool IsInternal { get; }

        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<ICommand>? Running;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<ICommand>? Ran;

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        ICall PrepareCall(ICommand command);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IValue> RunAsync(ICommand command, CancellationToken cancellationToken = default);

        #endregion
    }
}
