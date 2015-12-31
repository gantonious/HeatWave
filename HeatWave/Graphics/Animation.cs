using System.Collections.Generic;

namespace HeatWave.Graphics
{
    public abstract class Animation
    {
        private List<TexturedRegion> frames;

        public Animation()
        {

        }

        public abstract TexturedRegion GetFrame();
    }
}
