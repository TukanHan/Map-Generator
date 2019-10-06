using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MapGenerator.UnityPort
{
    [CreateAssetMenu(menuName = "Map Generator/Biom Data")]
    public class Biom : ScriptableObject, IDataModelValidation
    {
        public Sprite ground;

        [Range(0, 100)]
        public float treesIntensity;
        public List<TreeModelWithPriority> trees;

        [Range(0, 100)]
        public float objectsIntensity;
        public List<ObjectModelWithPriority> objects;

        [Range(0, 100)]
        public float locationsIntensity;
        public List<LocationModelWithPriority> locations;

        public DataModels.BiomModel ToModel()
        {
            return new DataModels.BiomModel
            {
                Ground = new DataModels.AbstractObjectModel() { AbstractObject = ground },
                TreesIntensity = treesIntensity,
                Trees = trees.Select(t => t.ToModel()).ToList(),
                ObjectsIntensity = objectsIntensity,
                Objects = objects.Select(o => o.ToModel()).ToList(),
                LocationsIntensity = locationsIntensity,
                Locations = locations.Select(l => l.ToModel()).ToList()
            };
        }

        public IEnumerable<ValidationError> Validate()
        {
            DataModelValidator dataModelValidator = new DataModelValidator();
            dataModelValidator.ValidateProperty(ground, "Ground");
            dataModelValidator.ValidateList(trees, "Trees");
            dataModelValidator.ValidateList(locations, "Locations");
            dataModelValidator.ValidateList(objects, "Objects");

            return dataModelValidator.Errors;
        }
    }
}