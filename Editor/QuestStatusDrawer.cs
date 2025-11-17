using UnityEditor;
using UnityEngine;

namespace Fsi.QuestSystem
{
    [CustomPropertyDrawer(typeof(QuestStatus))]
    public class QuestStatusDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Build a custom display list
            string[] display =
            {
                // "--- Initialization ---",
                "None",
                " ",
                "Available",
                "Locked",
                "Hidden",
                " ",
                // "--- Active ---",
                "Active",
                "Pending",
                "ReadyToComplete",
                " ",
                // "--- End States ---",
                "Complete",
                "Failed",
                "Abandoned",
                "Skipped",
                "Expired",
                "Replayable"
            };

            // Map visible index → enum value
            int[] values =
            {
                //-1,
                0, 
                -1,
                1, 2, 3,
                -1, // -1,
                4, 5, 6,
                -1, // -1,
                7, 8, 9, 10, 11, 12,
            };

            int currentEnumValue = property.intValue;

            // Find the display index that matches the current value
            int currentIndex = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == currentEnumValue)
                {
                    currentIndex = i;
                    break;
                }
            }

            int newIndex = EditorGUI.Popup(position, label.text, currentIndex, display);

            // Prevent selecting header rows
            if (values[newIndex] != -1)
            {
                property.intValue = values[newIndex];
            }

            EditorGUI.EndProperty();
        }
    }
}