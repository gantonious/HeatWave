using System;

namespace HeatWave.Graphics.Utils
{
    interface IVertexData : IDisposable
    {
        void Bind();
        void Unbind();
    }
}
