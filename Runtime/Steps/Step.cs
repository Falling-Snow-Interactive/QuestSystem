using System;
using Fsi.DataSystem;
using UnityEngine;

namespace Fsi.QuestSystem.Steps
{
    [Serializable]
    public class Step : SerializableData<int>
    {
        [SerializeField]
        private StepData data;

        [SerializeField]
        private QuestStatus questStatus;
        public QuestStatus QuestStatus => questStatus;
        
        public Step(StepData data)
        {
            this.data = data;
        }
    }
}