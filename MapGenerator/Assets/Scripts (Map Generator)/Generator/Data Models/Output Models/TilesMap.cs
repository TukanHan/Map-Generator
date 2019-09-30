namespace MapGenerator.DataModels
{
    /// <summary>
    /// Complete data for all map tiles.
    /// </summary>
    public class TilesMap
    {
        public int Width { get; }
        public int Height { get; }

        public Tile[,] Map { get; }

        public TilesMap(int width, int height)
        {
            Width = width;
            Height = height;

            Map = new Tile[height, width];

            for(int i=0; i<height; ++i)
            {
                for (int j = 0; j < width; j++)
                {
                    Map[i, j] = new Tile();
                }
            }
        }

        public bool IsOnMap(Vector2Int position)
        {
            return position.X >= 0 && position.Y >= 0 && position.X < Width && position.Y < Height;
        }

        public Tile this[int i, int j]
        {
            get { return Map[i,j]; }
            set { Map[i, j] = value; }
        }
    }
}