using System.Collections.Generic;
using System.ComponentModel;
using H.Core.Settings;

namespace H.Core.Storages
{
    /// <summary>
    /// 
    /// </summary>
    public class SettingsStorage : Dictionary<string, Setting>, ISettingsStorage
    {
        #region INotifyPropertyChanged

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public new Setting this[string key]
        {
            get => base[key];
            set
            {
                base[key] = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(key));

                value?.Set();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set(string key, object value)
        {
            if (!TryGetValue(key, out var thisSetting))
            {
                return;
            }

            thisSetting.Value = value;
            thisSetting.Set();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(key));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public new bool Remove(string key)
        {
            var result = base.Remove(key);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(key));

            return result;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void Load() { }
        
        /// <summary>
        /// 
        /// </summary>
        public void Save() { }
    }
}
