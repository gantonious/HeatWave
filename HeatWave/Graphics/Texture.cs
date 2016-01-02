using System;
using OpenTK.Graphics.OpenGL;

namespace HeatWave.Graphics
{
    public class Texture : IDisposable
    {
        public int TextureID { get; private set; }
        public float Width { get; private set; }
        public float Height { get; private set; }

        public Texture(int textureID, float width, float height)
        {
            this.TextureID = textureID;
            this.Width = width;
            this.Height = height;
        }

        public void Dispose()
        {
            GL.DeleteTexture(TextureID);
        }
    }
}
