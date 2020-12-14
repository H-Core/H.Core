using System;

namespace H.Core.Runners
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CommandBase
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
        public string Prefix { get; set; } = string.Empty;

        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<string>? Running;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<string>? Ran;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        protected void OnRunning(string value)
        {
            Running?.Invoke(this, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        protected void OnRan(string value)
        {
            Ran?.Invoke(this, value);
        }

        #endregion
    }
}
