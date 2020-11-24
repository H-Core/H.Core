using System.Collections.Generic;
using System.Threading.Tasks;

namespace H.Core
{
    public interface ISearcher : IModule
    {
        Task<List<string>> Search(string query);
    }
}
