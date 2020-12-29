namespace H.Core.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class EmptyArray<T>
    {
        /// <summary>
        /// 
        /// </summary>
#pragma warning disable CA1825 // Avoid zero-length array allocations
#pragma warning disable CA1000 // Do not declare static members on generic types
        public static T[] Value { get; } = new T[0];
#pragma warning restore CA1000 // Do not declare static members on generic types
#pragma warning restore CA1825 // Avoid zero-length array allocations
    }
}
