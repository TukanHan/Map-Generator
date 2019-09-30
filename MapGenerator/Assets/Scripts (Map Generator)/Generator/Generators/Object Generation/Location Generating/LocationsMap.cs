using MapGenerator.DataModels;

namespace MapGenerator.Generator
{
    /// <summary>
    /// To check if a location can be generated in a given place.
    /// </summary>
    public class LocationsMap
    {
        class LocationTile
        {
            public LocationTileType LocationTileType { get; private set; }
            public LocationInstance Location { get; private set; }

            public LocationTile(LocationTileType locationTileType)
            {
                LocationTileType = locationTileType;
            }

            public void SetLocation(LocationInstance location)
            {
                LocationTileType = LocationTileType.Location;
                Location = location;
            }
        }

        enum LocationTileType { Empty, Water, Location }

        public int Width { get; }
        public int Height { get; }

        private readonly LocationTile[,] locationsTiles;

        public LocationsMap(int width, int height)
        {
            Width = width;
            Height = height;

            locationsTiles = new LocationTile[height, width];
        }

        public void MarkBlockedTiles(TilesMap map)
        {
            for (int i = 0; i < Height; ++i)
            {
                for (int j = 0; j < Width; ++j)
                {
                    locationsTiles[i, j] = new LocationTile(map[i, j].HasWaterBiom ? LocationTileType.Water : LocationTileType.Empty);
                }
            }
        }

        public void MarkLocation(LocationInstance location)
        {
            foreach (Vector2Int tilePosition in location.Tiles)
            {
                locationsTiles[tilePosition.Y, tilePosition.X].SetLocation(location);
            }
        }

        public bool CanGenerateIn(Vector2Int position)
        {
            return locationsTiles[position.Y, position.X].LocationTileType == LocationTileType.Empty;
        }

        public bool IsOnMap(Vector2Int position)
        {
            return position.X >= 0 && position.Y >= 0 && position.X < Width && position.Y < Height;
        }

        public LocationInstance GetTileLocation(Vector2Int position)
        {
            return locationsTiles[position.Y, position.X].Location;
        }
    }
}
