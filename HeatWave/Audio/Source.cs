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
            get { return audioBuffer; }
            private set
            {
                AL.Source(SourceID, ALSourcei.Buffer, value.BufferID);
                audioBuffer = value;
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
