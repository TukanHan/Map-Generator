namespace MapGenerator.DataModels
{
    public class PriorityModel<T>
    {
        public int Priority { get; set; }
        public int MaxCount { get; set; }
        public T Model { get; set; }
    }
}