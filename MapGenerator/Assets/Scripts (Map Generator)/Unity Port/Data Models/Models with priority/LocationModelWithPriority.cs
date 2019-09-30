using MapGenerator.DataModels;
using System;
using System.Collections.Generic;

namespace MapGenerator.UnityPort
{
    [Serializable]
    public class LocationModelWithPriority : ModelWithPriority<LocationModel>, IDataModelValidation
    {
        public PriorityModel<DataModels.LocationModel> ToModel()
        {
            return new PriorityModel<DataModels.LocationModel>()
            {
                Priority = priority,
                Model = model.ToModel()
            };
        }

        public IEnumerable<ValidationError> Validate()
        {
            DataModelValidator dataModelValidator = new DataModelValidator();
            dataModelValidator.ValidateModel(model, "Model");

            return dataModelValidator.Errors;
        }
    }
}