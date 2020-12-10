using System;
using System.Reflection;
using H.Core.Attributes;

namespace H.Core.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ModuleExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool AllowMultipleInstance(this Type type) => 
            type.GetTypeInfo().GetCustomAttribute<AllowMultipleInstanceAttribute>() != null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public static bool AllowMultipleInstance(this IModule module)
        {
            module = module ?? throw new ArgumentNullException(nameof(module));
            
            return module.GetType().AllowMultipleInstance();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool AutoCreateInstance(this Type type)
        {
            var attribute = type.GetTypeInfo().GetCustomAttribute<AllowMultipleInstanceAttribute>();

            return attribute?.AutoCreateInstance ?? true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public static bool AutoCreateInstance(this IModule module)
        {
            module = module ?? throw new ArgumentNullException(nameof(module));
            
            return module.GetType().AutoCreateInstance();
        }
    }
}
