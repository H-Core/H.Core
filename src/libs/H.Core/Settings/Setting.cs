using System;

namespace H.Core.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; } = string.Empty;
        
        /// <summary>
        /// 
        /// </summary>
        public object? Value { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public object? DefaultValue { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public SettingType SettingType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Type? Type => SettingType == SettingType.Enumerable
            ? DefaultValue?.GetType().GetElementType()
            : DefaultValue?.GetType();

        /// <summary>
        /// 
        /// </summary>
        public Func<object?, bool>? CheckFunc { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsValid() => CheckFunc?.Invoke(Value) ?? true;

        /// <summary>
        /// 
        /// </summary>
        public Action<object?>? SetAction { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public void Set() => SetAction?.Invoke(Value);
    }
}
