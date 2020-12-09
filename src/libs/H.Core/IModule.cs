using System;
using System.Collections.Generic;
using H.Core.Storages;
using H.Core.Utilities;

namespace H.Core
{
    public interface IModule : IDisposable
    {
        string Name { get; }
        string ShortName { get; }
        string UniqueName { get; set; }
        bool IsRegistered { get; set; }
        string Description { get; }

        ISettingsStorage Settings { get; }
        ICollection<string> GetAvailableSettings();
        void SetSetting(string key, object value);
        object? GetSetting(string key);
        bool IsValid();

        event EventHandler<string>? NewCommand;
        event EventHandler<TextDeferredEventArgs>? NewCommandAsync;
        event EventHandler<IModule>? SettingsSaved;
        event EventHandler<Exception>? ExceptionOccurred;
        event EventHandler<string>? LogReceived;

        void SaveSettings();

        string[] GetSupportedVariables();
        object? GetModuleVariableValue(string name);
    }
}
