using MapGenerator.DataModels;
using System;

namespace MapGenerator.Generator
{
    public class BiomMapGenerator
    {
        private readonly BiomModel[,] biomes;

        private readonly GroundNoiseMapParametersModel temperatureNoiseMapParameters;
        private readonly GroundNoiseMapParametersModel heightNoiseMapParameters;

        private NoiseMapGenerator noiseGenerator;

        private float temperatureLayerCountInversion;
        private float heightLayerCountInversion;

        public BiomMapGenerator(Random random,
                                BiomModel[,] biomes,
                                GroundNoiseMapParametersModel temperatureNoiseMapParameters,
                                GroundNoiseMapParametersModel heightNoiseMapParameters)
        {
            this.temperatureNoiseMapParameters = temperatureNoiseMapParameters;
            this.heightNoiseMapParameters = heightNoiseMapParameters;
            this.biomes = biomes;

            noiseGenerator = new NoiseMapGenerator(random);
        }

        public void GenerateBiomMap(TilesMap map)
        {
            float[,] heightNoiseArray = noiseGenerator.Generate(heightNoiseMapParameters, map.Width, map.Height);
            float[,] temperatureNoiseArray = noiseGenerator.Generate(temperatureNoiseMapParameters, map.Width, map.Height);

            temperatureLayerCountInversion = 1f / biomes.GetLength(1);
            heightLayerCountInversion = 1f / biomes.GetLength(0);

            for (int i = 0; i < map.Height; ++i)
            {
                for (int j = 0; j < map.Width; ++j)
                {
                    Tile tile = map[i, j];
                    tile.Height = ScaleValue(heightNoiseArray[i, j], heightNoiseMapParameters);
                    tile.Temperature = ScaleValue(temperatureNoiseArray[i, j], temperatureNoiseMapParameters);
                    tile.Biom = CalculateBiom(tile.Temperature, tile.Height);
                }
            }
        }

        private BiomModel CalculateBiom(float temperature, float height)
        {
            int x = (int)(temperature / temperatureLayerCountInversion);
            int y = (int)(height / heightLayerCountInversion);

            return biomes[y,x];
        }

        private float ScaleValue(float value, GroundNoiseMapParametersModel noiseMapParameters)
        {
            float scale = noiseMapParameters.MaxValue - noiseMapParameters.MinValue;
            return scale * value + noiseMapParameters.MinValue;
        }
    }
}