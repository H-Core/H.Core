using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class ModuleExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        /// <param name="text"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task SayAsync(this Module module, string text, CancellationToken cancellationToken = default)
        {
            module = module ?? throw new ArgumentNullException(nameof(module));
            text = text ?? throw new ArgumentNullException(nameof(text));

            await module.RunAsync(new Command("say", text), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        /// <param name="text"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Say(this Module module, string text)
        {
            module = module ?? throw new ArgumentNullException(nameof(module));
            text = text ?? throw new ArgumentNullException(nameof(text));

            module.Run("say", text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        /// <param name="text"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Print(this Module module, string text)
        {
            module = module ?? throw new ArgumentNullException(nameof(module));
            text = text ?? throw new ArgumentNullException(nameof(text));

            module.Run("print", text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ShowSettings(this Module module)
        {
            module = module ?? throw new ArgumentNullException(nameof(module));

            module.Run("show-module-settings", module.UniqueName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Enable(this Module module)
        {
            module = module ?? throw new ArgumentNullException(nameof(module));

            module.Run("enable-module", module.UniqueName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Disable(this Module module)
        {
            module = module ?? throw new ArgumentNullException(nameof(module));

            module.Run("disable-module", module.UniqueName);
        }
    }
}
