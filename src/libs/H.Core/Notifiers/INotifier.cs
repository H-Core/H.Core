using System;

namespace H.Core.Notifiers
{
    /// <summary>
    /// 
    /// </summary>
    public interface INotifier : IModule
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        string Command { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        event EventHandler EventOccurred;

        #endregion
    }
}
