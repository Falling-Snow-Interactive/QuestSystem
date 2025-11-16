using Fsi.QuestSystem.Data;
using Fsi.QuestSystem.Data.Selector;
using UnityEngine;

namespace Fsi.QuestSystem.Gameplay
{
    public class QuestGiver : MonoBehaviour
    {
        [QuestSelector]
        [SerializeField]
        private QuestInstance data;
        public QuestInstance Data => data;

        public void Grant()
        {
            
        }
    }
}
