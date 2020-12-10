using H.Core.Utilities;

namespace H.Core.Storages
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InvariantDictionaryStorage<T> : InvariantStringDictionary<T>, IStorage<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual void Load() { }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual void Save() { }
    }
}
