using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using H.Core.Utilities;

namespace H.Core.Runners
{
    /// <summary>
    /// 
    /// </summary>
    public class Runner : Module, IRunner
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public InvariantStringDictionary<RunInformation> Actions { get; } = new ();

        #endregion

        #region Events
        
        /// <summary>
        /// 
        /// </summary>

        public event EventHandler<string>? Started;
        
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<string>? Completed;
        
        private void OnStarted(string value)
        {
            Started?.Invoke(this, value);
        }

        private void OnCompleted(string value)
        {
            Completed?.Invoke(this, value);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public RunInformation Run(string key, string data)
        {
            try
            {
                OnStarted(key);

                var info = RunInternal(key, data);

                OnCompleted(key);

                return info;
            }
            catch (Exception exception)
            {
                return new RunInformation(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetSupportedCommands() =>
            Actions.Select(i => $"{i.Key} {i.Value.Description}").ToArray();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual bool IsSupport(string key, string data) => GetInformation(key, data) != null;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool IsInternal(string key, string data) => GetInformation(key, data)?.IsInternal ?? false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public RunInformation? GetInformation(string key, string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return null;
            }

            var values = data.SplitOnlyFirst(' ');
            var first = values[0];
            if (first == null)
            {
                return null;
            }

            return Actions.TryGetValue(first, out var information) ? information : null;
        }

        #endregion

        #region Protected methods

        private string? FindVariablesAndReplace(string? command)
        {
            if (command == null || string.IsNullOrWhiteSpace(command))
            {
                return command;
            }

            foreach (var variable in Variables)
            {
                var variableName = variable.Key;
                if (string.IsNullOrWhiteSpace(variableName) || !command.Contains(variableName))
                {
                    continue;
                }

                var func = variable.Value;
                var value = func?.Invoke();

                command = command.Replace(variable.Key, value?.ToString() ?? string.Empty);
            }

            return command;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected virtual RunInformation RunInternal(string key, string data)
        {
            var values = data.SplitOnlyFirstIgnoreQuote(' ');
            var first = values[0];
            if (first == null)
            {
                return new RunInformation(new InvalidOperationException("RunInternal values[0] == null."));
            }

            var information = GetAction(first);
            var command = values[1];

            try
            {
                command = FindVariablesAndReplace(command);
            }
            catch (Exception exception)
            {
                information.Exception = exception;
            }

            var action = information.Action;
            if (action == null)
            {
                //Log
            }

            try
            {
                action?.Invoke(command);

                information.RunText = $"{values[0]} {command}";

                return information;
            }
            catch (Exception exception)
            {
                return information.WithException(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool IsWaitCommand { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public static string? WaitCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        protected async Task<string?> WaitNextCommand(int timeout)
        {
            var recordTimeout = (int)(0.6 * timeout);
            Run($"start-record {recordTimeout}");

            IsWaitCommand = true;

            var time = 0;
            while (IsWaitCommand && time < timeout)
            {
                await Task.Delay(10).ConfigureAwait(false);
                time += 10;
            }

            if (IsWaitCommand)
            {
                WaitCommand = null;
            }
            IsWaitCommand = false;

            return WaitCommand;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="additionalAccepts"></param>
        /// <returns></returns>
        protected async Task<bool> WaitAccept(int timeout, params string[] additionalAccepts)
        {
            var command = await WaitNextCommand(timeout).ConfigureAwait(false);

            var defaultAccepts = new List<string> {"yes", "да", "согласен"};
            defaultAccepts.AddRange(additionalAccepts);

            return command.IsAnyOrdinalIgnoreCase(defaultAccepts.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="timeout"></param>
        /// <param name="additionalAccepts"></param>
        /// <returns></returns>
        protected async Task<bool> WaitAccept(string message, int timeout, params string[] additionalAccepts)
        {
            await SayAsync(message).ConfigureAwait(false);

            return await WaitAccept(timeout, additionalAccepts).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        public static void StopWaitCommand(string command)
        {
            WaitCommand = command;
            IsWaitCommand = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        protected void AddAction(string key, Action<string?> action, string? description = null, bool isInternal = false)
        {
            AddAction(key, new RunInformation
            {
                Description = description,
                Action = action,
                IsInternal = isInternal
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        protected void AddInternalAction(string key, Action<string?> action, string? description = null)
        {
            AddAction(key, action, description, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="description"></param>
        /// <param name="isInternal"></param>
        protected void AddAsyncAction(string key, Func<string?, Task> func, string? description = null, bool isInternal = false)
        {
            AddAction(
                key, 
                async text => await (func.Invoke(text) ?? Task.FromResult(false)).ConfigureAwait(false),
                description, 
                isInternal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="description"></param>
        protected void AddInternalAsyncAction(string key, Func<string?, Task> func, string? description = null)
        {
            AddAsyncAction(key, func, description, true);
        }

        private void AddAction(string key, RunInformation information)
        {
            Actions.Add(key, information);
        }

        private RunInformation GetAction(string key)
        {
            return Actions.TryGetValue(key, out var handler) ? handler : new RunInformation();
        }

        #endregion
    }
}