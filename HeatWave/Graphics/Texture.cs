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

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(this, obj)) return true;
            if (obj == null || obj.GetType() != this.GetType()) return false;

            Texture other = (Texture)obj;

            return this.TextureID == other.TextureID
                   && this.Width == other.Width
                   && this.Height == other.Height;
        }

        public override int GetHashCode()
        {
            return TextureID;
        }

        public static bool operator ==(Texture left, Texture right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Texture left, Texture right)
        {
            return !(left == right);
        }
    }
}
