using MapGenerator.DataModels;
using UnityEngine;

namespace MapGenerator.UnityPort
{
    public class SpriteMapGenerator : IGraphicalMapGenerator
    {
        private Transform groundParent;
        private Transform waterParent;

        public void Render(Transform parentTransform, TilesMap map)
        {
            groundParent = CreateTransform("Ground Parent", parentTransform);
            waterParent = CreateTransform("Water Parent", parentTransform);

            DrawMap(map);
        }

        private Transform CreateTransform(string name, Transform parent)
        {
            GameObject gameObject = new GameObject(name);
            gameObject.transform.parent = parent;

            return gameObject.transform;
        }

        private void DrawMap(TilesMap map)
        {
            for (int i = 0; i < map.Height; ++i)
            {
                for (int j = 0; j < map.Width; ++j)
                {
                    Tile tile = map[i, j];

                    CreateTile(new Vector2Float(j, i), tile.Biom.Ground.AbstractObject as Sprite, groundParent, Color.white, SortingLayers.Default);

                    if (tile.WaterBiom != null)
                    {
                        CreateTile(new Vector2Float(j, i), tile.WaterBiom.Ground.AbstractObject as Sprite, waterParent,
                            new Color(1, 1, 1, (tile.WaterDeepness - 0.35f) * 5f), SortingLayers.Water);
                    }
                }
            }
        }

        private GameObject CreateTile(Vector2Float position, Sprite image, Transform parent, Color color, string layer)
        {
            GameObject gameObject = new GameObject();
            gameObject.transform.parent = parent;
            gameObject.transform.position = new Vector3(position.X, 0, position.Y);
            gameObject.transform.Rotate(new Vector3(90, 0, 0));

            SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.drawMode = SpriteDrawMode.Sliced;
            spriteRenderer.sprite = image;
            spriteRenderer.color = color;
            spriteRenderer.sortingLayerName = layer;

            return gameObject;
        }
    }
}