using UnityEditor;

namespace SortingLayerRestore
{
    public class SortingLayerRestoreManager : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            ReimportSortingLayers();
        }

        static void ReimportSortingLayers()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(SortingLayerRestoreScriptableObject)}");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);

                var scriptableObject = AssetDatabase.LoadAssetAtPath(path, typeof(SortingLayerRestoreScriptableObject)) as SortingLayerRestoreScriptableObject;
                scriptableObject.RestoreSortingLayers();

                AssetDatabase.DeleteAsset(path);
            }
        }
    }
}