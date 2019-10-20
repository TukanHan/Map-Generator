using System;
using UnityEngine;

namespace MapGenerator.UnityPort
{
    [Serializable]
    public class GroundNoiseMapParameters : NoiseMapParameters
    {
        [Range(0, 1)]
        public float minValue = 0;

        [Range(0, 1)]
        public float maxValue = 1;

        public new DataModels.GroundNoiseMapParametersModel ToModel()
        {
            return new DataModels.GroundNoiseMapParametersModel
            {
                Octaves = octaves,
                Frequency = frequency,
                TargetValue = CalculateTargetValue(),
                MinValue = minValue,
                MaxValue = maxValue
            };
        }
    }
}
