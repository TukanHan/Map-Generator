using MapGenerator.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MapGenerator.UnityPort
{
    [Serializable]
    public class WaterLayers : IDataModelValidation
    {
        public List<WaterBiom> waterBiomes= new List<WaterBiom>();

        public void AddWaterLevel()
        {
            float newLevelThreshold = 0.5f;
            if (waterBiomes.Any())
                newLevelThreshold = waterBiomes.Last().waterThresholding + 0.01f;

            waterBiomes.Add(new WaterBiom { waterThresholding = newLevelThreshold });
        }

        public WaterBiomModel[] ToModel()
        {
            return waterBiomes.Select(w => w.ToModel()).ToArray();
        }

        public IEnumerable<ValidationError> Validate()
        {
            DataModelValidator dataModelValidator = new DataModelValidator();
            dataModelValidator.ValidateList(waterBiomes, "Water Level");
            dataModelValidator.Errors.AddRange(ValidateLevelsThresholding());

            return dataModelValidator.Errors;
        }

        private IEnumerable<ValidationError> ValidateLevelsThresholding()
        {
            List<ValidationError> errors = new List<ValidationError>();
            for(int i=1; i<waterBiomes.Count; ++i)
            {
                if (waterBiomes[i - 1].waterThresholding >= waterBiomes[i].waterThresholding)
                    errors.Add(new ValidationError(ValidationErrorType.IncorrectData, $"Water Level {i}", $"Threshold of {i} level must be greater than previous level"));
            }

            return errors;
        }
    }
}
