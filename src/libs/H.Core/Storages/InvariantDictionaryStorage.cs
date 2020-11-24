using H.Core.Utilities;

namespace H.Core.Storages
{
    public class InvariantDictionaryStorage<T> : InvariantStringDictionary<T>, IStorage<T>
    {
        public virtual void Load() { }
        public virtual void Save() { }
    }
}
