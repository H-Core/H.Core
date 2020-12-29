using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Searchers
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISearcher : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ICollection<SearchResult>> SearchAsync(string query, CancellationToken cancellationToken = default);
    }
}
