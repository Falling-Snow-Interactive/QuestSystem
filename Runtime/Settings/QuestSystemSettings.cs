using System.Collections.Generic;
using System.Linq;
using Fsi.QuestSystem.Objectives;
using Fsi.QuestSystem.Types;
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
        
        [SerializeField]
        private List<QuestType> types = new();
        public static List<QuestType> Types => Settings.types;

        [SerializeField]
        private List<ObjectiveData> objectives = new();
        public static List<ObjectiveData> Objectives => Settings.objectives;

        #region Validate
        
        public void Validate()
        {
            ValidateQuests();
            ValidateTypes();
        }

        private void ValidateQuests()
        {
            #if UNITY_EDITOR
            if (quests == null || quests.Count == 0)
            {
                return;
            }

            // Group all quests by their QuestDataId
            List<IGrouping<string, QuestData>> duplicateGroups = quests
                .Where(q => q)
                .GroupBy(q => q.Id)
                .Where(g => !string.IsNullOrEmpty(g.Key) && g.Count() > 1)
                .ToList();

            if (duplicateGroups.Count > 0)
            {
                foreach (IGrouping<string, QuestData> group in duplicateGroups)
                {
                    string names = string.Join(", ", group.Select(q => q.name));
                    Debug.LogWarning($"QuestSystemSettings | Duplicate Quest.Id detected: '{group.Key}' used by {names}",
                        this
                    );
                }
            }
            #endif
        }

        private void ValidateTypes()
        {
            #if UNITY_EDITOR
            if (types == null || types.Count == 0)
            {
                return;
            }

            // Group all quests by their QuestDataId
            List<IGrouping<QuestTypeId, QuestType>> duplicateGroups = types
                .Where(q => q)
                .GroupBy(q => q.Id)
                .Where(g => g.Count() > 1)
                .ToList();

            if (duplicateGroups.Count > 0)
            {
                foreach (IGrouping<QuestTypeId, QuestType> group in duplicateGroups)
                {
                    string names = string.Join(", ", group.Select(q => q.name));
                    Debug.LogWarning($"QuestSystemSettings | Duplicate Type.Id detected: '{group.Key}' used by {names}",
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

        #region Get By Id
        
        public static QuestData GetTypeById(string id)
        {
            foreach (QuestData t in Settings.quests)
            {
                if (t.Id == id)
                {
                    return t;
                }
            }
            
            Debug.LogError($"Quest System | No quest with ID {id} found.");
            return null;
        }

        public static QuestType GetTypeById(QuestTypeId id)
        {
            foreach (QuestType t in Settings.types)
            {
                if (t.Id == id)
                {
                    return t;
                }
            }
            
            Debug.LogError($"Quest System | No quest type with ID {id} found.");
            return null;
        }

        public static ObjectiveData GetObjectiveById(ObjectiveDataId id)
        {
            foreach (ObjectiveData o in Settings.objectives)
            {
                if (o.Id == id)
                {
                    return o;
                }
            }
            
            Debug.LogError($"Quest System | No objective with ID {id} found.");
            return null;
        }

        #endregion
    }
}