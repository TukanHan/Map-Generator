using System;

namespace MapGenerator.UnityPort
{
    public class GraphicalMapGeneratorFactory
    {
        public IGraphicalMapGenerator GetGraphicalMapGenerator(GraphicalGenerationType generationType, ISpaceOrientation spaceOrientation)
        {
            switch (generationType)
            {
                case GraphicalGenerationType.Sprites:
                    return new SpriteMapGenerator(spaceOrientation);
                case GraphicalGenerationType.TileMap:
                    return new TileMapGenerator(spaceOrientation);
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
