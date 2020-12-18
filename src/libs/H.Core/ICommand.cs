namespace H.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommand
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        string[] Arguments { get; }

        /// <summary>
        /// 
        /// </summary>
        string Argument { get; }

        /// <summary>
        /// 
        /// </summary>
        byte[] Data { get; }

        /// <summary>
        /// 
        /// </summary>
        IProcess? Process { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsEmpty { get; }

        #endregion
    }
}
