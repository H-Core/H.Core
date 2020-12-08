using System;
using System.Collections.Generic;

namespace H.Core.CustomEventArgs
{
    [Serializable]
    public class RecorderEventArgs : EventArgs
    {
        public IReadOnlyCollection<byte>? RawData { get; set; }
        public IReadOnlyCollection<byte>? WavData { get; set; }
    }
}
