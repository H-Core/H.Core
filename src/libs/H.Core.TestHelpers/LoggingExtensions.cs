using System;
using System.Threading.Tasks;
using H.Core.Utilities;

namespace H.Core.TestHelpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class LoggingExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="module"></param>
        /// <returns></returns>
        public static T WithLogging<T>(this T module) where T : IModule
        {
            module = module ?? throw new ArgumentNullException(nameof(module));
            module.CommandReceived += (_, value) =>
            {
                Console.WriteLine($"{nameof(module.CommandReceived)}: {value}");
            };
            module.AsyncCommandReceived += (_, value, _) =>
            {
                Console.WriteLine($"{nameof(module.AsyncCommandReceived)}: {value}");

                return Task.FromResult(EmptyArray<IValue>.Value);
            };
            module.ExceptionOccurred += (_, value) =>
            {
                Console.WriteLine($"{nameof(module.ExceptionOccurred)}: {value}");
            };

            return module;
        }
    }
}
