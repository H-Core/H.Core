namespace H.Core.Utilities
{
    internal static class EmptyArray<T>
    {
        public static T[] Value { get; }

        static EmptyArray()
        {
#pragma warning disable CA1825 // Avoid zero-length array allocations
            Value = new T[0];
#pragma warning restore CA1825 // Avoid zero-length array allocations
        }
    }
}
