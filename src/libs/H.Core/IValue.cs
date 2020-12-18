namespace H.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IValue
    {
        #region Properties

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
        bool IsEmpty { get; }

        #endregion
    }
}
