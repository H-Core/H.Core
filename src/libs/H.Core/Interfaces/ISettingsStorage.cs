using System.ComponentModel;
using H.Core.Settings;

namespace H.Core
{
    public interface ISettingsStorage : IStorage<Setting>, INotifyPropertyChanged
    {
        void Set(string key, object value);
    }
}
