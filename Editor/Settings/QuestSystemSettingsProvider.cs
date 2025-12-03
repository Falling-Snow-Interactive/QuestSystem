using Fsi.Settings;
using UnityEditor;
using UnityEngine.UIElements;

namespace Fsi.QuestSystem.Settings
{
    public static class QuestSystemSettingsProvider
    {
        private const string Name = "Quest System";
        
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            SettingsProvider provider = new($"Falling Snow Interactive/{Name}", SettingsScope.Project)
            {
                label = Name,
                activateHandler = OnActivate,
            };
        
            return provider;
        }

        private static void OnActivate(string searchContext, VisualElement root)
        {
            SerializedObject prop = QuestSystemSettings.GetSerializedSettings();
            
            VisualElement settings = SettingsEditorUtility.CreateSettingsPage(prop, Name);
            settings.Add(new Button(OnValidateButton){text = "Validate", style = { height = 20}});
            root.Add(settings);
        }
        
        private static void OnValidateButton()
        {
            QuestSystemSettings.Settings.Validate();
        }
    }
}