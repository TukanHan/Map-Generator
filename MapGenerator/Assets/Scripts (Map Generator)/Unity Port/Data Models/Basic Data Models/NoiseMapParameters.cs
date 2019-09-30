using System;
using UnityEngine;

namespace MapGenerator.UnityPort
{
    [Serializable]
    public class NoiseMapParameters
    {
        [Range(1,8)]
        public int octaves = 5;

        [Range(10,500)]
        public float frequency = 150;

        [Range(0, 1)]
        public float targetValue = 0.5f;

        public DataModels.NoiseMapParametersModel ToModel()
        {
            return new DataModels.NoiseMapParametersModel
            {
                Octaves = octaves,
                Frequency = frequency,
                TargetValue = targetValue
            };
        }
    }
}
