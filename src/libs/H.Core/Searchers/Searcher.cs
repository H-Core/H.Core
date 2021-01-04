using System.Collections.Generic;
using System.Linq;
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
            Add(AsyncAction.WithSingleArgument("search", async (query, cancellationToken) =>
            {
                var results = await SearchAsync(query, cancellationToken)
                    .ConfigureAwait(false);

                return new Value(results.Select(result => result.Url).ToArray());
            }, "query"));
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract Task<ICollection<SearchResult>> SearchAsync(
            string query, 
            CancellationToken cancellationToken = default);

        #endregion
    }
}
