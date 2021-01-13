﻿using System;
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static Command Parse(string text)
        {
            text = text ?? throw new ArgumentNullException(nameof(text));
            if (string.IsNullOrWhiteSpace(text))
            {
                return Empty;
            }

            var values = text.CmdSplit();

            return new Command(
                values.ElementAt(0),
                values.Skip(1).ToArray());
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
        public IValue Input { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IValue Output { get; set; } = Value.Empty;

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
            Input = new Value(arguments);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="arguments"></param>
        public Command(string name, params string[] arguments)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Input = new Value(arguments);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public Command(byte[] data)
        {
            Input = new Value(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <param name="arguments"></param>
        public Command(string name, byte[] data, params string[] arguments)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));

            Input = new Value(data, arguments);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public Command(string name, IValue value)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Input = value;
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
                : $"{Name} {Input}";
        }

        #endregion
    }
}
