using System;

namespace H.Core.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class AllowMultipleInstanceAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public bool AutoCreateInstance { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="autoCreateInstance"></param>
        public AllowMultipleInstanceAttribute(bool autoCreateInstance)
        {
            AutoCreateInstance = autoCreateInstance;
        }
    }
}
