using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using H.Core.Utilities;

namespace H.Core.Runners
{
    /// <summary>
    /// 
    /// </summary>
    public class Runner : Module, IRunner, IEnumerable<IAction>
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        protected InvariantStringDictionary<IAction> Actions { get; } = new();

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        private bool TryPrepareAction(ICommand command, out IAction action)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));

            if (!Actions.TryGetValue(command.Name, out action))
            {
                return false;
            }

            for (var i = 0; i < command.Value.Arguments.Length; i++)
            {
                command.Value.Arguments[i] = FindVariablesAndReplace(command.Value.Arguments[i]);
            }

            return true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public ICall? TryPrepareCall(ICommand command)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));

            return TryPrepareAction(command, out var action)
                ? action.PrepareCall(command)
                : null;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public ICall? TryPrepareCall(IProcess<IValue> process, ICommand command)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));

            if (!TryPrepareAction(command, out var action))
            {
                return null;
            }
            if (action is not IProcessAction processAction)
            {
                throw new ArgumentException("Action is not IProcessAction");
            }

            return processAction.PrepareCall(process, command);
        }

        #endregion

        #region Protected methods

        private string FindVariablesAndReplace(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
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
        /// <param name="action"></param>
        public void Add(IAction action)
        {
            action = action ?? throw new ArgumentNullException(nameof(action));

            Actions.Add(action.Name, action);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IAction> GetEnumerator()
        {
            return Actions.Values.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Actions.Values.GetEnumerator();
        }

        #endregion
    }
}