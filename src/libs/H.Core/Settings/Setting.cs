﻿using System;

namespace H.Core.Settings
{
    public class Setting
    {
        public string Key { get; set; } = string.Empty;
        public object? Value { get; set; }
        public object? DefaultValue { get; set; }
        public SettingType SettingType { get; set; }

        public Type? Type => SettingType == SettingType.Enumerable
            ? DefaultValue?.GetType().GetElementType()
            : DefaultValue?.GetType();

        public Func<object?, bool>? CheckFunc { get; set; }
        public bool IsValid() => CheckFunc?.Invoke(Value) ?? true;

        public Action<object?>? SetAction { get; set; }
        public void Set() => SetAction?.Invoke(Value);
    }
}
