namespace HeatWave.Graphics
{
    public class Sprite : TexturedRegion
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public Sprite(float x, float y, float width, float height, Texture texture) : base (texture)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }
    }
}
