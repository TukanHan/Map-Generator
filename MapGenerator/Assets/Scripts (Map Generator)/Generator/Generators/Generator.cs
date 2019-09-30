using MapGenerator.DataModels;
using System;
using System.Collections.Generic;

namespace MapGenerator.Generator
{
    public class Generator
    {
        public List<AwaitingObject> AwaitingObjects { get; private set; }
        public TilesMap Map { get; set; }

        private readonly WaterBiomModel[] waterBiomes;
        private readonly BiomModel[,] biomes;

        private readonly WaterNoiseMapParametersModel waterNoiseMapParameters;
        private readonly GroundNoiseMapParametersModel heightNoiseMapParameters;
        private readonly GroundNoiseMapParametersModel temperatureNoiseMapParameters;     

        private readonly Random random;

        public Generator(int width, int height, int seed,
                         WaterBiomModel[] waterBiomes,
                         BiomModel[,] biomes,
                         WaterNoiseMapParametersModel waterNoiseMapParameters,
                         GroundNoiseMapParametersModel heightNoiseMapParameters,
                         GroundNoiseMapParametersModel temperatureNoiseMapParameters)
        {
            this.biomes = biomes;
            this.waterBiomes = waterBiomes;
            this.waterNoiseMapParameters = waterNoiseMapParameters;
            this.heightNoiseMapParameters = heightNoiseMapParameters;
            this.temperatureNoiseMapParameters = temperatureNoiseMapParameters;

            random = new Random(seed);
            Map = new TilesMap(width, height);
        }

        public void Generate()
        {
            BiomMapGenerator biomMapGenerator = new BiomMapGenerator(random, biomes, temperatureNoiseMapParameters, heightNoiseMapParameters);
            biomMapGenerator.GenerateBiomMap(Map);

            BiomMapSmoother biomMapSmoother = new BiomMapSmoother();
            biomMapSmoother.SmoothBiomMap(Map);

            WaterMapGenerator waterMapGenerator = new WaterMapGenerator(random, waterBiomes, waterNoiseMapParameters);
            waterMapGenerator.GenerateWaterMap(Map);

            WaterMapSmoother waterMapSmoother = new WaterMapSmoother(waterBiomes);
            waterMapSmoother.SmoothWaterMap(Map);

            ObjectGenerator objectGenerator = new ObjectGenerator(Map, random);
            AwaitingObjects = objectGenerator.Generate();
        }
    }
}