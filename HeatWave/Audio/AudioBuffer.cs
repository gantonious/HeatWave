using OpenTK.Audio.OpenAL;
using System;

namespace HeatWave.Audio
{
    public class AudioBuffer : IDisposable
    {
        public int BufferID { get; private set; }

        public AudioBuffer(int bufferID)
        {
            this.BufferID = bufferID;
        }

        public void Dispose()
        {
            AL.DeleteBuffer(BufferID);
        }
    }
}
