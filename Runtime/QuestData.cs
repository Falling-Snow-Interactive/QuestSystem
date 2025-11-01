using System.Collections.Generic;
using Fsi.DataSystem;
using Fsi.QuestSystem.Objectives;
using Fsi.QuestSystem.Types;
using Fsi.QuestSystem.Types.Selector;
using UnityEngine;

namespace Fsi.QuestSystem
{
    [CreateAssetMenu(menuName = "Fsi/Quest System/Quest/Data", fileName = "New Quest Data")]
    public class QuestData : ScriptableData<string>
    {
        [Header("Quest")]

        [QuestTypeSelector]
        [SerializeField]
        private QuestType type;
        public QuestType Type => type;
        
        [SerializeField]
        private List<QuestStep> steps = new();
        public List<QuestStep> Steps => steps;
    }
}
