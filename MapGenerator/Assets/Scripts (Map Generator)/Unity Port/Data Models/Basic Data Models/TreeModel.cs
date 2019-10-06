using System.Collections.Generic;
using UnityEngine;

namespace MapGenerator.UnityPort
{
    [CreateAssetMenu(menuName = "Map Generator/Tree Data")]
    public class TreeModel : PrefabModel, IDataModelValidation
    {
        public float minScale = 0.8f;
        public float maxScale = 1.5f;

        public DataModels.TreeModel ToModel()
        {
            return new DataModels.TreeModel()
            {
                AbstractObject = prefab,
                MinScale = minScale,
                MaxScale = maxScale
            };
        }

        public IEnumerable<ValidationError> Validate()
        {
            DataModelValidator dataModelValidator = new DataModelValidator();
            dataModelValidator.ValidateProperty(prefab, "Prefab");

            return dataModelValidator.Errors;
        }
    }
}