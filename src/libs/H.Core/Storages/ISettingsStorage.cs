using System.ComponentModel;
using H.Core.Settings;

namespace H.Core.Storages
{
    public interface ISettingsStorage : IStorage<Setting>, INotifyPropertyChanged
    {
        void Set(string key, object value);
    }
}
