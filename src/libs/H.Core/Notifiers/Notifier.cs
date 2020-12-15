using System;

namespace H.Core.Notifiers
{
    /// <summary>
    /// 
    /// </summary>
    public class Notifier : Module, INotifier
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string Command { get; set; } = string.Empty;
        
        /// <summary>
        /// 
        /// </summary>
        public Func<string>? CommandFactory { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? EventOccurred;

        /// <summary>
        /// 
        /// </summary>
        protected void OnEventOccurred()
        {
            EventOccurred?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        protected Notifier()
        {
            AddSetting(nameof(Command), o => Command = o, _ => true, Command);

            EventOccurred += (_, _) =>
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(Command))
                    {
                        Run(Command);
                    }
                    if (CommandFactory != null)
                    {
                        Run(CommandFactory());
                    }
                }
                catch (Exception exception)
                {
                    OnExceptionOccurred(exception);
                }
            };
        }

        #endregion

    }
}
