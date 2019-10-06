using System.Collections.Generic;
using UnityEngine;

namespace MapGenerator.UnityPort
{
    [CreateAssetMenu(menuName = "Map Generator/Fence")]
    public class FenceModel : ScriptableObject, IDataModelValidation
    {
        public GameObject topLeft;
        public GameObject topLeftInside;
        public GameObject topMiddle;
        public GameObject topRight;
        public GameObject topRightInside;

        public GameObject middleLeft;
        public GameObject middleRight;

        public GameObject bottomLeft;
        public GameObject bottomLeftInside;
        public GameObject bottomMiddle;
        public GameObject bottomRight;
        public GameObject bottomRightInside;

        public DataModels.FenceModel ToModel()
        {
            return new DataModels.FenceModel()
            {
                TopLeft = new DataModels.AbstractObjectModel() { AbstractObject = topLeft },
                TopLeftInside = new DataModels.AbstractObjectModel { AbstractObject = topLeftInside },
                TopMiddle = new DataModels.AbstractObjectModel { AbstractObject = topMiddle },
                TopRight = new DataModels.AbstractObjectModel { AbstractObject = topRight },
                TopRightInside = new DataModels.AbstractObjectModel { AbstractObject = topRightInside },
                MiddleLeft = new DataModels.AbstractObjectModel { AbstractObject = middleLeft },
                MiddleRight = new DataModels.AbstractObjectModel { AbstractObject = middleRight },
                BottomLeft = new DataModels.AbstractObjectModel { AbstractObject = bottomLeft },
                BottomLeftInside = new DataModels.AbstractObjectModel { AbstractObject = bottomLeftInside },
                BottomMiddle = new DataModels.AbstractObjectModel { AbstractObject = bottomMiddle },
                BottomRight = new DataModels.AbstractObjectModel { AbstractObject = bottomRight },
                BottomRightInside = new DataModels.AbstractObjectModel { AbstractObject = bottomRightInside }
            }; ;
        }

        public IEnumerable<ValidationError> Validate()
        {
            DataModelValidator dataModelValidator = new DataModelValidator();
            dataModelValidator.ValidateProperty(topLeft, "TopLeft");
            dataModelValidator.ValidateProperty(topLeftInside, "TopLeftInside");
            dataModelValidator.ValidateProperty(topMiddle, "TopMiddle");
            dataModelValidator.ValidateProperty(topRight, "TopRight");
            dataModelValidator.ValidateProperty(topRightInside, "TopRightInside");
            dataModelValidator.ValidateProperty(middleLeft, "MiddleLeft");
            dataModelValidator.ValidateProperty(middleRight, "MiddleRight");
            dataModelValidator.ValidateProperty(bottomLeft, "BottomLeft");
            dataModelValidator.ValidateProperty(bottomLeftInside, "BottomLeftInside");
            dataModelValidator.ValidateProperty(bottomMiddle, "BottomMiddle");
            dataModelValidator.ValidateProperty(bottomRight, "BottomRight");
            dataModelValidator.ValidateProperty(bottomRightInside, "BottomRightInside");

            return dataModelValidator.Errors;
        }
    }
}