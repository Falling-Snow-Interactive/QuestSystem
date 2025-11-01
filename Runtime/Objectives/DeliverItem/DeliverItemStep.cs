using UnityEngine;

namespace Fsi.QuestSystem.Objectives.DeliverItem
{
    [CreateAssetMenu(menuName = "Fsi/Quest System/Quest/Step/Deliver Item", fileName = "New Objective Data")]
    public class DeliverItemStep : QuestStep
    {
        public override ObjectiveDataId ObjectiveId => ObjectiveDataId.DeliverItem;
        
        // TODO - Make this in the inventory package - Kira
        // [ItemSelector]
        // [SerializeField]
        // private ItemData item;
        // public ItemData Item => item;

        [Min(1)]
        [SerializeField]
        private int amount = 1;
        public int Amount => amount;

        [SerializeField]
        private string npcTag;
        public string NpcTag => npcTag;
    }
}