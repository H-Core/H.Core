using System;
using H.Core.Attributes;

namespace H.Core.Notifiers
{
    /// <summary>
    /// 
    /// </summary>
    [AllowMultipleInstance(false)]
    public class Notifier : Module, INotifier
    {
        #region Properties

        private string Command { get; set; } = string.Empty;

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
                OnLogReceived($"{Name} AfterEvent");
                try
                {
                    if (string.IsNullOrWhiteSpace(Command))
                    {
                        OnLogReceived("Command is empty");
                        return;
                    }

                    Run(Command);
                    OnLogReceived($"Run command: {Command}");
                }
                catch (Exception exception)
                {
                    OnLogReceived($"Exception: {exception}");
                }
            };
        }

        #endregion

    }
}
