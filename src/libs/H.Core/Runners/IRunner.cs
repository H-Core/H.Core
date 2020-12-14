using System;

namespace H.Core.Runners
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRunner : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        RunInformation Run(string key, string data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        bool IsSupported(string command);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        bool IsInternal(string key, string data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        RunInformation? GetInformation(string command);

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<string>? Started;
        
        /// <summary>
        /// 
        /// </summary>
        event EventHandler<string>? Completed;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string[] GetSupportedCommands();
    }
}
