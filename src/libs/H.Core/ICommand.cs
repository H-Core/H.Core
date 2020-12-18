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
        IValue Value { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsEmpty { get; }

        #endregion
    }
}
