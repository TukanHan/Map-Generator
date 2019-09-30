using System.Collections.Generic;

namespace MapGenerator.DataModels
{
    public class BiomModel
    {
        public AbstractObjectModel Ground { get; set; }

        public float TreesIntensity { get; set; }
        public List<PriorityModel<TreeModel>> Trees { get; set; }

        public float ObjectsIntensity { get; set; }
        public List<PriorityModel<ObjectModel>> Objects { get; set; }

        public float LocationsIntensity { get; set; }
        public List<PriorityModel<LocationModel>> Locations { get; set; }
    }
}
