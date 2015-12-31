using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace HeatWave
{
    public sealed class SceneManager : GameWindow
    {
        private Stack<Scene> scenes;
        public AssetManager AssetManager { get; private set; }

        public SceneManager(int width, int height) : base(width, height) {
            scenes = new Stack<Scene>();
            AssetManager = new AssetManager();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Title = "HeatWave Game";
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);         
        }

        public void PushScene(Scene scene)
        {
            UnbindEventsFromActiveScene();
            scenes.Push(scene);
            BindEventsToActiveScene();
        }

        public void PopScene()
        {
            if (scenes.Count == 0) return;
            UnbindEventsFromActiveScene();
            scenes.Pop();
            BindEventsToActiveScene();
        }

        private void BindEventsToActiveScene()
        {
            if (scenes.Count == 0) return;
            Resize += scenes.Peek().SceneResized;
        }

        private void UnbindEventsFromActiveScene()
        {
            if (scenes.Count == 0) return;
            Resize -= scenes.Peek().SceneResized;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            SwapBuffers();
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, Width, Height, 0, -1, 1);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (scenes.Count > 0) scenes.Peek().Update(e.Time);
            else Exit(); 
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            if (scenes.Count == 0) return;
            GL.Clear(ClearBufferMask.ColorBufferBit);

            scenes.Peek().Render();
            SwapBuffers();
        }
    }
}
