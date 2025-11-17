using System;
using Fsi.Localization;
using Fsi.QuestSystem.Libraries;
using UnityEditor;
using UnityEngine;

namespace Fsi.QuestSystem.Settings
{
    public class QuestSystemSettings : ScriptableObject
    {
        private const string ResourcePath = "Settings/QuestSystemSettings";
        private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

        private static QuestSystemSettings settings;
        public static QuestSystemSettings Settings => settings ??= GetOrCreateSettings();

        [Header("Library")]

        [SerializeField]
        private QuestLibrary quests;
        public static QuestLibrary Quests => Settings.quests;

        [Header("Localization")]

        [SerializeField]
        private LocEntry locEnemyStep;
        
        [SerializeField]
        private LocEntry locNPCStep;
        
        [SerializeField]
        private LocEntry locItemStep;
        
        [SerializeField]
        private LocEntry locLocationStep;

        #region Validate
        
        public void Validate()
        {
            quests.Validate();
        }
        
        #endregion

        #region Settings

        private static QuestSystemSettings GetOrCreateSettings()
        {
            QuestSystemSettings set = Resources.Load<QuestSystemSettings>(ResourcePath);

            #if UNITY_EDITOR
            if (!set)
            {
                if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                {
                    AssetDatabase.CreateFolder("Assets", "Resources");
                }

                if (!AssetDatabase.IsValidFolder("Assets/Resources/Settings"))
                {
                    AssetDatabase.CreateFolder("Assets/Resources", "Settings");
                }

                set = CreateInstance<QuestSystemSettings>();
                AssetDatabase.CreateAsset(set, FullPath);
                AssetDatabase.SaveAssets();
            }
            #endif

            return set;
        }

        #if UNITY_EDITOR
        public static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }
        #endif

        #endregion
    }
}