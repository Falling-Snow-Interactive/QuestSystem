using System.Collections.Generic;
using Fsi.QuestSystem.Steps;
using UnityEngine;

namespace Fsi.QuestSystem
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField]
        private List<QuestInstance> quests = new();
        public List<QuestInstance> Quests => quests;
        
        public bool TryGetQuest(string id, out QuestInstance questInstance)
        {
            foreach (QuestInstance q in quests)
            {
                if (q.Data.ID == id)
                {
                    questInstance = q;
                    return true;
                }
            }

            questInstance = null;
            return false;
        }

        public bool CheckQuestStatus(string questID, QuestStatus status)
        {
            if (TryGetQuest(questID, out QuestInstance quest))
            {
                return quest.Status == status;
            }

            return false;
        }

        public bool CheckQuestStepStatus(string questID, int step, QuestStatus status)
        {
            if (TryGetQuest(questID, out QuestInstance quest) && quest.TryGetStep(step, out StepInstance s))
            {
                return s.Status == status;
            }

            return false;
        }
    }
}
