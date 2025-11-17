using System.Collections.Generic;
using Fsi.DataSystem;
using Fsi.QuestSystem.Steps;
using UnityEngine;

namespace Fsi.QuestSystem
{
    [CreateAssetMenu(menuName = "Fsi/Quest System/Quests/Data", fileName = "New Quest Data")]
    public class QuestData : ScriptableData<string>
    {
        [Header("Steps")]

        [SerializeField]
        private List<StepData> steps = new();
        public List<StepData> Steps => steps;

        [Header("Tracker Properties")]

        [SerializeField]
        private bool pinOnAccept = false;
        public bool PinOnAccept => pinOnAccept;
    }
}
