using System.Collections.Generic;
using Fsi.DataSystem;
using Fsi.QuestSystem.Steps;
using UnityEngine;

namespace Fsi.QuestSystem.Data
{
    [CreateAssetMenu(menuName = "Fsi/Quest System/Quests/Data", fileName = "New Quest Data")]
    public class QuestData : ScriptableData<QuestID>
    {
        [Header("Steps")]

        [SerializeField]
        private List<StepData> steps = new();
        public List<StepData> Steps => steps;
    }
}
