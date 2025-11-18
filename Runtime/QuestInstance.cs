using System;
using System.Collections.Generic;
using System.Linq;
using Fsi.QuestSystem.Steps;
using UnityEngine;

namespace Fsi.QuestSystem
{
    [Serializable]
    public class QuestInstance : ISerializationCallbackReceiver
    {
        #region Public Properties
        
        /// <summary>
        /// 
        /// </summary>
        public string ID => data ? data.ID : "No Quest Data";
        
        /// <summary>
        /// 
        /// </summary>
        public string Name => data ? data.Name : "No Quest Data";
        
        /// <summary>
        /// 
        /// </summary>
        public QuestData Data => data;

        /// <summary>
        /// 
        /// </summary>
        public QuestStatus Status
        {
            get => status;
            set => status = value;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public float Progress => TryGetActiveQuestStep(out int index) ? (float)index / steps.Count : 0;
        
        #endregion
        
        #region Inspector Properties
        
        [HideInInspector]
        [SerializeField]
        private string name;
        
        [SerializeField]
        private QuestData data;

        [SerializeField]
        private QuestStatus status;

        [SerializeField]
        private List<StepInstance> steps;
        
        #endregion

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

        #region Steps
        
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

        public bool TryGetActiveQuestStep(out int index, out StepInstance step)
        {
            step = null;
            for (index = 0; index < steps.Count; index++)
            {
                step = steps[index];
                if (step.Status == QuestStatus.Active)
                {
                    return true;
                }
            }

            return false;
        }
        
        public bool TryGetActiveQuestStep(out int index)
        {
            for (index = 0; index < steps.Count; index++)
            {
                StepInstance step = steps[index];
                if (step.Status == QuestStatus.Active)
                {
                    return true;
                }
            }

            return false;
        }
        
        public bool TryGetActiveQuestStep(out StepInstance step)
        {
            step = null;
            foreach (StepInstance s in steps)
            {
                step = s;
                if (step.Status == QuestStatus.Active)
                {
                    return true;
                }
            }

            return false;
        }

        public bool SetStepStatus(int index, QuestStatus status)
        {
            if (steps.Count > index)
            {
                steps[index].Status = status;
                return true;
            }

            return false;
        }
        
        #endregion
        
        #region Serialization

        public void OnBeforeSerialize()
        {
            name = ToString();
        }

        public void OnAfterDeserialize() { }
        
        #endregion
        
        #region Object Overrides

        public override string ToString() => $"{ID} - {Status}";
        
        #endregion
    }
}
