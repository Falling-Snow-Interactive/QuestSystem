using System;
using System.Collections.Generic;
using Fsi.QuestSystem.Data;
using Fsi.QuestSystem.Steps;
using UnityEngine;

namespace Fsi.QuestSystem
{
    [Serializable]
    public class Quest
    {
        [SerializeField]
        private QuestData data;
        public QuestData Data => data;

        [SerializeField]
        private QuestStatus questStatus;
        public QuestStatus QuestStatus => questStatus;

        [SerializeField]
        private List<Step> steps;

        public Quest(QuestData data)
        {
            this.data = data;
            questStatus = QuestStatus.Unassigned;

            steps = new List<Step>();
            foreach (StepData s in data.Steps)
            {
                Step step = StepFactory.CreateStep(s);
                steps.Add(step);
            }
        }

        public bool TryGetStep(int id, out Step step)
        {
            foreach (Step s in steps)
            {
                if (s.ID == id)
                {
                    step = s;
                    return true;
                }
            }

            step = null;
            return false;
        }
    }
}
