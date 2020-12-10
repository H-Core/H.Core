using System.Collections.Generic;
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
        /// <returns></returns>
        Task<List<string>> Search(string query);
    }
}
