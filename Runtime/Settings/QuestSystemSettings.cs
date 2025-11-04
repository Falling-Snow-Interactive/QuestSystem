using System.Collections.Generic;
using System.Linq;
using Fsi.QuestSystem.Data;
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
        private List<QuestData> quests = new();
        public static List<QuestData> Quests => Settings.quests;

        #region Validate
        
        public void Validate()
        {
            ValidateQuests();
        }

        private void ValidateQuests()
        {
            #if UNITY_EDITOR
            if (quests == null || quests.Count == 0)
            {
                return;
            }
            
            quests.RemoveAll(q => q == null);

            // Group all quests by their QuestDataId
            List<IGrouping<QuestDataID, QuestData>> duplicateGroups = quests
                .Where(q => q)
                .GroupBy(q => q.ID)
                .Where(g => QuestDataID.None != g.Key && g.Count() > 1)
                .ToList();

            if (duplicateGroups.Count > 0)
            {
                foreach (IGrouping<QuestDataID, QuestData> group in duplicateGroups)
                {
                    string names = string.Join(", ", group.Select(q => q.name));
                    Debug.LogWarning($"QuestSystemSettings | Duplicate Quest.Id detected: '{group.Key}' used by {names}",
                        this
                    );
                }
            }
            #endif
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