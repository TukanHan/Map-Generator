using System.Collections.Generic;

namespace MapGenerator.DataModels
{
    public class LocationModel
    {
        public int MinSize { get; set; }
        public int MaxSize { get; set; }

        public float ChanceForFence { get; set; }
        public List<PriorityModel<FenceModel>> Fences { get; set; }

        public float BigObjectsIntensity { get; set; }
        public List<PriorityModel<BigObjectModel>> BigObjects { get; set; }

        public float ObjectsIntensity { get; set; }
        public List<PriorityModel<ObjectModel>> Objects { get; set; }
    }
}
