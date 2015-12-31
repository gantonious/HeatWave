namespace HeatWave.Graphics
{
    public class Sprite : TexturedRegion
    {
        public float X { get; protected set; }
        public float Y { get; protected set; }
        public float Width { get; protected set; }
        public float Height { get; protected set; }

        public Sprite(float x, float y, float width, float height, Texture texture) : base (texture)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }
    }
}
