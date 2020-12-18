using System;
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
        /// <returns></returns>
        public static Value Parse(string text)
        {
            text = text ?? throw new ArgumentNullException(nameof(text));
            
            return new Value(text.Split(' '));
        }

        #endregion

        #region Properties

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
        public bool IsEmpty => Arguments.Length == 0 && Data.Length == 0;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public Value(params string[] arguments)
        {
            Arguments = arguments;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public Value(byte[] data)
        {
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
            return $"{Argument}";
        }

        #endregion
    }
}
