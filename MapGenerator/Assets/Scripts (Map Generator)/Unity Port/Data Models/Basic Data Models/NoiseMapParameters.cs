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

        [Range(0f, 1f)]
        public float targetValue = 1f;

        public DataModels.NoiseMapParametersModel ToModel()
        {
            return new DataModels.NoiseMapParametersModel
            {
                Octaves = octaves,
                Frequency = frequency,
                TargetValue = CalculateTargetValue()
            };
        }

        protected float CalculateTargetValue()
        {
            return (float)Math.Pow(Math.Log(2.1 -targetValue , 1.6f), 7) + 0.000001f;
        }
    }
}