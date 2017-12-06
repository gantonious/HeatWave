using System;

namespace HeatWave
{
    public abstract class Scene
    {
        public SceneManager SceneManager { get; private set; }

        public Scene(SceneManager sceneManager)
        {
            SceneManager = sceneManager;
        }

        public virtual void SceneResized(object sender, EventArgs e) { }
        public abstract void Update(double delta);
        public abstract void Render();
    }
}
