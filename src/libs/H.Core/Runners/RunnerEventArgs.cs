namespace H.Core.Runners
{
    public class RunnerEventArgs
    {
        public IRunner? Runner { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
