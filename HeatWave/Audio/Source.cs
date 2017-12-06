using System;
using OpenTK.Audio.OpenAL;

namespace HeatWave.Audio
{
    public class Source : IDisposable
    {
        private AudioBuffer audioBuffer;
        public int SourceID { get; private set; }

        public AudioBuffer AudioBuffer
        {
            get
            {
                return audioBuffer;
            }
            private set
            {
                AL.Source(SourceID, ALSourcei.Buffer, value.BufferID);
                audioBuffer = value;
            }
        }

        public bool Looping
        {
            get
            {   bool isLooping;
                AL.GetSource(SourceID, ALSourceb.Looping, out isLooping);
                return isLooping;
            }
            set
            {
                AL.Source(SourceID, ALSourceb.Looping, value);
            }
        }

        public float ReferenceDistance
        {
            get
            {
                float referenceDistance;
                AL.GetSource(SourceID, ALSourcef.ReferenceDistance, out referenceDistance);
                return referenceDistance;
            }
            set
            {
                AL.Source(SourceID, ALSourcef.ReferenceDistance, value);
            }
        }
        
        public Source(AudioBuffer audioBuffer)
        {
            this.SourceID = AL.GenSource();
            this.AudioBuffer = audioBuffer;
        }

        public void SetPosition(float x, float y)
        {
            AL.Source(SourceID, ALSource3f.Position, x, y, 0);
        }

        public void SetVelocity(float xVelocity, float yVelocity)
        {
            AL.Source(SourceID, ALSource3f.Position, xVelocity, yVelocity, 0);
        }

        public void SetDirection(float x, float y)
        {
            AL.Source(SourceID, ALSource3f.Direction, x, y, 0);
        }

        public void Play()
        {
            AL.SourcePlay(SourceID);
        }

        public void Pause()
        {
            AL.SourcePause(SourceID);
        }

        public void Stop()
        {
            AL.SourceStop(SourceID);
        }

        public void Dispose()
        {
            AL.DeleteSource(SourceID);
        }
    }
}
