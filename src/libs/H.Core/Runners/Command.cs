using System;
using System.Linq;
using H.Core.Utilities;

namespace H.Core.Runners
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Command
    {
        #region Static methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Command Parse(string text)
        {
            var values = text.SplitOnlyFirstIgnoreQuote(' ');

            return new Command(
                values.ElementAt(0),
                (values.ElementAtOrDefault(1) ?? string.Empty).Split(' '));
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public string[] Arguments { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="arguments"></param>
        public Command(string name, params string[] arguments)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Arguments = arguments;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name} {string.Join(" ", Arguments)}";
        }

        #endregion
    }
}
