using System;
using System.Collections;
using System.Collections.Generic;
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

            for (var i = 0; i < command.Input.Arguments.Length; i++)
            {
                command.Input.Arguments[i] = FindVariablesAndReplace(command.Input.Arguments[i]);
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