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
        /// <param name="name"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        ICall? TryPrepareCall(string name, params string[] arguments);

        #endregion
    }
}
