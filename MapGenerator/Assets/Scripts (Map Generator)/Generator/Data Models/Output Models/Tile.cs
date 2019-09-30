namespace MapGenerator.DataModels
{
    /// <summary>
    /// Complete data for map tile.
    /// </summary>
    public class Tile
    {
        public BiomModel Biom { get; set; }
        public BiomModel WaterBiom { get; set; }

        public bool HasWaterBiom
        {
            get { return WaterBiom != null; }
        }
        public float WaterDeepness { get; set; }
        public float Temperature { get; set; }
        public float Height { get; set; }

        public BiomModel GetBiomModel()
        {
            if (HasWaterBiom)
                return WaterBiom;
            else
                return Biom;
        }
    }
}