using System;
using System.Linq;
using H.Core.Utilities;

namespace H.Core
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Command : ICommand
    {
        #region Static methods

        /// <summary>
        /// Empty command.
        /// </summary>
        public static Command Empty { get; } = new ();
        
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
        public string Name { get; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string[] Arguments { get; } = EmptyArray<string>.Value;

        /// <summary>
        /// 
        /// </summary>
        public string Argument => string.Join(" ", Arguments);

        /// <summary>
        /// 
        /// </summary>
        public byte[] Data { get; } = EmptyArray<byte>.Value;

        /// <summary>
        /// 
        /// </summary>
        public IProcess<ICommand>? Process { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsEmpty => string.IsNullOrWhiteSpace(Name);

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public Command(params string[] arguments)
        {
            Arguments = arguments;
        }
        
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public Command(byte[] data)
        {
            Data = data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public Command(string name, byte[] data)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Data = data;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return IsEmpty
                ? string.Empty
                : $"{Name} {Argument}";
        }

        #endregion
    }
}
