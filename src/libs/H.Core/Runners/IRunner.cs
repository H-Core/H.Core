namespace H.Core.Runners
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRunner : IModule
    {
        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        ICall? TryPrepareCall(ICommand command);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="process"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        ICall? TryPrepareCall(IProcess<IValue> process, ICommand command);

        #endregion
    }
}
