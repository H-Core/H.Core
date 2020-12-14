﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace H.Core.Runners
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public bool IsInternal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public bool IsCancellable { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<string[]>? Running;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<string[]>? Ran;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        protected void OnRunning(string[] value)
        {
            Running?.Invoke(this, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        protected void OnRan(string[] value)
        {
            Ran?.Invoke(this, value);
        }

        #endregion


        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract Task RunAsync(string[] arguments, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public ICall PrepareCall(params string[] arguments)
        {
            return new Call(this, arguments);
        }

        #endregion
    }
}