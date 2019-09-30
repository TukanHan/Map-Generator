using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenerator.UnityPort
{
    [Serializable]
    public class WaterBiom : IDataModelValidation
    {
        [Range(0, 1)]
        [Tooltip("The threshold above which will be generated water biom.")]
        public float waterThresholding = 0.5f;

        [Tooltip("Biom generated at a given water level.")]
        public Biom biom;

        public DataModels.WaterBiomModel ToModel()
        {
            return new DataModels.WaterBiomModel
            {
                Biom = biom.ToModel(),
                WaterThresholding = waterThresholding
            };
        }

        public IEnumerable<ValidationError> Validate()
        {
            DataModelValidator dataModelValidator = new DataModelValidator();
            dataModelValidator.ValidateModel(biom, "Biom");

            return dataModelValidator.Errors;
        }
    }
}
