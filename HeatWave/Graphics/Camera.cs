namespace HeatWave.Graphics
{
    public abstract class Camera
    {
        public float Zoom { get; set; }
        public float X { get; protected set; }
        public float Y { get; protected set; }
        public float ViewWidth { get; set; }
        public float ViewHeight { get; set; }

        public Camera(float x, float y, float viewWidth, float viewHeight)
        {
            this.X = x;
            this.Y = y;
            this.ViewWidth = viewWidth;
            this.ViewHeight = viewHeight;
            this.Zoom = 1;
        }

        public abstract void Update(float focusX, float focusY);

    }
}
