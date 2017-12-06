using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace HeatWave.Graphics.Utils
{
    class VertexArrayObject
    {

        private int vaoID = 0;
        private Dictionary<int, int> vbos = new Dictionary<int, int>();

        public VertexArrayObject()
        {
            initialize();
        }

        private void initialize()
        {
            vaoID = GL.GenVertexArray();
        }

        private int getVBO(int attributeID)
        {
            if (!vbos.ContainsKey(attributeID)) vbos[attributeID] = GL.GenBuffer();
            return vbos[attributeID];
        }

        private void deleteVBO(int attributeID)
        {
            if (!vbos.ContainsKey(attributeID)) return;
            GL.DeleteBuffer(vbos[attributeID]);
            vbos.Remove(attributeID);
        }

        public void bindIndicies(int[] indicies, int length)
        {
            int VBOID = getVBO(-1);
            GL.BindVertexArray(vaoID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, VBOID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(length * sizeof(int)), indicies, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        public void registerAttribute<T>(int attributeID, T[] data, int elementSizeinBytes, int elementSize) where T : struct
        {
            int VBOID = getVBO(attributeID);
            GL.BindVertexArray(vaoID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOID);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(data.Length * elementSizeinBytes), data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attributeID, elementSize, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        public void Dispose()
        {
            foreach (int attributeID in vbos.Keys) deleteVBO(attributeID);
            GL.DeleteVertexArray(vaoID);
        }

        public void bind()
        {
            GL.BindVertexArray(vaoID);
            foreach (int key in vbos.Keys) GL.EnableVertexAttribArray(key);
        }

        public void unbind()
        {
            foreach (int key in vbos.Keys) GL.DisableVertexAttribArray(key);
            GL.BindVertexArray(0);
        }
    }
}
