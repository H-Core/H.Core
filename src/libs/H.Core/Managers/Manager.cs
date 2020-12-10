using System;
using H.Core.Storages;

namespace H.Core.Managers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Manager<T> : BaseManager
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public IStorage<T> Storage { get; }

        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<string?>? NotHandledText;
        
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<string>? HandledText;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public delegate void ValueDelegate(string key, T value);
        
        /// <summary>
        /// 
        /// </summary>
        public event ValueDelegate? NewValue;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storage"></param>
        public Manager(IStorage<T>? storage = null)
        {
            Storage = storage ?? new InvariantDictionaryStorage<T>();
            Storage.Load();

            NewText += OnNewText;
        }

        #endregion

        #region Event handlers

        private void OnNewText(object? sender, string? text)
        {
            if (text == null ||
                string.IsNullOrWhiteSpace(text) ||
                !Storage.ContainsKey(text))
            {
                NotHandledText?.Invoke(this, text);
                return;
            }

            HandledText?.Invoke(this, text);

            var value = Storage[text];
            NewValue?.Invoke(text, value);
        }

        #endregion
    }
}
