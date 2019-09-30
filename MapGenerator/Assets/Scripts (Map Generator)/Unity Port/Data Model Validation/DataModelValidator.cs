using System.Collections.Generic;
using UnityEngine;

namespace MapGenerator.UnityPort
{
    public class DataModelValidator
    {
        public List<ValidationError> Errors { get; set; } = new List<ValidationError>();

        public void ValidateProperty(Object property, string propertyName)
        {
            if (property == null)
            {
                Errors.Add(new ValidationError(ValidationErrorType.EmptyProperty, propertyName));
            }
        }

        public void ValidateModel(IDataModelValidation dataModel, string dataModelName)
        {
            if (dataModel == null)
                Errors.Add(new ValidationError(ValidationErrorType.EmptyProperty, dataModelName));
            else
                Errors.AddRange(ValidateChild(dataModel, dataModelName));
        }

        public void ValidateList<T>(List<T> list, string collectionName) where T : IDataModelValidation
        {
            if (list == null)
            {
                Errors.Add(new ValidationError(ValidationErrorType.EmptyCollection, collectionName));
            }
            else
            {
                for (int i = 0; i < list.Count; ++i)
                {
                    string elementName = $"{collectionName}[{i}]";

                    if (list[i] == null)
                        Errors.Add(new ValidationError(ValidationErrorType.EmptyCollectionElement, elementName));
                    else
                        Errors.AddRange(ValidateChild(list[i], elementName));
                }
            }
        }

        private IEnumerable<ValidationError> ValidateChild(IDataModelValidation dataModel, string parent)
        {
            var childErrors = dataModel.Validate();

            foreach (var childError in childErrors)
                childError.CallStack.Push(parent);

            return childErrors;
        }
    }
}