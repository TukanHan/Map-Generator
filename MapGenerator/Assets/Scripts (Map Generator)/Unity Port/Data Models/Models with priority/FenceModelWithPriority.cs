using MapGenerator.DataModels;
using System;
using System.Collections.Generic;

namespace MapGenerator.UnityPort
{
    [Serializable]
    public class FenceModelWithPriority : ModelWithPriority<FenceModel>, IDataModelValidation
    {
        public PriorityModel<DataModels.FenceModel> ToModel()
        {
            return new PriorityModel<DataModels.FenceModel>()
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