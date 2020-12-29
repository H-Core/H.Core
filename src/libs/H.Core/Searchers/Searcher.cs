using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using H.Core.Runners;

namespace H.Core.Searchers
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Searcher : Runner, ISearcher
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        protected Searcher()
        {
            Add(AsyncAction.WithSingleArgument("search", SearchAsync, "text"));
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract Task<ICollection<SearchResult>> SearchAsync(
            string text, 
            CancellationToken cancellationToken = default);

        #endregion
    }
}
