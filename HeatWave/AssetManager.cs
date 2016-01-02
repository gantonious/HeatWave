using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.IO;
using HeatWave.Graphics;
using HeatWave.Audio;
using OpenTK.Graphics.OpenGL;
using OpenTK.Audio.OpenAL;

namespace HeatWave
{
    public class AssetManager
    {
        private Dictionary<string, Texture> textureCache = new Dictionary<string, Texture>();

        public Texture LoadTexture(string path)
        {
            if (textureCache.ContainsKey(path)) return textureCache[path];
            if (!File.Exists(path)) return new Texture(0, 0, 0);

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

        // The following method was taken from the OpenTK Library Audio Example:
        // https://github.com/opentk/opentk/blob/develop/Source/Examples/OpenAL/1.1/Playback.cs
        // Going to update this later, and proabably the rest of the AssetManager structure
        
        public AudioBuffer LoadWave(string path)
        {
            Stream stream = File.Open(path, FileMode.Open);
            
            if (stream == null)
                throw new ArgumentNullException("stream");

            using (BinaryReader reader = new BinaryReader(stream))
            {
                // RIFF header
                string signature = new string(reader.ReadChars(4));
                if (signature != "RIFF")
                    throw new NotSupportedException("Specified stream is not a wave file.");

                int riff_chunck_size = reader.ReadInt32();

                string format = new string(reader.ReadChars(4));
                if (format != "WAVE")
                    throw new NotSupportedException("Specified stream is not a wave file.");

                // WAVE header
                string format_signature = new string(reader.ReadChars(4));
                if (format_signature != "fmt ")
                    throw new NotSupportedException("Specified wave file is not supported.");

                int format_chunk_size = reader.ReadInt32();
                int audio_format = reader.ReadInt16();
                int num_channels = reader.ReadInt16();
                int sample_rate = reader.ReadInt32();
                int byte_rate = reader.ReadInt32();
                int block_align = reader.ReadInt16();
                int bits_per_sample = reader.ReadInt16();

                string data_signature = new string(reader.ReadChars(4));
                if (data_signature != "data")
                    throw new NotSupportedException("Specified wave file is not supported.");

                int data_chunk_size = reader.ReadInt32();

                byte[] soundData = reader.ReadBytes((int)reader.BaseStream.Length);

                int bufferID = AL.GenBuffer();

                AL.BufferData(bufferID, GetSoundFormat(num_channels, bits_per_sample), soundData, soundData.Length, sample_rate);

                return new AudioBuffer(bufferID);
            }
        }

        private ALFormat GetSoundFormat(int channels, int bits)
        {
            switch (channels)
            {
                case 1: return bits == 8 ? ALFormat.Mono8 : ALFormat.Mono16;
                case 2: return bits == 8 ? ALFormat.Stereo8 : ALFormat.Stereo16;
                default: throw new NotSupportedException("The specified sound format is not supported.");
            }
        }

    }
}
