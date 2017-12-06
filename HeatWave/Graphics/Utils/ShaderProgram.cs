using System;
using OpenTK;
using OpenTK.Graphics.ES20;
using System.Collections.Generic;

namespace HeatWave.Graphics.Utils
{
    class ShaderCompilationFailedException : Exception
    {
        public ShaderCompilationFailedException(string message = "") : base(message) { }
    }

    class ShaderProgram : IDisposable
    {
        private int shaderProgramID;
        private int vertexShaderID;
        private int fragmentShaderID;
        private Dictionary<string, int> uniforms = new Dictionary<string, int>();

        public ShaderProgram(string vertexShaderSource, string fragmentShaderSource)
        {
            vertexShaderID = CompileShader(vertexShaderSource, ShaderType.VertexShader);
            fragmentShaderID = CompileShader(fragmentShaderSource, ShaderType.FragmentShader);
            shaderProgramID = GL.CreateProgram();
            GL.AttachShader(shaderProgramID, vertexShaderID);
            GL.AttachShader(shaderProgramID, fragmentShaderID);
            BindAttributes();
            GL.LinkProgram(shaderProgramID);
            GL.ValidateProgram(shaderProgramID);
            GetUniforms();
        }

        public void Start()
        {
            GL.UseProgram(shaderProgramID);
        }

        public void Stop()
        {
            GL.UseProgram(0);
        }

        public void BindAttributes()
        {
            BindAttribute(0, "position");
            BindAttribute(1, "textureCoords");
        }

        public void GetUniforms()
        {
            uniforms["transformationMatrix"] = GetUniform("transformationMatrix");
        }

        public int GetUniform(string uniformName)
        {
            if (!uniforms.ContainsKey(uniformName))
            {
                uniforms[uniformName] = GL.GetUniformLocation(shaderProgramID, uniformName);
            }
            return uniforms[uniformName];
        }

        public void LoadWorldRef(Matrix4 matrix)
        {
            LoadMatrix(uniforms["transformationMatrix"], matrix);
        }

        public void LoadFloat(int location, float number)
        {
            GL.Uniform1(location, number);
        }

        public void LoadVector(int location, Vector3 vector)
        {
            GL.Uniform3(location, vector.X, vector.Y, vector.Z);
        }

        public void LoadMatrix(int location, Matrix4 matrix)
        {
            GL.UniformMatrix4(location, false, ref matrix);
        }

        public void BindAttribute(int attributeID, string variable)
        {
            GL.BindAttribLocation(shaderProgramID, attributeID, variable);
        }
    
        private int CompileShader(string shaderSource, ShaderType shaderType)
        {
            int shaderID = GL.CreateShader(shaderType);
            GL.ShaderSource(shaderID, shaderSource);
            GL.CompileShader(shaderID);

            int output;
            GL.GetShader(shaderID, ShaderParameter.CompileStatus, out output);

            if (output != 1) throw new ShaderCompilationFailedException(GL.GetShaderInfoLog(shaderID));
            return shaderID;
        }

        public void Dispose()
        {
            GL.UseProgram(0);
            GL.DetachShader(shaderProgramID, vertexShaderID);
            GL.DetachShader(shaderProgramID, fragmentShaderID);
            GL.DeleteShader(vertexShaderID);
            GL.DeleteShader(fragmentShaderID);
            GL.DeleteProgram(shaderProgramID);
        }
    }
}
