using System.ComponentModel;
using H.Core.Settings;

namespace H.Core.Storages
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISettingsStorage : IStorage<Setting>, INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Set(string key, object value);
    }
}
