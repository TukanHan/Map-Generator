using MapGenerator.DataModels;
using System;
using System.Collections.Generic;

namespace MapGenerator.UnityPort
{
    [Serializable]
    public class ObjectModelWithPriority : ModelWithPriority<ObjectModel>, IDataModelValidation
    {
        public PriorityModel<DataModels.ObjectModel> ToModel()
        {
            return new PriorityModel<DataModels.ObjectModel>()
            {
                Model = model.ToModel(),
                Priority = priority
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
