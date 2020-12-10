using System;

namespace H.Core.Storages
{
    /// <summary>
    /// 
    /// </summary>
    public static class StorageExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T? GetOrAdd<T>(this IStorage<T?> storage, string key, T value = default)
        {
            if (storage == null)
            {
                throw new ArgumentNullException(nameof(storage));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (!storage.ContainsKey(key))
            {
                storage[key] = value;
            }

            return storage[key];
        }
    }
}
