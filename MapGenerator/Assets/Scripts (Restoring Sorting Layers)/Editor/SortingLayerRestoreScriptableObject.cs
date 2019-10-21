using MapGenerator.UnityPort;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace SortingLayerRestore
{
    [CreateAssetMenu(menuName = "Restore Sorting Layer")]
    public class SortingLayerRestoreScriptableObject : ScriptableObject
    {
        public string prefabsPath = "Assets/Prefabs";

        public void RestoreSortingLayers()
        {
            AddSortingLayers();
            EditPrefabs();
        }

        private void EditPrefabs()
        {
            string[] assets = AssetDatabase.FindAssets("t:GameObject", new[] { prefabsPath });
            foreach (string asset in assets)
            {
                EditPrefab(AssetDatabase.GUIDToAssetPath(asset));
            }
        }

        private void EditPrefab(string path)
        {
            bool success = false;
            try
            {
                GameObject prefab = PrefabUtility.LoadPrefabContents(path);

                prefab.GetComponent<LayerOrderSetter>().spriteRenderer.sortingLayerName = SortingLayers.Objects;

                EditorUtility.SetDirty(prefab);
                EditorSceneManager.MarkSceneDirty(prefab.scene);

                PrefabUtility.SaveAsPrefabAsset(prefab, path, out success);
                PrefabUtility.UnloadPrefabContents(prefab);

                Debug.Log($"Editing {path} success");
            }
            catch
            {
                Debug.LogWarning($"Editing {path} falied");
            }
        }

        private void AddSortingLayers()
        {
            SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadMainAssetAtPath("ProjectSettings/TagManager.asset"));
            AddSortingLayer(SortingLayers.Ground, serializedObject);
            AddSortingLayer(SortingLayers.Water, serializedObject);
            AddSortingLayer(SortingLayers.Objects, serializedObject);
        }

        private void AddSortingLayer(string layerName, SerializedObject serializedObject)
        {
            SerializedProperty sortingLayers = serializedObject.FindProperty("m_SortingLayers");

            List<int> Ids = new List<int>();
            for (int i = 0; i < sortingLayers.arraySize; i++)
            {
                if (sortingLayers.GetArrayElementAtIndex(i).FindPropertyRelative("name").stringValue.Equals(layerName))
                {
                    Debug.LogWarning($"Layer {layerName} already exist");
                    return;
                }
                Ids.Add(sortingLayers.GetArrayElementAtIndex(i).FindPropertyRelative("uniqueID").intValue);
            }

            sortingLayers.InsertArrayElementAtIndex(sortingLayers.arraySize);
            var newLayer = sortingLayers.GetArrayElementAtIndex(sortingLayers.arraySize - 1);
            newLayer.FindPropertyRelative("name").stringValue = layerName;
            newLayer.FindPropertyRelative("uniqueID").intValue = RandomId(Ids);
            serializedObject.ApplyModifiedProperties();

            Debug.Log($"Layer {layerName} has been added");
        }

        private int RandomId(List<int> usedIds)
        {
            int newId;
            do
            {
                newId = Random.Range(10, 100000);
            }
            while (usedIds.Contains(newId));

            return newId;
        }
    }
}