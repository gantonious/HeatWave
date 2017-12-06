using System;
using HeatWave.Graphics.Utils;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace HeatWave.Graphics
{
    public class BatchedRenderer : Renderer
    {
        private static int MAXSIZE = 100000;
        private int lastTextureID = -1;
        VertexArrayObject vao = new VertexArrayObject();
        Vector3[] vertBuffer;
        Vector2[] texBuffer;
        int idx;
        ShaderProgram shader;

        public BatchedRenderer ()
        {
            vertBuffer = new Vector3[MAXSIZE];
            texBuffer = new Vector2[MAXSIZE];
            idx = 0;

            string vertexShader = "#version 410 core\n" +
                                "in vec3 position;\n" +
                                "in vec2 textureCoords;\n" +
                                "out vec2 pass_textureCoords;\n" +
                                "uniform mat4 transformationMatrix;\n" +
                                "void main(void) {\n" +
                                "gl_Position = transformationMatrix * vec4(position, 1.0);\n" +
                                "pass_textureCoords = textureCoords;\n" +
                                "}\n";
            string fragmentShader = "#version 410 core\n" +
                                    "in vec2 pass_textureCoords;\n" +
                                    "out vec4 out_Color;\n" +
                                    "uniform sampler2D textureSampler;\n" +
                                    "void main(void) {\n" +
                                    "out_Color = texture(textureSampler, pass_textureCoords);\n" +
                                    "}\n";

            shader = new ShaderProgram(vertexShader, fragmentShader);
        }

        public override void Begin()
        {
        }

        public override void End()
        {
            flush();
        }

        private void flush()
        {
            vao.registerAttribute(0, vertBuffer, Vector3.SizeInBytes, 3);
            vao.registerAttribute(1, texBuffer, Vector2.SizeInBytes, 2);

            vao.bind();

            shader.Start();

            //TODO: fix or find a better way of doing this
            Matrix4 translationToOrigin = Matrix4.CreateTranslation(-1 * vertBuffer[0].X,
                                                                    -1 * vertBuffer[0].Y, 0);
            Matrix4 scale = Matrix4.CreateScale(2 / 700f,
                                                -2  / 400f, 1);
            Matrix4 translationToFinalSpot = Matrix4.CreateTranslation(2 * (vertBuffer[0].X  - 700 / 2f) / 700f,
                                                                        -2 * (vertBuffer[0].Y  - 400 / 2f) / 400f, 0);

            Matrix4 world = Matrix4.Mult(translationToOrigin, scale);
            world = Matrix4.Mult(world, translationToFinalSpot);

            shader.LoadWorldRef(world);

            GL.DrawArrays(PrimitiveType.Quads, 0, idx);

            shader.Stop();
            idx = 0;
        }

        private void SwapTextures(int textureID)
        {
            flush();
            lastTextureID = textureID;
            GL.BindTexture(TextureTarget.Texture2D, textureID);
        }

        public override void Draw(float x, float y, float width, float height, float u1, float v1, float u2, float v2, Texture texture, Color color)
        {
            if (lastTextureID != texture.TextureID) SwapTextures(texture.TextureID);
            if (idx + 4 > MAXSIZE) flush();

            vertBuffer[idx] = new Vector3(x, y + height, 0);
            vertBuffer[idx + 1] = new Vector3(x + width, y + height, 0);
            vertBuffer[idx + 2] = new Vector3(x + width, y, 0);
            vertBuffer[idx + 3] = new Vector3(x, y, 0);

            texBuffer[idx] = new Vector2(u1, v2);
            texBuffer[idx + 1] = new Vector2(u2, v2);
            texBuffer[idx + 2] = new Vector2(u2, v1);
            texBuffer[idx + 3] = new Vector2(u1, v1);

            idx += 4;
        }

    }
}
