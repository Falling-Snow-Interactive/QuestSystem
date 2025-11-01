using Fsi.QuestSystem.Data;
using Fsi.QuestSystem.Data.Selector;
using UnityEngine;

namespace Fsi.QuestSystem.Gameplay
{
    public class QuestGiver : MonoBehaviour
    {
        [QuestSelector]
        [SerializeField]
        private QuestData data;
        public QuestData Data => data;

        public void Grant()
        {
            
        }
    }
}
