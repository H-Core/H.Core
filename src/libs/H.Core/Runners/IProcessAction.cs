using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Runners
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProcessAction : IAction
    {
        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="process"></param>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IValue> RunAsync(IProcess<IValue> process, ICommand command, CancellationToken cancellationToken = default);

        #endregion
    }
}
