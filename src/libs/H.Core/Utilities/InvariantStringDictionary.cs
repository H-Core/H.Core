using System;
using System.Collections.Generic;

namespace H.Core.Utilities
{
    public class InvariantStringDictionary<T> : Dictionary<string, T>
    {
        #region Public methods

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

        public new bool ContainsKey(string key)
        {
            key = key ?? throw new ArgumentNullException(nameof(key));
            
            return base.ContainsKey(ToInvariantString(key));
        }

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

        public new bool Remove(string key)
        {
            key = key ?? throw new ArgumentNullException(nameof(key));
            
            return base.Remove(ToInvariantString(key));
        }

        #endregion

        #region Private methods

        private static string ToInvariantString(string text) => text.ToUpperInvariant();

        #endregion
    }
}
