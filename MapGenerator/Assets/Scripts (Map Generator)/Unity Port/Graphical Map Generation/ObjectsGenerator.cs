using MapGenerator.DataModels;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenerator.UnityPort
{
    public class ObjectsGenerator
    {
        private Transform objectParent;

        public void Render(Transform parentTransform, List<AwaitingObject> awaitingObjects)
        {
            objectParent = CreateTransform("Object Parent", parentTransform);

            DrawObjects(awaitingObjects);
        }

        private Transform CreateTransform(string name, Transform parent)
        {
            GameObject gameObject = new GameObject(name);
            gameObject.transform.parent = parent;

            return gameObject.transform;
        }

        private void DrawObjects(List<AwaitingObject> awaitingObjects)
        {
            foreach (AwaitingObject ao in awaitingObjects)
            {
                CreateObject(ao.AbstractObject as GameObject, ao.Position, ao.Scale);
            }
        }

        private GameObject CreateObject(GameObject prefab, Vector2Float position, float scale)
        {
            GameObject gameObject = GameObject.Instantiate(prefab, new Vector3(position.X, 0, position.Y), Quaternion.identity, objectParent);
            gameObject.transform.localScale = new Vector3(scale, scale, scale);

            return gameObject;
        }
    }
}
