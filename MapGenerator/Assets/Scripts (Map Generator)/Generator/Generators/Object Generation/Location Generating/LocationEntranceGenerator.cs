using System;
using System.Collections.Generic;
using System.Linq;

namespace MapGenerator.Generator
{
    public class LocationEntranceGenerator
    {
        public class BorderTile
        {
            public Vector2Int Position { get; set; }
            public LocationTileType[,] NeighboursArray { get; set; }
        }

        private static readonly List<LocationTileType[,]> entranceTemplates = new List<LocationTileType[,]>
        {
            //Top
            new LocationTileType[,] { { LocationTileType.None, LocationTileType.None, LocationTileType.None },
                                      { LocationTileType.Border, LocationTileType.Border, LocationTileType.Border },
                                      { LocationTileType.Space, LocationTileType.Space, LocationTileType.Space } },
            //Bottom
            new LocationTileType[,] { { LocationTileType.Space, LocationTileType.Space, LocationTileType.Space },
                                      { LocationTileType.Border, LocationTileType.Border, LocationTileType.Border },
                                      { LocationTileType.None, LocationTileType.None, LocationTileType.None } }, 
            //Left
            new LocationTileType[,] { { LocationTileType.None, LocationTileType.Border, LocationTileType.Space },
                                      { LocationTileType.None, LocationTileType.Border, LocationTileType.Space },
                                      { LocationTileType.None, LocationTileType.Border, LocationTileType.Space } }, 
            //Right
            new LocationTileType[,] { { LocationTileType.Space, LocationTileType.Border, LocationTileType.None },
                                      { LocationTileType.Space, LocationTileType.Border, LocationTileType.None },
                                      { LocationTileType.Space, LocationTileType.Border, LocationTileType.None } }
        };

        public List<BorderTile> BorderTiles { get; private set; }
        public List<BorderTile> EntranceTiles { get; private set; }
        public bool IsFenceGenerated { get; private set; }

        private Random random;

        public LocationEntranceGenerator(Random random, LocationShape locationShape)
        {
            this.random = random;

            List<BorderTile> allBorderTiles = GetBorderTilesNeighboursArray(locationShape);
            List<BorderTile> possibleEntrance = SelectPosibleEntranceTiles(allBorderTiles);

            EntranceTiles = RandomEntrance(possibleEntrance);
            BorderTiles = allBorderTiles.Where(x => !EntranceTiles.Contains(x)).ToList();

            IsFenceGenerated = EntranceTiles.Any();
        }

        private List<BorderTile> RandomEntrance(List<BorderTile> possibleEntrance)
        {
            List<BorderTile> entrances = new List<BorderTile>();

            int maxEntranceCount = 4;
            float acceptableEntranceDistance = 7;
            for(int i=0; i< maxEntranceCount && possibleEntrance.Any(); ++i)
            {
                BorderTile randomEntrance = possibleEntrance[random.Next(0, possibleEntrance.Count)];

                if(!entrances.Any(e => Vector2Int.CalculateDistance(randomEntrance.Position, e.Position) < acceptableEntranceDistance))
                {
                    entrances.Add(randomEntrance);
                    possibleEntrance.Remove(randomEntrance);
                }  
            }

            return entrances;
        }

        private List<BorderTile> GetBorderTilesNeighboursArray(LocationShape locationShape)
        {
            List<BorderTile> borderTilesNeighboursArray = new List<BorderTile>();
            foreach (Vector2Int borderTilePos in locationShape.BorderTilesPositions)
            {
                borderTilesNeighboursArray.Add(new BorderTile
                {
                    Position = borderTilePos,
                    NeighboursArray = locationShape.GetBlockNeighbours(borderTilePos)
                });
            }

            return borderTilesNeighboursArray;
        }

        private List<BorderTile> SelectPosibleEntranceTiles(IEnumerable<BorderTile> allBorderTiles)
        {
            List<BorderTile> posibleEntranceTiles = new List<BorderTile>();
            foreach (BorderTile borderTile in allBorderTiles)
            {
                if (CanBeEntranceTile(borderTile))
                    posibleEntranceTiles.Add(borderTile);
            }

            return posibleEntranceTiles;
        }

        private bool CanBeEntranceTile(BorderTile borderTile)
        {
            foreach(var entranceTemplate in entranceTemplates)
            {
                if (CompareNeighboursArrays(borderTile.NeighboursArray, entranceTemplate))
                    return true;
            }

            return false;
        }

        private bool CompareNeighboursArrays(LocationTileType[,] arrayA, LocationTileType[,] arrayB)
        {
            for(int i=0; i<3; ++i)
            {
                for(int j=0; j<3; ++j)
                {
                    if (arrayA[i, j] != arrayB[i, j])
                        return false;
                }
            }

            return true;
        }
    }
}
