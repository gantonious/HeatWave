using System;
using OpenTK.Audio.OpenAL;

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

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(this, obj)) return true;
            if (obj == null || obj.GetType() != this.GetType()) return false;

            AudioBuffer other = (AudioBuffer)obj;

            return this.BufferID == other.BufferID;
        }

        public override int GetHashCode()
        {
            return BufferID;
        }

        public static bool operator ==(AudioBuffer left, AudioBuffer right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(AudioBuffer left, AudioBuffer right)
        {
            return !(left == right);
        }
    }
}
