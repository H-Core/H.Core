using System;

#pragma warning disable CA1054, CA1056, 1572, 1573, 1591

namespace H.Core.Searchers
{
    /// <summary>
    /// Represents a search result from ISearcher's.
    /// </summary>
    /// <param name="Url"></param>
    /// <param name="Description"></param>
    [Serializable]
    public record SearchResult(string Url, string Description);
}

namespace System.Runtime.CompilerServices { internal class IsExternalInit { } }
