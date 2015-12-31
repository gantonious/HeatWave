using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.IO;
using OpenTK.Graphics.OpenGL;
using HeatWave.Graphics;

namespace HeatWave
{
    public class AssetManager
    {
        private Dictionary<string, Texture> textureCache = new Dictionary<string, Texture>();

        public Texture loadTexture(string path)
        {
            if (textureCache.ContainsKey(path)) return textureCache[path];
            if (!File.Exists(path)) path = "Assets/Textures/notFound.png";

            int textureID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textureID);

            Bitmap bmp = new Bitmap(path);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                            OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS,
                (int)TextureWrapMode.Clamp);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int)TextureMinFilter.Linear);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int)TextureMagFilter.Linear);

            textureCache[path] = new Texture(textureID, bmp.Width, bmp.Height);
            return textureCache[path];
        }
    }

}
