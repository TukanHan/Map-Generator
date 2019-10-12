using UnityEditor;
using UnityEngine;

namespace MapGenerator.UnityPort
{
    [CustomPropertyDrawer(typeof(GroundNoiseMapParameters))]
    public class GroundNoiseMapParametersDrawer : PropertyDrawer
    {
        const int rowCount = 4;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int line = property.isExpanded ? (1 + rowCount) : 1;
            return EditorGUIUtility.singleLineHeight * line + ((line - 1) * 2);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            Rect foldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            if (property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label, true))
            {
                EditorGUI.LabelField(position, label);
                EditorGUI.indentLevel++;

                var rowsPositions = CalculateRowPositions(position);

                EditorGUI.PropertyField(rowsPositions[0], property.FindPropertyRelative("octaves"),
                    new GUIContent("Octaves", "The number of octaves affects the detail of the noise map."));
                EditorGUI.PropertyField(rowsPositions[1], property.FindPropertyRelative("frequency"),
                    new GUIContent("Frequency", "The frequency value affects the size of the generated spaces."));
                EditorGUI.PropertyField(rowsPositions[2], property.FindPropertyRelative("targetValue"),
                    new GUIContent("Target value", "Expected value from the spectrum of values."));

                float min = property.FindPropertyRelative("minValue").floatValue;
                float max = property.FindPropertyRelative("maxValue").floatValue;
                EditorGUI.MinMaxSlider(rowsPositions[3], new GUIContent("Value range",
                    "The noise map value will be in the given range."), ref min, ref max, 0, 1);
                property.FindPropertyRelative("minValue").floatValue = min;
                property.FindPropertyRelative("maxValue").floatValue = max;

                EditorGUI.indentLevel--;
                EditorGUI.EndProperty();
            }
        }

        private Rect[] CalculateRowPositions(Rect position)
        {
            var rowsPositions = new Rect[rowCount];
            for (int i = 0; i < rowCount; ++i)
            {
                rowsPositions[i] = new Rect(position.x, position.y + (i + 1) * 18, position.width, 16);
            }
            return rowsPositions;
        }
    }
}
