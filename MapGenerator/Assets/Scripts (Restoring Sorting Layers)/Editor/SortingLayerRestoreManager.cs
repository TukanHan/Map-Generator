#if UNITY_EDITOR

using UnityEditor;

namespace SortingLayerRestore
{
    [InitializeOnLoad]
    public static class SortingLayerRestoreManager
    {
        static SortingLayerRestoreManager()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(SortingLayerRestoreScriptableObject)}");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
        
                var scriptableObject = AssetDatabase.LoadAssetAtPath(path, typeof(SortingLayerRestoreScriptableObject)) as SortingLayerRestoreScriptableObject;
                scriptableObject.DoYourDiuty();

                AssetDatabase.DeleteAsset(path);
            }
        }
    }
}

#endif