using MapGenerator.DataModels;
using System.Collections.Generic;

namespace MapGenerator.Generator
{
    public class LocationInstance
    {
        public LocationModel Template { get; }

        public List<Vector2Int> Tiles { get; set; } = new List<Vector2Int>();
        public LocationShape Shape { get; set; }

        public bool HasFence { get { return Fence.Count > 0; } }
        public List<AwaitingObject> Fence { get; } = new List<AwaitingObject>();
        public List<AwaitingObject> BigObjects { get; } = new List<AwaitingObject>();
        public List<AwaitingObject> Objects { get; } = new List<AwaitingObject>();

        public LocationInstance(LocationModel template)
        {
            this.Template = template;
        }
    }
}
