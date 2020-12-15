﻿namespace H.Core.Runners
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommand
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        string[] Arguments { get; }

        /// <summary>
        /// 
        /// </summary>
        string Argument { get; }

        #endregion
    }
}
