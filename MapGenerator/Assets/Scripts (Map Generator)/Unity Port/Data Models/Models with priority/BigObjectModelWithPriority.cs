using MapGenerator.DataModels;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenerator.UnityPort
{
    [Serializable]
    public class BigObjectModelWithPriority : ModelWithPriority<BigObjectModel>, IDataModelValidation
    {
        [Range(1, 100)]
        public int maxCount = 100;

        public PriorityModel<DataModels.BigObjectModel> ToModel()
        {
            return new PriorityModel<DataModels.BigObjectModel>()
            {
                Model = model.ToModel(),
                Priority = priority,
                MaxCount = maxCount
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
