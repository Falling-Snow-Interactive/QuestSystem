using Fsi.Ui.Spacers;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.QuestSystem.Settings
{
    public static class QuestSystemSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            SettingsProvider provider = new("Fsi/Quest System", SettingsScope.Project)
            {
                label = "Quest System",
                activateHandler = OnActivate,
            };
        
            return provider;
        }

        private static void OnActivate(string searchContext, VisualElement root)
        {
            root.style.marginTop = 5;
            root.style.marginRight = 5;
            root.style.marginLeft = 5;
            root.style.marginBottom = 5;
    
            SerializedObject settingsProp = QuestSystemSettings.GetSerializedSettings();
        
            Label title = new("Quest System Settings");
            root.Add(title);
        
            root.Add(new Spacer());
        
            root.Add(new InspectorElement(settingsProp));
            
            root.Add(new Spacer());
            
            root.Add(new Button(OnValidateButton){text = "Validate", style = { height = 20}});
        
            root.Bind(settingsProp);
        }

        private static void OnValidateButton()
        {
            QuestSystemSettings.Settings.Validate();
        }
    }
}