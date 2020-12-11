using System;

namespace H.Core.Converters
{
    /// <summary>
    /// Returns unhandled exceptions.
    /// </summary>
    public class ExceptionsStorage
    {
        /// <summary>
        /// Returns unhandled exceptions.
        /// </summary>
        public event EventHandler<Exception>? Occurred;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void OnOccurred(Exception value)
        {
            Occurred?.Invoke(this, value);
        }
    }
}
