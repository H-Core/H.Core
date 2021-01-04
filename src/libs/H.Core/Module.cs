using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using H.Core.Settings;
using H.Core.Storages;
using H.Core.Utilities;

namespace H.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class Module : IModule
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ShortName => GetType().Name;
        
        /// <summary>
        /// 
        /// </summary>
        public string UniqueName { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public bool IsRegistered { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public ISettingsStorage Settings { get; } = new SettingsStorage();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsValid() => Settings.All(entry => entry.Value.IsValid());

        /// <summary>
        /// 
        /// </summary>
        protected InvariantStringDictionary<Func<object?>> Variables { get; } = new ();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetSupportedVariables() => Variables.Keys.ToArray();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        protected void AddVariable(string key, Func<object> action) => Variables[key] = action;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object? GetModuleVariableValue(string name) => Variables.TryGetValue(name, out var func) ? func() : null;

        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ICommand>? CommandReceived;
        
        /// <summary>
        /// 
        /// </summary>
        public event AsyncEventHandler<ICommand, IValue>? AsyncCommandReceived;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        protected void OnCommandReceived(ICommand value)
        {
            CommandReceived?.Invoke(this, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cancellationToken"></param>
        protected Task<IValue[]> OnAsyncCommandReceivedAsync(ICommand value, CancellationToken cancellationToken = default)
        {
            return AsyncCommandReceived.InvokeAsync(this, value, cancellationToken);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<Exception>? ExceptionOccurred;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        protected void OnExceptionOccurred(Exception value)
        {
            ExceptionOccurred?.Invoke(this, value);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        protected Module()
        {
            Name = GetType().FullName;
            UniqueName = GetType().Name;

            Settings.PropertyChanged += (_, args) =>
            {
                var key = args.PropertyName;
                if (!Settings.ContainsKey(key))
                {
                    OnExceptionOccurred(new InvalidOperationException($"Settings is not exists: {key}"));
                }
            };
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="arguments"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Run(string name, params string[] arguments)
        {
            name = name ?? throw new ArgumentNullException(nameof(name));
            
            Run(new Command(name, arguments));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Run(ICommand command)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));

            if (command.IsEmpty)
            {
                return;
            }
            
            OnCommandReceived(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IValue[]> RunAsync(ICommand command, CancellationToken cancellationToken = default)
        {
            return await OnAsyncCommandReceivedAsync(command, cancellationToken).ConfigureAwait(false);
        }

        #endregion

        #region Private/protected methods

        #region Settings

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ICollection<string> GetAvailableSettings()
        {
            return Settings.Keys;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetSetting(string key, object value)
        {
            Settings.Set(key, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object? GetSetting(string key)
        {
            return Settings[key].Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="setAction"></param>
        /// <param name="checkFunc"></param>
        /// <param name="defaultValue"></param>
        /// <param name="type"></param>
        protected void AddSetting<T>(string key, Action<T> setAction, Func<T, bool> checkFunc, T defaultValue, SettingType type = SettingType.Default)
        {
            key = key ?? throw new ArgumentNullException(nameof(key));
            Settings[key] = new Setting
            {
                Key = key,
                Value = defaultValue,
                DefaultValue = defaultValue,
                SettingType = type,
                CheckFunc = o => CanConvert<T>(o) && checkFunc.Invoke(ConvertTo<T>(o)),
                SetAction = o => setAction.Invoke(ConvertTo<T>(o))
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="setAction"></param>
        /// <param name="checkFunc"></param>
        /// <param name="defaultValues"></param>
        protected void AddEnumerableSetting<T>(string key, Action<T> setAction, Func<T, bool> checkFunc, T[] defaultValues)
        {
            key = key ?? throw new ArgumentNullException(nameof(key));
            Settings[key] = new Setting
            {
                Key = key,
                Value = defaultValues.ElementAtOrDefault(0),
                DefaultValue = defaultValues,
                SettingType = SettingType.Enumerable,
                CheckFunc = o => CanConvert<T>(o) && checkFunc.Invoke(ConvertTo<T>(o)),
                SetAction = o => setAction.Invoke(ConvertTo<T>(o))
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected static bool IsNull(string? key) => key == null;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected static bool NoEmpty(string key) => !string.IsNullOrEmpty(key);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        protected static bool Any<T>(T _) => true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected static bool IsNull(int key) => key == 0;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected static bool Positive(int key) => key > 0;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected static bool Negative(int key) => key < 0;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected static bool NotNegative(int key) => key >= 0;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected static bool NotPositive(int key) => key <= 0;


        #endregion

        private static bool CanConvert<T>(object? value)
        {
            try
            {
                var unused = Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static T ConvertTo<T>(object? value) => (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);

        #endregion

        #region IDisposable

        /// <summary>
        /// 
        /// </summary>
        public virtual void Dispose()
        {
        }

        #endregion
    }
}
