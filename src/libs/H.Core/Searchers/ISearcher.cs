using System.Collections.Generic;
using System.Threading.Tasks;

namespace H.Core.Searchers
{
    public interface ISearcher : IModule
    {
        Task<List<string>> Search(string query);
    }
}
