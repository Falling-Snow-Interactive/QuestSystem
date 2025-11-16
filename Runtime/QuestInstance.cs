using System;
using System.Collections.Generic;
using Fsi.QuestSystem.Steps;
using UnityEngine;

namespace Fsi.QuestSystem
{
    [Serializable]
    public class QuestInstance : ISerializationCallbackReceiver
    {
        [HideInInspector]
        [SerializeField]
        private string name;
        
        public string ID => data ? data.ID : "No Quest Data";
        public string Name => data ? data.Name : "No Quest Data";
        
        [SerializeField]
        private QuestData data;
        public QuestData Data => data;

        [SerializeField]
        private QuestStatus status;
        public QuestStatus Status => status;

        [SerializeField]
        private List<StepInstance> steps;

        public QuestInstance(QuestData data)
        {
            this.data = data;
            status = QuestStatus.Active;

            steps = new List<StepInstance>();
            foreach (StepData s in data.Steps)
            {
                StepInstance stepInstance = StepFactory.CreateStep(s, steps.Count == 0
                                                                          ? QuestStatus.Active
                                                                          : QuestStatus.None);
                steps.Add(stepInstance);
            }
        }

        public bool TryGetStep(int index, out StepInstance stepInstance)
        {
            if (steps.Count < index)
            {
                stepInstance = steps[index];
                return true;
            }

            stepInstance = null;
            return false;
        }

        public override string ToString() => $"{ID} - {Status}";

        public void OnBeforeSerialize()
        {
            name = ToString();
        }

        public void OnAfterDeserialize() { }
    }
}
