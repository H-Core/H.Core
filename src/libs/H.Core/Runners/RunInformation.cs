using System;

namespace H.Core.Runners
{
    /// <summary>
    /// 
    /// </summary>
    public class RunInformation
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Exception? Exception { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public bool IsInternal { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string? Description { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Action<string?>? Action { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? RunText { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public RunInformation()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        public RunInformation(Exception exception)
        {
            Exception = exception;
            IsInternal = false;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public RunInformation WithException(Exception exception)
        {
            Exception = exception;

            return this;
        }

        #endregion

    }
}
