namespace MapGenerator.DataModels
{
    /// <summary>
    /// Data about an abstract object waiting to generate.
    /// </summary>
    public class AwaitingObject : AbstractObjectModel
    {
        public Vector2Float Position { get; set; }
        public float Scale { get; set; } = 1;
    }
}