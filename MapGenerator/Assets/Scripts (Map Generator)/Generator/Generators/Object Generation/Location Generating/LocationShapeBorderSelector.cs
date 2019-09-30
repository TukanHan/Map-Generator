using System.Collections.Generic;

namespace MapGenerator.Generator
{
    public class LocationShapeBorderSelector
    {
        public List<Vector2Int> ShapeBorder { get; private set; } = new List<Vector2Int>();
        public List<LocationInstance> NeighboringLocations { get; private set; } = new List<LocationInstance>();

        private bool[,] locationShapeMap;
        private readonly LocationsMap locationsMap;

        public LocationShapeBorderSelector(LocationsMap locationsMap, bool[,] locationShapeMap, List<Vector2Int> locationShapeBlocksPos)
        {
            this.locationsMap = locationsMap;
            this.locationShapeMap = locationShapeMap;

            SelectBorder(locationShapeBlocksPos);
        }

        private void SelectBorder(List<Vector2Int> locationShapeBlocksPos)
        {
            foreach (Vector2Int pos in locationShapeBlocksPos)
            {
                if (IsBlockOnShapeBorder(pos))
                    ShapeBorder.Add(pos);
            }
        }

        private bool IsBlockOnShapeBorder(Vector2Int pos)
        {
            bool blockOnShapeBorder = false;

            for (int i = pos.Y - 1; i <= pos.Y + 1; ++i)
            {
                for (int j = pos.X - 1; j <= pos.X + 1; ++j)
                {
                    if (locationsMap.IsOnMap(new Vector2Int(j, i)))
                    {
                        LocationInstance location = locationsMap.GetTileLocation(new Vector2Int(j, i));
                        if (location != null && !NeighboringLocations.Contains(location))
                            NeighboringLocations.Add(location);

                        if (!locationShapeMap[i, j])
                            blockOnShapeBorder = true;
                    }
                }
            }

            return blockOnShapeBorder;
        }
    }
}