using System.Collections.Generic;
using Fsi.DataSystem;
using Fsi.QuestSystem.Steps;
using Fsi.Ui.Spacers;
using UnityEngine;

namespace Fsi.QuestSystem
{
    [CreateAssetMenu(menuName = "Fsi/Quest System/Quests/Data", fileName = "New Quest Data")]
    public class QuestData : ScriptableData<string>
    {
        [Spacer]
        
        [Header("Steps")]

        [SerializeField]
        private List<StepData> steps = new();
        public List<StepData> Steps => steps;
        
        [Spacer()]

        [Header("Quest Tracker Properties")]

        [SerializeField]
        private bool pinOnAccept = false;
        public bool PinOnAccept => pinOnAccept;
    }
}
