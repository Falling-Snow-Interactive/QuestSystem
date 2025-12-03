using System;
using System.Collections.Generic;
using Fsi.QuestSystem.Steps;
using Fsi.QuestSystem.Tracker;
using UnityEngine;

namespace Fsi.QuestSystem
{
    [Serializable]
    public class QuestInstance : ISerializationCallbackReceiver
    {
        #region Events

        public static event Action<QuestInstance> Updated;
        
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
                    Updated?.Invoke(this);
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public float Progress => steps.Count > 0 ? (float)Mathf.Clamp(stepIndex, 0, steps.Count) / steps.Count : 0;

        public int StepIndex => stepIndex;
        
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

        [SerializeField]
        private int stepIndex = 0;
        
        #endregion

        public QuestInstance(QuestData data)
        {
            this.data = data;
            status = QuestStatus.Active;

            steps = new List<StepInstance>();
            foreach (StepData s in data.Steps)
            {
                // TODO - Factory - Kira
                StepInstance stepInstance = StepFactory.CreateStep(s);
                steps.Add(stepInstance);
            }

            if (steps.Count > 0)
            {
                steps[0].Status = QuestStatus.Active;
                steps[0].Enable();
                steps[0].Updated += OnStepUpdated;
            }

            stepIndex = 0;
        }

        #region Steps
        
        public bool TryGetStep(int index, out StepInstance step)
        {
            if (index < steps.Count)
            {
                step = steps[index];
                return true;
            }

            step = null;
            return false;
        }

        public bool TryGetActiveQuestStep(out StepInstance step)
        {
            return TryGetStep(stepIndex, out step);
        }

        public void AdvanceStep()
        {
            QuestTracker.Instance.SetQuestStepStatus(data.ID, stepIndex, QuestStatus.Complete, 
                                                     out QuestInstance q, out StepInstance s);
            s.Disable();
            s.Updated -= OnStepUpdated;
            stepIndex++;
            if (stepIndex < steps.Count)
            {
                QuestTracker.Instance.SetQuestStepStatus(data.ID, stepIndex, QuestStatus.Active, out q, out s);
                s.Enable();
                s.Updated += OnStepUpdated;
            }
            else
            {
                // TODO - Finish quest here. Award stuff. Probably... - Kira
                // QuestTracker.Instance.SetQuestStepStatus()
            }

            Updated?.Invoke(this);
        }
        
        private void OnStepUpdated(StepInstance stepInstance)
        {
            if (stepInstance.ShouldAdvance)
            {
                AdvanceStep();
            }
            Updated?.Invoke(this);
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
