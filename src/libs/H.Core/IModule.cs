using System;
using System.Collections.Generic;
using H.Core.Storages;
using H.Core.Utilities;

namespace H.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModule : IDisposable
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// 
        /// </summary>
        string ShortName { get; }
        
        /// <summary>
        /// 
        /// </summary>
        string UniqueName { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        bool IsRegistered { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        string Description { get; }
        
        /// <summary>
        /// 
        /// </summary>
        ISettingsStorage Settings { get; }

        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<ICommand>? CommandReceived;

        /// <summary>
        /// 
        /// </summary>
        event AsyncEventHandler<ICommand>? AsyncCommandReceived;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<Exception>? ExceptionOccurred;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<string>? LogReceived;

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ICollection<string> GetAvailableSettings();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetSetting(string key, object value);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object? GetSetting(string key);
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool IsValid();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string[] GetSupportedVariables();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        object? GetModuleVariableValue(string name);

        #endregion
    }
}
