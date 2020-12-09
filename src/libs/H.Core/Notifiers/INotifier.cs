using System;

namespace H.Core.Notifiers
{
    public interface INotifier : IModule
    {
        event EventHandler EventOccurred;
    }
}
