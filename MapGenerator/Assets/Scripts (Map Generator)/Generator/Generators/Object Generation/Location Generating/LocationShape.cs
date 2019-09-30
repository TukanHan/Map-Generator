using MapGenerator.DataModels;
using System;
using System.Collections.Generic;

namespace MapGenerator.Generator
{
    public enum LocationTileType
    {
        None,
        Space,
        Border
    };

    public class LocationShape
    {
        public List<Vector2Int> AllTilesPositions { get; }
        public List<Vector2Int> SpaceTilesPositions { get; }
        public List<Vector2Int> BorderTilesPositions { get; }
        public LocationTileType[,] ShapeMap { get; }
        public List<LocationInstance> NeighboringLocations { get; }

        private readonly LocationsMap locationsMap;

        public LocationShape(TilesMap tilesMap, LocationsMap locationsMap, Random random, Vector2Int position, int shapeSize)
        {
            this.locationsMap = locationsMap;

            LocationShapeGenerator locationShapeGenerator = new LocationShapeGenerator(tilesMap, locationsMap, random);
            bool[,] shapeMap = locationShapeGenerator.RandomLocationShape(position, shapeSize);

            LocationShapeSmoother locationShapeSmoother = new LocationShapeSmoother(locationsMap);
            AllTilesPositions = locationShapeSmoother.SmoothLocationShape(shapeMap);

            LocationShapeBorderSelector locationShapeBorderSelector = new LocationShapeBorderSelector(locationsMap, shapeMap, AllTilesPositions);
            BorderTilesPositions = locationShapeBorderSelector.ShapeBorder;
            NeighboringLocations = locationShapeBorderSelector.NeighboringLocations;

            SpaceTilesPositions = new List<Vector2Int>(AllTilesPositions);
            BorderTilesPositions.ForEach(block => SpaceTilesPositions.Remove(block));

            ShapeMap = new LocationTileType[locationsMap.Height, locationsMap.Width];
            SpaceTilesPositions.ForEach(tile => ShapeMap[tile.Y, tile.X] = LocationTileType.Space);
            BorderTilesPositions.ForEach(tile => ShapeMap[tile.Y, tile.X] = LocationTileType.Border);
        }

        public LocationTileType[,] GetBlockNeighbours(Vector2Int pos)
        {
            LocationTileType[,] neighbours = new LocationTileType[3, 3];

            for (int i = pos.Y - 1; i <= pos.Y + 1; ++i)
            {
                for (int j = pos.X - 1; j <= pos.X + 1; ++j)
                {
                    if (locationsMap.IsOnMap(new Vector2Int(j, i)))
                        neighbours[i - pos.Y + 1, j - pos.X + 1] = ShapeMap[i, j];
                    else
                        neighbours[i - pos.Y + 1, j - pos.X + 1] = LocationTileType.None;
                }
            }

            return neighbours;
        }

        public bool IsShapeCorrect()
        {
            return Math.Pow(BorderTilesPositions.Count, 2) / (4 * Math.PI * AllTilesPositions.Count) < 2;
        }
    }
}