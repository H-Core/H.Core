﻿using System;
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
        public ICall? TryPrepareCall(string name, params string[] arguments)
        {
            name = name ?? throw new ArgumentNullException(nameof(name));

            return TryPrepareCall(new Command(name, arguments));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public ICall? TryPrepareCall(ICommand command)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));

            if (!Actions.TryGetValue(command.Name, out var action))
            {
                return null;
            }

            for (var i = 0; i < command.Arguments.Length; i++)
            {
                command.Arguments[i] = FindVariablesAndReplace(command.Arguments[i]);
            }

            return action.PrepareCall(command.Arguments);
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