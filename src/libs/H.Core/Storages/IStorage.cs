using System.Collections.Generic;

namespace H.Core.Storages
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStorage<T> : IDictionary<string, T> //IEnumerable<KeyValuePair<string, T>>
    {
        //T this[string key] { get; set; }
        //bool ContainsKey(string key);
        //bool TryGetValue(string key, out T value);
        //bool Remove(string key);

        /// <summary>
        /// 
        /// </summary>
        void Load();
        
        /// <summary>
        /// 
        /// </summary>
        void Save();
    }
}
