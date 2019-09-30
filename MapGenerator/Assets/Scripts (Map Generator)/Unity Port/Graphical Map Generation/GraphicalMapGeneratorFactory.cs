using System;

namespace MapGenerator.UnityPort
{
    public class GraphicalMapGeneratorFactory
    {
        public IGraphicalMapGenerator GetGraphicalMapGenerator(GraphicalGenerationType generationType)
        {
            switch (generationType)
            {
                case GraphicalGenerationType.Sprites:
                    return new SpriteMapGenerator();
                case GraphicalGenerationType.TileMap:
                    return new TileMapGenerator();
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
