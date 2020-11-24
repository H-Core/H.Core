using System;

namespace H.Core
{
    public interface INotifier : IModule
    {
        event EventHandler EventOccurred;
    }
}
