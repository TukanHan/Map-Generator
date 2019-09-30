using System.Collections.Generic;

namespace MapGenerator.Generator
{
    /// <summary>
    /// To check if a object can be generated in a given place.
    /// </summary>
    public class ObjectsMap
    {
        public enum ObjectType { Empty, Object, BigObject, Fance, Tree };

        private readonly ObjectType[,] objectsOnMap;

        public int Width { get; }
        public int Height { get; }

        public ObjectsMap(int width, int height)
        {
            Width = width;
            Height = height;

            objectsOnMap = new ObjectType[Height, Width];
        }

        public bool IsOnMap(Vector2Int position)
        {
            return position.X >= 0 && position.Y >= 0 && position.X < Width && position.Y < Height;
        }

        public bool IsTileEmpty(Vector2Int position)
        {
            return objectsOnMap[position.Y, position.X] == ObjectType.Empty;
        }

        public bool AreNeighboringTilesEmpty(Vector2Int position)
        {
            for (int i = position.Y - 1; i <= position.Y + 1; ++i)
            {
                for (int j = position.X - 1; j <= position.X + 1; ++j)
                {
                    if (IsOnMap(new Vector2Int(j, i)) && !IsTileEmpty(new Vector2Int(j, i)))
                        return false;
                }
            }

            return true;
        }

        public bool AreTilesEmpty(Vector2Int leftDown, Vector2Int rightUp)
        {
            for (int i = leftDown.Y; i <= rightUp.Y; ++i)
            {
                for (int j = leftDown.X; j <= rightUp.X; ++j)
                {
                    if (!IsOnMap(new Vector2Int(j, i)) || !IsTileEmpty(new Vector2Int(j, i)))
                        return false;
                }
            }

            return true;
        }

        public bool AreTilesEmpty(IEnumerable<Vector2Int> positions)
        {
            foreach (Vector2Int position in positions)
            {
                if (!IsOnMap(position) || !IsTileEmpty(position))
                    return false;
            }

            return true;
        }

        public void MarkTiles(Vector2Int position, ObjectType objectType)
        {
            MarkTiles(position, position, objectType);
        }

        public void MarkTiles(Vector2Int leftDown, Vector2Int rightUp, ObjectType objectType)
        {
            for (int i = leftDown.Y; i <= rightUp.Y; ++i)
            {
                for (int j = leftDown.X; j <= rightUp.X; ++j)
                {
                    if (IsOnMap(new Vector2Int(j, i)))
                        objectsOnMap[i, j] = objectType;
                }
            }
        }
    }
}