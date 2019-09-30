using MapGenerator.DataModels;
using System.Collections.Generic;

namespace MapGenerator.Generator
{
    public class WaterMapSmoother
    {
        private TilesMap map;
        private readonly WaterBiomModel[] waterBiomes;

        private Stack<Vector2Int> positionToCheck = new Stack<Vector2Int>();

        public WaterMapSmoother(WaterBiomModel[] waterBiomes)
        {
            this.waterBiomes = waterBiomes;
        }

        public void SmoothWaterMap(TilesMap map)
        {
            this.map = map;

            for (int i = 0; i < map.Height; ++i)
            {
                for (int j = 0; j < map.Width; ++j)
                {
                    positionToCheck.Push(new Vector2Int(j, i));
                }
            }

            int loopCount = map.Width * map.Height * 2;
            while (positionToCheck.Count > 0)
            {
                SmoothWater(positionToCheck.Pop());

                if (--loopCount < 0)
                    break;
            }
        }

        private void SmoothWater(Vector2Int pos)
        {
            List<Vector2Int> neighbourPos = new List<Vector2Int>();
            int countOfNeighbourBlock = 0;
            int countOfNeighbourEmpty = 0;
            int countOfNeighbour = 0;

            for (int i = pos.Y - 1; i <= pos.Y + 1; ++i)
            {
                for (int j = pos.X - 1; j <= pos.X + 1; ++j)
                {
                    if (i != pos.Y || j != pos.X)
                    {
                        if (map.IsOnMap(new Vector2Int(j, i)))
                        {
                            if (map[i, j].HasWaterBiom)
                            {
                                countOfNeighbourBlock++;
                                if ((i - pos.Y + 1 == j - pos.X) || (j - pos.X + 1 == i - pos.Y))
                                    countOfNeighbour++;
                            }
                            else
                            {
                                countOfNeighbourEmpty++;
                            }

                            neighbourPos.Add(new Vector2Int(j, i));
                        }
                    }
                }
            }

            if (countOfNeighbourEmpty == 5 && map[pos.Y, pos.X].HasWaterBiom) //Delete corner block
            {
                map[pos.Y, pos.X].WaterBiom = null;
                neighbourPos.ForEach(x => positionToCheck.Push(x));
            }
            else if (countOfNeighbourBlock == 5 && !map[pos.Y, pos.X].HasWaterBiom) //Add corner block 
            {
                map[pos.Y, pos.X].WaterBiom = GetWaterBiom(map[pos.Y, pos.X].WaterDeepness);
                neighbourPos.ForEach(x => positionToCheck.Push(x));
            }
            else if (countOfNeighbour < 2 && map[pos.Y, pos.X].HasWaterBiom)
            {
                map[pos.Y, pos.X].WaterBiom = null;
                neighbourPos.ForEach(x => positionToCheck.Push(x));
            }
            else if (countOfNeighbour > 2 && !map[pos.Y, pos.X].HasWaterBiom)
            {
                map[pos.Y, pos.X].WaterBiom = GetWaterBiom(map[pos.Y, pos.X].WaterDeepness);
                neighbourPos.ForEach(x => positionToCheck.Push(x));
            }
        }

        private BiomModel GetWaterBiom(float value)
        {
            BiomModel biom = waterBiomes.Length > 0 ? waterBiomes?[0].Biom : null;

            for (int i = 1; i < waterBiomes.Length; ++i)
            {
                if (waterBiomes[i].WaterThresholding <= value)
                    biom = waterBiomes[i].Biom;
                else
                    break;
            }

            return biom;
        }
    }
}