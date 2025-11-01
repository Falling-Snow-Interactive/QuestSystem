using System.Collections.Generic;
using UnityEngine;

namespace Fsi.QuestSystem.Gameplay
{
    /// <summary>
    /// This is the overarching controller of all things Quests for the gameplay instance.s
    /// </summary>
    public class QuestService : MonoBehaviour
    {
        [Header("Runtime")]

        [SerializeField]
        private List<Quest> quests = new();
        public List<Quest> Quests => quests;
        
    }
}
