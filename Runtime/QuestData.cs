using System.Collections.Generic;
using Fsi.DataSystem;
using Fsi.QuestSystem.Steps;
using Fsi.Ui.Dividers;
using UnityEngine;

namespace Fsi.QuestSystem
{
    [CreateAssetMenu(menuName = Menu, fileName = "New Quest Data")]
    public class QuestData : ScriptableData<string>
    {
        // Asset Menu
        private new const string Menu = ScriptableData<string>.Menu + "Quest System/Quest";
        
        [Divider]
        
        [Header("Steps")]

        [SerializeField]
        private List<StepData> steps = new();
        public List<StepData> Steps => steps;
        
        [Divider]

        [Header("Quest Tracker Properties")]

        [SerializeField]
        private bool pinOnAccept = false;
        public bool PinOnAccept => pinOnAccept;
    }
}
