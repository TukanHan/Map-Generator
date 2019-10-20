using System;
using MapGenerator.DataModels;
using UnityEngine;

namespace MapGenerator.UnityPort
{
    [Serializable]
    public class WaterNoiseMapParameters : NoiseMapParameters
    {
        [Range(0, 1)]
        public float minWaterPercent = 0;

        [Range(0, 1)]
        public float maxWaterPercent = 1;

        public new WaterNoiseMapParametersModel ToModel()
        {
            return new DataModels.WaterNoiseMapParametersModel
            {
                Octaves = octaves,
                Frequency = frequency,
                TargetValue = CalculateTargetValue(),
                MinWaterPercent = minWaterPercent,
                MaxWaterPercent = maxWaterPercent
            };
        }
    }
}
