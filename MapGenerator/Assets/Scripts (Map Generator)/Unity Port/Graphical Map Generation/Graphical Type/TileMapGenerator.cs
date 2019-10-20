using System.Collections.Generic;
using MapGenerator.DataModels;
using UnityEngine;
using UnityEngine.Tilemaps;
using Tile = UnityEngine.Tilemaps.Tile;

namespace MapGenerator.UnityPort
{
    public class TileMapGenerator : IGraphicalMapGenerator
    {
        private Tilemap groundTilemap;
        private Tilemap waterTilemap;

        private Dictionary<Sprite, Tile> tileDictionary = new Dictionary<Sprite, Tile>();
        private readonly ISpaceOrientation spaceOrientation;

        public TileMapGenerator(ISpaceOrientation spaceOrientation)
        {
            this.spaceOrientation = spaceOrientation;
        }

        public void Render(Transform parentTransform, TilesMap map)
        {
            Grid grid = CreateGrid(parentTransform);
            groundTilemap = CreateTilemap(grid.transform, "Ground Tilemap", SortingLayers.Ground);
            waterTilemap = CreateTilemap(grid.transform, "Water Tilemap", SortingLayers.Water);
            DrawMap(map);
        }

        private void DrawMap(TilesMap map)
        {
            for(int y = 0; y < map.Height; ++y)
            {
                for(int x =0; x<map.Width; ++x)
                {
                    DataModels.Tile tile = map[y, x];

                    Tile groundTile = GetTileSO(tile.Biom.Ground.AbstractObject as Sprite);
                    groundTilemap.SetTile(new Vector3Int(x, y, 0), groundTile);

                    if(tile.WaterBiom != null)
                    {
                        Tile waterTile = GetTileSO(tile.WaterBiom.Ground.AbstractObject as Sprite);
                        waterTilemap.SetTile(new Vector3Int(x, y, 0), waterTile);
                    }
                }
            }
        }

        private Tile GetTileSO(Sprite sprite)
        {
            bool tileAlreadyExist = tileDictionary.TryGetValue(sprite, out Tile tile);
            if (!tileAlreadyExist)
            {
                tile = (Tile)ScriptableObject.CreateInstance("Tile");
                tile.sprite = sprite;
                tileDictionary.Add(sprite, tile);
            }

            return tile;
        }

        private Grid CreateGrid(Transform parent)
        {
            GameObject gameObject = new GameObject("Grid");
            gameObject.transform.parent = parent;

            Grid grid = gameObject.AddComponent<Grid>();
            grid.cellSwizzle = spaceOrientation.GetGridOrientation();

            return grid;
        }

        private Tilemap CreateTilemap(Transform parent, string name, string layer)
        {
            GameObject gameObject = new GameObject(name);
            gameObject.transform.parent = parent;

            TilemapRenderer tilemapRenderer = gameObject.AddComponent<TilemapRenderer>();
            tilemapRenderer.sortingLayerName = layer;

            Tilemap tilemap = gameObject.GetComponent<Tilemap>();
            tilemap.orientation = spaceOrientation.GetTilemapOrientation();
            tilemap.tileAnchor = new Vector3();

            return tilemap;
        }
    }
}
