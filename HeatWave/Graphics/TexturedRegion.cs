namespace HeatWave.Graphics
{
    public class TexturedRegion
    {
        public Texture Texture { get; private set; }
        public float U1 { get; protected set; }
        public float V1 { get; protected set; }
        public float U2 { get; protected set; }
        public float V2 { get; protected set; }

        public TexturedRegion(Texture texture)
        {
            this.Texture = texture;
            SetRegion(0, 0, 1, 1);
        }

        public TexturedRegion(Texture texture, float u1, float v1, float u2, float v2)
        {
            this.Texture = texture;
            SetRegion(u1, v1, u2, v2);
        }

        public void SetRegion(float u1, float v1, float u2, float v2)
        {
            this.U1 = u1;
            this.V1 = v1;
            this.U2 = u2;
            this.V2 = v2;
        }

        public TexturedRegion GetTileRegion(int column, int row, float tileWidth, float tileHeight)
        {
            float u1 = (column * tileWidth) / Texture.Width;
            float v1 = column * tileHeight / Texture.Height;
            float u2 = (column * tileWidth + tileWidth) / Texture.Width;
            float v2 = (column * tileHeight + tileHeight) / Texture.Height;

            return new TexturedRegion(Texture, u1, v1, u2, v2);
        }
    }
}
