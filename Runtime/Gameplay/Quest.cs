using System;
using Fsi.QuestSystem.Data;
using UnityEngine;

namespace Fsi.QuestSystem.Gameplay
{
    [Serializable]
    public class Quest
    {
        [SerializeField]
        private QuestData data;
        
        // TODO - Add the quest steps. Could be a queue. Also save the completed steps. optional: hidden steps.

        public Quest(QuestData data)
        {
            
        }
    }
}