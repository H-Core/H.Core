using System;

namespace H.Core.Notifiers
{
    /// <summary>
    /// 
    /// </summary>
    public interface INotifier : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        event EventHandler EventOccurred;
    }
}
