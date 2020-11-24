using System;

namespace H.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AllowMultipleInstanceAttribute : Attribute
    {
        public bool AutoCreateInstance { get; }

        public AllowMultipleInstanceAttribute(bool autoCreateInstance)
        {
            AutoCreateInstance = autoCreateInstance;
        }
    }
}
