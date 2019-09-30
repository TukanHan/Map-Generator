using MapGenerator.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MapGenerator.Generator
{
    public class RouletteWheelSelector
    {
        private Random random;

        public RouletteWheelSelector(Random random)
        {
            this.random = random;
        }

        public T RouletteWheelSelection<T>(List<PriorityModel<T>> prefabModels)
        {
            int sum = prefabModels.Sum(x => x.Priority);

            int randomValue = random.Next(sum);
            int currentValue = 0;
            int currentPrefabIndex = -1;

            do
            {
                currentPrefabIndex++;
                currentValue += prefabModels[currentPrefabIndex].Priority;
            }
            while (currentValue < randomValue);

            return prefabModels[currentPrefabIndex].Model;
        }
    }
}

