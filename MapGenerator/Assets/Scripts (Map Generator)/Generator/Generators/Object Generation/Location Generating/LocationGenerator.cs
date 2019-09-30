using MapGenerator.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MapGenerator.Generator
{
    public class LocationGenerator
    {
        private readonly TilesMap tilesMap;
        private readonly LocationsMap locationsMap;        
        private readonly ObjectsMap objectsMap;
        private readonly Random random;
        private readonly RouletteWheelSelector rouletteWheelSelector;

        public LocationGenerator(TilesMap tilesMap, LocationsMap locationsMap, ObjectsMap objectsMap, Random random)
        {
            this.tilesMap = tilesMap;
            this.locationsMap = locationsMap;
            this.objectsMap = objectsMap;
            this.random = random;
            this.rouletteWheelSelector = new RouletteWheelSelector(random);
        }

        public LocationInstance GenerateLocation(LocationModel locationTemplate, Vector2Int startingPosition)
        {
            LocationInstance location = new LocationInstance(locationTemplate);

            int size = random.Next(locationTemplate.MinSize, locationTemplate.MaxSize);
            location.Shape = new LocationShape(tilesMap, locationsMap, random, startingPosition, size);

            if (location.Shape.AllTilesPositions.Count >= locationTemplate.MinSize)
            {
                location.Tiles.AddRange(location.Shape.AllTilesPositions);

                GenerateBigObjects(location);
                GenerateObjects(location);

                if (locationTemplate.Fences.Any() &&
                    random.RandomByThreshold(locationTemplate.ChanceForFence) &&
                    location.BigObjects.Any() &&
                    location.Shape.IsShapeCorrect() &&
                    location.Shape.NeighboringLocations.All(l => !l.HasFence))
                {
                    GenerateFence(location);
                }

                return location;
            }

            return null;
        }

        private void GenerateBigObjects(LocationInstance location)
        {
            List<PriorityModel<BigObjectModel>> bigObjectPossibleToGenerate = new List<PriorityModel<BigObjectModel>>(location.Template.BigObjects);
            Dictionary<BigObjectModel, int> bigObjectModelCountDictionaty = new Dictionary<BigObjectModel, int>();
            bigObjectPossibleToGenerate.ForEach(model => bigObjectModelCountDictionaty[model.Model] = model.MaxCount);

            foreach (Vector2Int pos in location.Shape.SpaceTilesPositions)
            {
                if (bigObjectPossibleToGenerate.Any() && random.RandomByThreshold(location.Template.BigObjectsIntensity))
                {
                    BigObjectModel prefab = rouletteWheelSelector.RouletteWheelSelection(bigObjectPossibleToGenerate);

                    List<Vector2Int> positions = SelectPositions(pos, new Vector2Int(pos.X + prefab.TileWidthCount - 1, pos.Y + prefab.TileHeightCount - 1)).ToList();
                    if (objectsMap.AreTilesEmpty(positions) && positions.All(p => location.Shape.ShapeMap[p.Y, p.X] == LocationTileType.Space))
                    {
                        bigObjectModelCountDictionaty[prefab]--;
                        if (bigObjectModelCountDictionaty[prefab] == 0)
                            bigObjectPossibleToGenerate.Remove(bigObjectPossibleToGenerate.Find(x => x.Model == prefab));

                        AwaitingObject awaitingObject = new AwaitingObject()
                        {
                            AbstractObject = prefab.AbstractObject,
                            Position = new Vector2Float(pos.X + (prefab.TileWidthCount - 1) / 2f, pos.Y)
                        };

                        location.BigObjects.Add(awaitingObject);
                        objectsMap.MarkTiles(pos, new Vector2Int(pos.X + prefab.TileWidthCount - 1, pos.Y + prefab.TileHeightCount - 1), ObjectsMap.ObjectType.BigObject);
                    }
                }
            }
        }

        private void GenerateObjects(LocationInstance location)
        {
            foreach (Vector2Int blockPos in location.Shape.SpaceTilesPositions)
            {
                if (objectsMap.IsTileEmpty(blockPos) &&
                    location.Template.Objects.Any() &&
                    random.RandomByThreshold(location.Template.ObjectsIntensity))
                {
                    ObjectModel prefab = rouletteWheelSelector.RouletteWheelSelection(location.Template.Objects);

                    AwaitingObject awaitingObject = new AwaitingObject()
                    {
                        AbstractObject = prefab.AbstractObject,
                        Position = new Vector2Float(blockPos.X, blockPos.Y)
                    };

                    location.Objects.Add(awaitingObject);
                    objectsMap.MarkTiles(blockPos, ObjectsMap.ObjectType.Object);
                }
            }
        }

        private void GenerateFence(LocationInstance location)
        {
            LocationEntranceGenerator locationEnteranceGenerator = new LocationEntranceGenerator(random, location.Shape);
            if(locationEnteranceGenerator.IsFenceGenerated)
            {
                FenceModel fence = rouletteWheelSelector.RouletteWheelSelection(location.Template.Fences);

                foreach (var borderTile in locationEnteranceGenerator.BorderTiles)
                {
                    AwaitingObject awaitingObject = new AwaitingObject()
                    {
                        AbstractObject = fence.GetFancePart(borderTile.NeighboursArray).AbstractObject,
                        Position = new Vector2Float(borderTile.Position.X, borderTile.Position.Y)
                    };

                    location.Fence.Add(awaitingObject);
                    objectsMap.MarkTiles(borderTile.Position, ObjectsMap.ObjectType.Fance);
                }
            }    
        }

        private IEnumerable<Vector2Int> SelectPositions(Vector2Int leftDown, Vector2Int rightUp)
        {
            for (int i = leftDown.Y; i <= rightUp.Y; ++i)
            {
                for (int j = leftDown.X; j <= rightUp.X; ++j)
                {
                    yield return new Vector2Int(j, i);
                }
            }
        }
    }
}