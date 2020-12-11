using System;
using System.Collections.Concurrent;
using System.Linq;

namespace H.Core.Utilities
{
    /// <summary>
    /// Contains unhandled exceptions.
    /// </summary>
    public class ExceptionsBag
    {
        #region Properties

        /// <summary>
        /// Occurred exceptions.
        /// </summary>
        public ConcurrentBag<Exception> Exceptions { get; } = new();

        /// <summary>
        /// 
        /// </summary>
        public AggregateException AggregateException => new (Exceptions);

        #endregion

        #region Events

        /// <summary>
        /// Returns unhandled exceptions.
        /// </summary>
        public event EventHandler<Exception>? ExceptionOccurred;

        #endregion

        #region Methods

        /// <summary>
        /// Adds to exceptions array and raises event.
        /// </summary>
        /// <param name="value"></param>
        public void OnOccurred(Exception value)
        {
            Exceptions.Add(value);

            ExceptionOccurred?.Invoke(this, value);
        }

        /// <summary>
        /// Throws <see cref="AggregateException"/> if any exceptions occur.
        /// </summary>
        /// <exception cref="AggregateException"></exception>
        public void EnsureNoExceptions()
        {
            var exception = AggregateException;
            if (exception.InnerExceptions.Any())
            {
                throw exception;
            }
        }

        #endregion
    }
}
