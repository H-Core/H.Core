using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Runners
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommand
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        bool IsInternal { get; }
        
        /// <summary>
        /// 
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsCancellable { get; }

        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<string[]>? Running;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<string[]>? Ran;

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ICall PrepareCall(params string[] arguments);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RunAsync(string[] arguments, CancellationToken cancellationToken = default);

        #endregion
    }
}
