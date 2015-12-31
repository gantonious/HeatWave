using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace HeatWave.Graphics
{
    public class ImmediateModeRenderer : Renderer
    {
        private int lastTextureID = -1;

        public ImmediateModeRenderer() { }

        public override void Begin()
        {
            GL.Begin(PrimitiveType.Quads);
        }

        public override void End()
        {
            GL.End();
        }

        private void SwapTextures(int textureID)
        {
            End();
            lastTextureID = textureID;
            GL.BindTexture(TextureTarget.Texture2D, textureID);
            Begin();
        }

        public override void Draw(float x, float y, float width, float height, float u1, float v1, float u2, float v2, Texture texture, Color color)
        {
            if (lastTextureID != texture.TextureID) SwapTextures(texture.TextureID);

            GL.Color3(color);

            GL.TexCoord2(u1, v1);
            GL.Vertex3(x, y, 0);

            GL.TexCoord2(u1, v2);
            GL.Vertex3(x, y + height, 0);

            GL.TexCoord2(u2, v2);
            GL.Vertex3(x + width, y + height, 0);

            GL.TexCoord2(u2, v1);
            GL.Vertex3(x + width, y, 0);
        }
    }
}
