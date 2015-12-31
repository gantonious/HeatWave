using System.Drawing;

namespace HeatWave.Graphics
{
    public abstract class Renderer
    {
        public abstract void Begin();
        public abstract void End();
        public abstract void Draw(float x, float y, float width, float height, float u1, float v1, float u2, float v2, Texture texture, Color color);

        public void Draw(Sprite sprite)
        {
            Draw(sprite.X, sprite.Y, sprite.Width, sprite.Height,
                 sprite.U1, sprite.V1, sprite.U2, sprite.V2, sprite.Texture, Color.Transparent);
        }

        public void Draw(float x, float y, float width, float height, Color color)
        {
            Draw(x, y, width, height, 0, 0, 1, 1, new Texture(0, 0, 0), color);
        }

        public void Draw(float x, float y, float width, float height, Texture texture)
        {
            Draw(x, y, width, height, 0, 0, 1, 1, texture, Color.Transparent);
        }

        public void Draw(float x, float y, float width, float height, TexturedRegion texturedRegion)
        {
            Draw(x, y, width, height, texturedRegion.U1, texturedRegion.V1,
                 texturedRegion.U2, texturedRegion.V2, texturedRegion.Texture, Color.Transparent);
        }

    }
}
