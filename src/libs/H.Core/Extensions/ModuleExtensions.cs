using System;
using System.Reflection;
using H.Core.Attributes;

namespace H.Core.Extensions
{
    public static class ModuleExtensions
    {
        public static bool AllowMultipleInstance(this Type type) => 
            type.GetTypeInfo().GetCustomAttribute<AllowMultipleInstanceAttribute>() != null;

        public static bool AllowMultipleInstance(this IModule module)
        {
            module = module ?? throw new ArgumentNullException(nameof(module));
            
            return module.GetType().AllowMultipleInstance();
        }

        public static bool AutoCreateInstance(this Type type)
        {
            var attribute = type.GetTypeInfo().GetCustomAttribute<AllowMultipleInstanceAttribute>();

            return attribute?.AutoCreateInstance ?? true;
        }

        public static bool AutoCreateInstance(this IModule module)
        {
            module = module ?? throw new ArgumentNullException(nameof(module));
            
            return module.GetType().AutoCreateInstance();
        }
    }
}
