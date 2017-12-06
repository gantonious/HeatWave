using System.Collections.Generic;
using OpenTK.Audio.OpenAL;
using OpenTK;

namespace HeatWave.Audio
{
    public class AudioManager
    {
        private List<Source> sources;

        public AudioManager()
        {
            sources = new List<Source>();;
        }

        public void SetListenerPosition(float x, float y)
        {
            AL.Listener(ALListener3f.Position, x, y, 0);
        }

        public void SetListenerVelocity(float xVelocity, float yVelocity)
        {
            AL.Listener(ALListener3f.Position, xVelocity, yVelocity, 0);
        }

        public void SetListenerOrentation(float x, float y)
        {
            Vector3 at = new Vector3(x, y, 0);
            Vector3 up = new Vector3(0, 0, 1);
            AL.Listener(ALListenerfv.Orientation, ref at, ref up);
        }

        public void PlayAll()
        {
            foreach (Source source in sources) source.Play();
        }

        public void PauseAll()
        {
            foreach (Source source in sources) source.Pause();
        }

        public void StopAll()
        {
            foreach (Source source in sources) source.Stop();
        }
    }
}
