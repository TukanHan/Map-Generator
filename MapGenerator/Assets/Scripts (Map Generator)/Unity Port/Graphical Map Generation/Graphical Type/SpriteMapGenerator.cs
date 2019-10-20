using MapGenerator.DataModels;
using UnityEngine;

namespace MapGenerator.UnityPort
{
    public class SpriteMapGenerator : IGraphicalMapGenerator
    {
        private Transform groundParent;
        private Transform waterParent;
        private readonly ISpaceOrientation spaceOrientation;

        public SpriteMapGenerator(ISpaceOrientation spaceOrientation)
        {
            this.spaceOrientation = spaceOrientation;
        }

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

                    Vector3 position = spaceOrientation.GetPositionVector(new Vector2Float(j, i));

                    CreateTile(position, tile.Biom.Ground.AbstractObject as Sprite, groundParent, SortingLayers.Ground);

                    if (tile.WaterBiom != null)
                    {
                        CreateTile(position, tile.WaterBiom.Ground.AbstractObject as Sprite,
                            waterParent, SortingLayers.Water);
                    }
                }
            }
        }

        private GameObject CreateTile(Vector3 position, Sprite image, Transform parent, string layer)
        {
            GameObject gameObject = new GameObject();
            gameObject.transform.parent = parent;
            gameObject.transform.position = position;
            gameObject.transform.Rotate(spaceOrientation.GetSpriteRotationVector());

            SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.drawMode = SpriteDrawMode.Sliced;
            spriteRenderer.sprite = image;
            spriteRenderer.sortingLayerName = layer;

            return gameObject;
        }
    }
}