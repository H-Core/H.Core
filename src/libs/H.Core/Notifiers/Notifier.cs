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
        public ICommand Command { get; set; } = new Command();
        
        /// <summary>
        /// 
        /// </summary>
        public Func<ICommand>? CommandFactory { get; set; }

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
                    if (!Command.IsEmpty)
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
