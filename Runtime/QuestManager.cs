using System;
using System.Collections.Generic;
using Fsi.QuestSystem.Data;
using Fsi.QuestSystem.Settings;
using Fsi.QuestSystem.Steps;
using UnityEngine;

namespace Fsi.QuestSystem
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField]
        private List<Quest> quests = new();
        public List<Quest> Quests => quests;

        private void Start()
        {
            foreach (QuestData q in QuestSystemSettings.Quests)
            {
                Quest quest = new(q);
                quests.Add(quest);
            }
        }
        
        public bool TryGetQuest(QuestID id, out Quest quest)
        {
            foreach (Quest q in quests)
            {
                if (q.Data.ID == id)
                {
                    quest = q;
                    return true;
                }
            }

            quest = null;
            return false;
        }

        public bool CheckQuestStatus(QuestID questID, QuestStatus status)
        {
            if (TryGetQuest(questID, out Quest quest))
            {
                return quest.QuestStatus == status;
            }

            return false;
        }

        public bool CheckQuestStepStatus(QuestID questID, int step, QuestStatus status)
        {
            if (TryGetQuest(questID, out Quest quest) && quest.TryGetStep(step, out Step s))
            {
                return s.QuestStatus == status;
            }

            return false;
        }
    }
}
