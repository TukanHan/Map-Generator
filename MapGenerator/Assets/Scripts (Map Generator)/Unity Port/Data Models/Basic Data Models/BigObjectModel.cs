using System.Collections.Generic;
using UnityEngine;

namespace MapGenerator.UnityPort
{
    [CreateAssetMenu(menuName = "Map Generator/Big Object")]
    public class BigObjectModel : PrefabModel, IDataModelValidation
    {
        public int tileWidthCount = 1;
        public int tileHeightCount = 1;

        public DataModels.BigObjectModel ToModel()
        {
            return new DataModels.BigObjectModel()
            {
                AbstractObject = prefab,
                TileHeightCount = tileHeightCount,
                TileWidthCount = tileWidthCount
            }; ;
        }

        public IEnumerable<ValidationError> Validate()
        {
            DataModelValidator dataModelValidator = new DataModelValidator();
            dataModelValidator.ValidateProperty(prefab, "Prefab");

            return dataModelValidator.Errors;
        }
    }
}