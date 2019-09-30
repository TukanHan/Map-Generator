using System.Collections.Generic;
using System;
using MapGenerator.DataModels;

namespace MapGenerator.Generator
{
    public class LocationShapeGenerator
    {
        private readonly RouletteWheelSelector rouletteWheelSelector;
        private readonly LocationsMap locationsMap;
        private readonly TilesMap tilesMap;
        private Vector2Int startingPos;

        private List<PriorityModel<Vector2Int>> possibleBlocksPositions;

        public LocationShapeGenerator(TilesMap tilesMap, LocationsMap locationsMap, Random random)
        {
            this.tilesMap = tilesMap;
            this.locationsMap = locationsMap;
            this.rouletteWheelSelector = new RouletteWheelSelector(random);
        }

        public bool[,] RandomLocationShape(Vector2Int startingPos, int locationSize)
        {
            this.startingPos = startingPos;
            bool[,] locationShapeMap = new bool[locationsMap.Height, locationsMap.Width];

            possibleBlocksPositions = new List<PriorityModel<Vector2Int>>()
            {
                new PriorityModel<Vector2Int> { Priority = 1, Model = startingPos }
            };

            while (locationSize > 0 && possibleBlocksPositions.Count > 0)
            {
                Vector2Int selectedPosition = rouletteWheelSelector.RouletteWheelSelection(possibleBlocksPositions);

                locationShapeMap[selectedPosition.Y, selectedPosition.X] = true;
                AddNeighbors(locationShapeMap, selectedPosition);
                possibleBlocksPositions.Remove(possibleBlocksPositions.Find(b => b.Model.Equals(selectedPosition)));

                locationSize--;
            }

            return locationShapeMap;
        }

        private void AddNeighbors(bool[,] placeShape, Vector2Int pos)
        {
            if (IsOnMap(new Vector2Int(-1, 0) + pos) && locationsMap.CanGenerateIn(new Vector2Int(pos.X - 1, pos.Y)) && !placeShape[pos.Y, pos.X - 1])
                AddOrUpdateNeighbor(new Vector2Int(-1, 0) + pos);

            if (IsOnMap(new Vector2Int(1, 0) + pos) && locationsMap.CanGenerateIn(new Vector2Int(pos.X + 1, pos.Y)) && !placeShape[pos.Y, pos.X + 1])
                AddOrUpdateNeighbor(new Vector2Int(1, 0) + pos);

            if (IsOnMap(new Vector2Int(0, -1) + pos) && locationsMap.CanGenerateIn(new Vector2Int(pos.X, pos.Y - 1)) && !placeShape[pos.Y - 1, pos.X])
                AddOrUpdateNeighbor(new Vector2Int(0, -1) + pos);

            if (IsOnMap(new Vector2Int(0, 1) + pos) && locationsMap.CanGenerateIn(new Vector2Int(pos.X, pos.Y + 1)) && !placeShape[pos.Y + 1, pos.X])
                AddOrUpdateNeighbor(new Vector2Int(0, 1) + pos);
        }

        private void AddOrUpdateNeighbor(Vector2Int neighborPos)
        {
            PriorityModel<Vector2Int> shapePoint = possibleBlocksPositions.Find(point => point.Model.Equals(neighborPos));

            if (shapePoint == null)
            {
                PriorityModel<Vector2Int> possibleBlock = new PriorityModel<Vector2Int>
                {
                    Model = neighborPos,
                    Priority = (tilesMap[neighborPos.Y, neighborPos.X].Biom == tilesMap[startingPos.Y, startingPos.X].Biom) ? 4 : 1
                };

                possibleBlocksPositions.Add(possibleBlock);
            }
            else
            {
                shapePoint.Priority++;
            }     
        }

        private bool IsOnMap(Vector2Int pos)
        {
            return pos.X >= 0 && pos.Y >= 0 && pos.X < locationsMap.Width && pos.Y < locationsMap.Height;
        }
    }
}