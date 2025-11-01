using Fsi.QuestSystem.Settings;
using UnityEngine;

namespace Fsi.QuestSystem.Objectives
{
    // [CreateAssetMenu(menuName = "Fsi/Quest System/Quest/Step/Type", fileName = "New Objective Data")]
    public abstract class QuestStep : ScriptableObject
    {
        public abstract ObjectiveDataId ObjectiveId { get; }

        private ObjectiveData data = null;
        public ObjectiveData Data
        {
            get
            {
                data ??= QuestSystemSettings.GetObjectiveById(ObjectiveId);
                return data;
            }
        }
    }
}