using System;
using System.Collections.Generic;
using Fsi.QuestSystem.Steps;
using UnityEngine;

namespace Fsi.QuestSystem
{
    [Serializable]
    public class QuestInstance : ISerializationCallbackReceiver
    {
        #region Events

        public event Action Changed;
        
        #endregion
        
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
            set
            {
                if (status != value)
                {
                    status = value;
                    Changed?.Invoke();
                }
            }
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

        [SerializeReference]
        private List<StepInstance> steps;
        
        #endregion

        public QuestInstance(QuestData data)
        {
            this.data = data;
            status = QuestStatus.Active;

            steps = new List<StepInstance>();
            foreach (StepData s in data.Steps)
            {
                // TODO - Factory - Kira
                StepInstance stepInstance = new NpcStepInstance(s); // StepFactory.CreateStep(s, QuestStatus.None);
                steps.Add(stepInstance);

                stepInstance.Changed += OnStepChanged;
            }

            if (steps.Count > 0)
            {
                SetStepStatus(0, QuestStatus.Active);
                steps[0].Enable();
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

        public bool TryGetScenePosition(out Vector3 position)
        {
            if (TryGetActiveQuestStep(out StepInstance step) 
                && step is IScenePosition scenePosition)
            {
                return scenePosition.TryGetScenePosition(out position);
            }
            
            position = Vector3.zero;
            return false;
        }
        
        private void OnStepChanged()
        {
            Changed?.Invoke();
        }
        
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
