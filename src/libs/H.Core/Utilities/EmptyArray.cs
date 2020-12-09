namespace H.Core.Utilities
{
    internal static class EmptyArray<T>
    {
        public static T[] Value { get; }

        static EmptyArray()
        {
            Value = new T[0];
        }
    }
}
