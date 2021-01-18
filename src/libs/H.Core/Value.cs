using System;
using System.Linq;
using H.Core.Utilities;

namespace H.Core
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Value : IValue
    {
        #region Static methods

        /// <summary>
        /// Empty value.
        /// </summary>
        public static Value Empty { get; } = new ();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static Value Parse(string text)
        {
            text = text ?? throw new ArgumentNullException(nameof(text));
            if (string.IsNullOrWhiteSpace(text))
            {
                return Empty;
            }

            return new Value(text.CmdSplit());
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string[] Arguments { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Argument => string.Join(" ", Arguments);

        /// <summary>
        /// 
        /// </summary>
        public byte[] Data { get; set; } = EmptyArray<byte>.Value;

        /// <summary>
        /// 
        /// </summary>
        public bool IsEmpty => Arguments.Length == 0 && Data.Length == 0;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        public Value(params string[] arguments)
        {
            Arguments = arguments;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="arguments"></param>
        public Value(byte[] data, params string[] arguments)
        {
            Data = data;
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
            return Arguments.Any()
                ? string.Join(
                    " ", 
                    Arguments.Select(argument => $"\"{argument}\""))
                : Data.Any()
                    ? $"{Data.Length} bytes"
                    : string.Empty;
        }

        #endregion
    }
}
