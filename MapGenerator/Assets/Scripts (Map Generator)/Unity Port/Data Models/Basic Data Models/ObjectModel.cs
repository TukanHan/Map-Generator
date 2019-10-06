using System.Collections.Generic;
using UnityEngine;

namespace MapGenerator.UnityPort
{
    [CreateAssetMenu(menuName = "Map Generator/Object")]
    public class ObjectModel : PrefabModel, IDataModelValidation
    {
        public DataModels.ObjectModel ToModel()
        {
            return new DataModels.ObjectModel()
            {
                AbstractObject = prefab
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