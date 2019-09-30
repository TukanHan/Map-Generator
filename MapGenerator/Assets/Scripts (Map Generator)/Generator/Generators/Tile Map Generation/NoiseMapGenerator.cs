using MapGenerator.DataModels;
using System;

namespace MapGenerator.Generator
{
    public class NoiseMapGenerator
    {
        private float xOff;
        private float yOff;

        private Random random;

        public NoiseMapGenerator(Random random)
        {
            this.random = random;
        }

        public float[,] Generate(NoiseMapParametersModel noiseMapParameters, int width, int height)
        {
            xOff = random.Next(0, 1000000);
            yOff = random.Next(0, 1000000);

            float[,] map = new float[height, width];

            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    map[i, j] = Clamp(CalculateValue(noiseMapParameters, j, i), 0, 0.999f);
                }
            }

            return map;
        }

        private float Clamp(float value, float min, float max)
        {
            return value > max ? max : (value < min ? min : value);
        }

        private float CalculateValue(NoiseMapParametersModel parameters, float x, float y)
        {
            float noise = 0.0f;
            float gain = 1.0f;
            for (int i = 0; i < parameters.Octaves; ++i)
            {
                float value = ImprovedNoise.Noise2D((x + xOff) * gain / parameters.Frequency, (y + yOff) * gain / parameters.Frequency);
                noise += value * parameters.TargetValue / gain;     
                gain *= 2;
            }

            return noise;
        }
    }
}