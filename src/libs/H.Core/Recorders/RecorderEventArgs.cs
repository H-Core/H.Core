using System;
using H.Core.Utilities;

namespace H.Core.Recorders
{
    [Serializable]
    public class RecorderEventArgs : EventArgs
    {
        public byte[] RawData { get; set; }
        public byte[] WavData { get; set; }

        public RecorderEventArgs(byte[]? rawData = null, byte[]? wavData = null)
        {
            RawData = rawData ?? EmptyArray<byte>.Value;
            WavData = wavData ?? EmptyArray<byte>.Value;
        }
    }
}
