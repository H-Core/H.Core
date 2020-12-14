using System;
using System.Collections.Generic;

namespace H.Core.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InvariantStringDictionary<T> : Dictionary<string, T>
    {
        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public new T this[string key]
        {
            get
            {
                key = key ?? throw new ArgumentNullException(nameof(key));

                return base[ToInvariantString(key)];
            }
            set
            {
                key = key ?? throw new ArgumentNullException(nameof(key));

                base[ToInvariantString(key)] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public new bool ContainsKey(string key)
        {
            key = key ?? throw new ArgumentNullException(nameof(key));
            
            return base.ContainsKey(ToInvariantString(key));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public new bool TryGetValue(string key, out T value)
        {
            if (!ContainsKey(key))
            {
                value = default!;
                return false;
            }
            
            value = this[key];
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public new bool Remove(string key)
        {
            key = key ?? throw new ArgumentNullException(nameof(key));
            
            return base.Remove(ToInvariantString(key));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public new void Add(string key, T value)
        {
            key = key ?? throw new ArgumentNullException(nameof(key));

            base.Add(ToInvariantString(key), value);
        }

        #endregion

        #region Private methods

        private static string ToInvariantString(string text) => text.ToUpperInvariant();

        #endregion
    }
}
