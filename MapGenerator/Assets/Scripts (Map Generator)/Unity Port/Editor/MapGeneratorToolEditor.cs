using UnityEngine;
using UnityEditor;

namespace MapGenerator.UnityPort
{
    [CustomEditor(typeof(MapGeneratorTool))]
    public class MapGeneratorToolEditor : Editor
    {
        private SerializedProperty heightNoiseMapParameters;
        private SerializedProperty temperatureNoiseMapParameters;
        private SerializedProperty waterNoiseMapParameters;
        private SerializedProperty waterLayersProp;

        private MapGeneratorTool mapGenerator;

        void OnEnable()
        {
            waterNoiseMapParameters = serializedObject.FindProperty("waterNoiseMapParameters");
            heightNoiseMapParameters = serializedObject.FindProperty("heightNoiseMapParameters");
            temperatureNoiseMapParameters = serializedObject.FindProperty("temperatureNoiseMapParameters");
            waterLayersProp = serializedObject.FindProperty("waterLayers").FindPropertyRelative("waterBiomes");

            mapGenerator = (MapGeneratorTool)target;
        }

        public override void OnInspectorGUI()
        {
            DrawSizeSection();
            EditorGUILayout.Space();

            DrawNoiseMapsParametersSection();
            EditorGUILayout.Space();

            DrawWaterBiomesSection(mapGenerator.waterLayers);
            EditorGUILayout.Space();

            DrawBiomesDiagramSection(mapGenerator.biomesDiagram);
            EditorGUILayout.Space();

            DrawGenerationSection();
            EditorGUILayout.Space();

            DrawButtonSection();
        }

        private void DrawSizeSection()
        {
            EditorGUILayout.LabelField("Size", EditorStyles.boldLabel);
            mapGenerator.width = EditorGUILayout.IntSlider(new GUIContent("Width", "Count of tiles in the X axis."),
                                                           mapGenerator.width, 10, 300);
            mapGenerator.height = EditorGUILayout.IntSlider(new GUIContent("Height", "Count of tiles in the Y axis."),
                                                            mapGenerator.height, 10, 300);         
        }

        private void DrawNoiseMapsParametersSection()
        {
            EditorGUILayout.LabelField("Noise Maps Parameters", EditorStyles.boldLabel);
            serializedObject.Update();
            EditorGUILayout.PropertyField(heightNoiseMapParameters, new GUIContent("Height noise map parameters"), true);
            EditorGUILayout.PropertyField(temperatureNoiseMapParameters, new GUIContent("Temperature noise map parameters"), true);
            EditorGUILayout.PropertyField(waterNoiseMapParameters, new GUIContent("Water noise map parameters"), true);
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawWaterBiomesSection(WaterLayers waterLayers)
        {
            EditorGUILayout.LabelField("Water Biomes", EditorStyles.boldLabel);

            serializedObject.Update();
            for (int i=0; i< waterLayers.waterBiomes.Count; ++i)
            {
                EditorGUILayout.PropertyField(waterLayersProp.GetArrayElementAtIndex(i), new GUIContent($"Water level {i}"), true);
            }

            if (GUILayout.Button("Add water level", GUILayout.Width(150)))
                waterLayers.AddWaterLevel();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawBiomesDiagramSection(BiomesDiagram biomesDiagram)
        {
            EditorGUILayout.LabelField("Biomes Diagram", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            biomesDiagram.heightLayerCount = EditorGUILayout.IntField(
                new GUIContent("Height layer count", "Count of height layers in the biomes diagram."),
                biomesDiagram.heightLayerCount, GUILayout.MinWidth(10));
            biomesDiagram.temperatureLayerCount = EditorGUILayout.IntField(
                new GUIContent("Temp. layer count", "Count of temperature layers in the biomes diagram."),
                biomesDiagram.temperatureLayerCount, GUILayout.MinWidth(10));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            for (int j = -1; j < biomesDiagram.temperatureLayerCount; j++)
            {
                EditorGUILayout.BeginVertical();
                for (int i = 0; i <= biomesDiagram.heightLayerCount; i++)
                {
                    if (j == -1 && i == biomesDiagram.heightLayerCount)
                        GUILayout.Label("");
                    else if (j == -1)
                        GUILayout.Label($"Height {biomesDiagram.heightLayerCount - i}");
                    else if (i == biomesDiagram.heightLayerCount)
                        GUILayout.Label($"Temperature {j + 1}");
                    else
                        biomesDiagram[i,j] = (Biom)EditorGUILayout.ObjectField(biomesDiagram[i, j], typeof(Biom), true);
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawGenerationSection()
        {
            EditorGUILayout.LabelField(new GUIContent("Generation"), EditorStyles.boldLabel);

            mapGenerator.generationType = (GraphicalGenerationType)EditorGUILayout.EnumPopup(
                new GUIContent("Generation type", "The way a graphic representation of the map will be generated."),
                mapGenerator.generationType);

            mapGenerator.orientationType = (SpaceOrientationType)EditorGUILayout.EnumPopup(
                new GUIContent("Space orientation", "The space orientation in which the map is generated."),
                mapGenerator.orientationType);

            mapGenerator.generateOnStart = EditorGUILayout.Toggle(
                new GUIContent("Generate on start", "Is the map to be generated when the scene starts?"),
                mapGenerator.generateOnStart);

            mapGenerator.generateRandomSeed = EditorGUILayout.Toggle(
                new GUIContent("Generate random seed", "Is the map to be generated based on random seed?"),
                mapGenerator.generateRandomSeed);

            if (!mapGenerator.generateRandomSeed)
            {
                EditorGUILayout.BeginHorizontal();

                mapGenerator.seed = EditorGUILayout.IntField(
                    new GUIContent("Seed", "Random factor based on which the map is generated."),
                    mapGenerator.seed);

                if (GUILayout.Button("Random Seed"))
                    mapGenerator.RandomSeed();

                EditorGUILayout.EndHorizontal();
            }
        }

        private void DrawButtonSection()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Generate"))
            {
                if (mapGenerator.generateRandomSeed)
                    mapGenerator.RandomSeed();
            
                mapGenerator.TryGenerate();
            }

            if (GUILayout.Button("Clear"))
            {
                mapGenerator.Clear();
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}