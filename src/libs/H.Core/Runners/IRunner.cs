using System;

namespace H.Core.Runners
{
    public interface IRunner : IModule
    {
        RunInformation Run(string key, string data);
        bool IsSupport(string key, string data);
        bool IsInternal(string key, string data);
        RunInformation? GetInformation(string key, string data);

        event EventHandler<string>? Started;
        event EventHandler<string>? Completed;

        string[] GetSupportedCommands();
    }
}
