namespace H.Core.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public class TextDeferredEventArgs : DeferredEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static TextDeferredEventArgs Create(string text) => new (text);

        /// <summary>
        /// 
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public TextDeferredEventArgs(string text)
        {
            Text = text;
        }
    }
}
