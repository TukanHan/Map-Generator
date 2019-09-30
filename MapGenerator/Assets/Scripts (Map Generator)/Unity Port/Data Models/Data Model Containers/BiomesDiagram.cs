using System;
using System.Collections.Generic;

namespace MapGenerator.UnityPort
{
    [Serializable]
    public class BiomesDiagram : IDataModelValidation
    {
        public const int MaxXLayerCount = 6;
        public const int MaxYLayerCount = 6;

        public int temperatureLayerCount = 2;
        public int heightLayerCount = 3;

        public Biom[] biomes = new Biom[MaxXLayerCount * MaxYLayerCount];

        public Biom this[int i, int j]
        {
            get { return biomes[i * MaxYLayerCount + j]; }
            set { biomes[i * MaxYLayerCount + j] = value; }
        }

        public DataModels.BiomModel[,] ToModel()
        {
            DataModels.BiomModel[,] biomArray = new DataModels.BiomModel[heightLayerCount, temperatureLayerCount];

            for (int i = 0; i < heightLayerCount; i++)
            {
                for (int j = 0; j < temperatureLayerCount; j++)
                {
                    biomArray[i, j] = this[i,j].ToModel();
                }
            }

            return biomArray;
        }

        public IEnumerable<ValidationError> Validate()
        {
            List<ValidationError> errors = new List<ValidationError>();

            for (int i = 0; i < heightLayerCount; ++i)
            {
                for (int j = 0; j < temperatureLayerCount; ++j)
                {
                    string elementName = $"Biomes diagram (height: {heightLayerCount - i} temperature: {j+1})";

                    if (this[i, j] == null)
                        errors.Add(new ValidationError(ValidationErrorType.EmptyCollection, elementName));
                    else
                        errors.AddRange(ValidateCollection(this[i, j], elementName));
                }
            }

            return errors;
        }

        private IEnumerable<ValidationError> ValidateCollection(IDataModelValidation dataModel, string parent)
        {
            var childErrors = dataModel.Validate();

            foreach (var childError in childErrors)
                childError.CallStack.Push(parent);

            return childErrors;
        }
    }
}
