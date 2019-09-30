using MapGenerator.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MapGenerator.Generator
{
    public class WaterMapGenerator
    {
        private class WaterTile
        {
            public float DeepnessValue { get; set; }
            public Vector2Int Position { get; set; }

            public WaterTile(float deepnessValue, Vector2Int position)
            {
                DeepnessValue = deepnessValue;
                Position = position;
            }
        }

        private readonly WaterBiomModel[] waterBiomes;
        private readonly WaterNoiseMapParametersModel waterNoiseMapParameters;

        private NoiseMapGenerator noiseGenerator;

        public WaterMapGenerator(Random random, WaterBiomModel[] waterBiomes,
                                 WaterNoiseMapParametersModel waterNoiseMapParameters)
        {
            this.waterBiomes = waterBiomes;
            this.waterNoiseMapParameters = waterNoiseMapParameters;
            this.noiseGenerator = new NoiseMapGenerator(random);
        }

        public void GenerateWaterMap(TilesMap map)
        {
            if(waterBiomes.Length > 0)
            {
                float firstThreshold = waterBiomes[0].WaterThresholding;

                float[,] waterDeepnessMap = noiseGenerator.Generate(waterNoiseMapParameters, map.Width, map.Height);
                List<WaterTile> waterTiles = GetAllWaterTiles(waterDeepnessMap, firstThreshold, out int deepEnoughTilesCount);
                SetWaterMap(map, waterTiles, deepEnoughTilesCount, firstThreshold);
            }   
        }

        private List<WaterTile> GetAllWaterTiles(float[,] waterDeapnesMap, float firstThreshold, out int deepEnoughTilesCount)
        {
            deepEnoughTilesCount = 0;
            List<WaterTile> waterPoints = new List<WaterTile>();
            for (int i = 0; i < waterDeapnesMap.GetLength(0); i++)
            {
                for (int j = 0; j < waterDeapnesMap.GetLength(1); j++)
                {
                    waterPoints.Add(new WaterTile(waterDeapnesMap[i, j], new Vector2Int(j, i)));
                    if (waterDeapnesMap[i, j] >= firstThreshold)
                        ++deepEnoughTilesCount;
                }
            }

            return waterPoints.OrderByDescending(x => x.DeepnessValue).ToList();
        }

        private int CalculateWaterTilesCount(int deepEnoughTilesCount, int totalTilesCount)
        {
            if (deepEnoughTilesCount < (waterNoiseMapParameters.MinWaterPercent * totalTilesCount))
                return (int)(waterNoiseMapParameters.MinWaterPercent * totalTilesCount);
            else if (deepEnoughTilesCount > (waterNoiseMapParameters.MaxWaterPercent * totalTilesCount))
                return (int)(waterNoiseMapParameters.MaxWaterPercent * totalTilesCount);
            else
                return deepEnoughTilesCount;
        }

        private void SetWaterMap(TilesMap map, List<WaterTile> waterTiles, int deepEnoughTilesCount, float firstThreshold)
        {
            int waterTilesCount = CalculateWaterTilesCount(deepEnoughTilesCount, waterTiles.Count);
            if(waterTilesCount > 0)
            {
                WaterTile shallowestWaterTile = waterTiles[waterTilesCount - 1];
                float valueOffset = -shallowestWaterTile.DeepnessValue + firstThreshold;

                for (int i = 0; i < waterTilesCount; ++i)
                {
                    WaterTile waterTile = waterTiles[i];
                    Vector2Int position = waterTile.Position;
                    SetWaterTile(map[position.Y, position.X], waterTile.DeepnessValue + valueOffset);
                }
            }  
        }

        private void SetWaterTile(Tile tile, float value)
        {
            tile.WaterBiom = CalculateWaterBiom(value);
            tile.WaterDeepness = value;
        }

        private BiomModel CalculateWaterBiom(float value)
        {
            BiomModel biom = null;

            for(int i=0; i< waterBiomes.Length; ++i)
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