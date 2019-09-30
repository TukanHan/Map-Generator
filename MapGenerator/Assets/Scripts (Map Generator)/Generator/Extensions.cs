using System;

namespace MapGenerator.Generator
{
    public static class Extensions
    {
        public static double RandomPercent(this Random random)
        {
            return random.NextDouble() * 100;
        }

        public static bool RandomByThreshold(this Random random, double threshold)
        {
            return random.RandomPercent() < threshold;
        }
    }
}
