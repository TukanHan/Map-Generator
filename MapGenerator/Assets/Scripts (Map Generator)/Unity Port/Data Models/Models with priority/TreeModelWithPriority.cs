using MapGenerator.DataModels;
using System;
using System.Collections.Generic;

namespace MapGenerator.UnityPort
{
    [Serializable]
    public class TreeModelWithPriority : ModelWithPriority<TreeModel>, IDataModelValidation
    {
        public PriorityModel<DataModels.TreeModel> ToModel()
        {
            return new PriorityModel<DataModels.TreeModel>()
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