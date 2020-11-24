using System;
using H.Core.Runners;

namespace H.Core
{
    public interface IRunner : IModule
    {
        RunInformation Run(string key, string data);
        bool IsSupport(string key, string data);
        bool IsInternal(string key, string data);
        RunInformation? GetInformation(string key, string data);

        event EventHandler<RunnerEventArgs>? BeforeRun;
        event EventHandler<RunnerEventArgs>? AfterRun;

        string[] GetSupportedCommands();
    }
}
