using System;
using Fsi.Characters.Data;
using Fsi.Characters.Data.Selector;
using Fsi.Characters.Selector;
using Fsi.Gameplay;
using Fsi.Inventory.Items;
using UnityEngine;

namespace Fsi.QuestSystem.Steps
{
    [Serializable]
    public class StepData : ISerializationCallbackReceiver
    {
        [HideInInspector]
        [SerializeField]
        private string name;
        
        [SerializeField]
        private StepType stepType = StepType.None;
        public StepType StepType => stepType;
        
        // Enemy
        [ShowIf(nameof(stepType), StepType.Enemy)]
        [EnemySelector]
        [SerializeField]
        private EnemyData enemy;
        public EnemyData Enemy => enemy;

        [ShowIf(nameof(stepType), StepType.Enemy)]
        [SerializeField]
        private int numberOfEnemies = 1;
        public int NumberOfEnemies => numberOfEnemies;

        // NPC
        [ShowIf(nameof(stepType), Steps.StepType.NPC)]
        [NPCSelector]
        [SerializeField]
        private NPCData npc;
        public NPCData Npc => npc;
        
        // Item
        [ShowIf(nameof(stepType), StepType.Item)]
        [SerializeField]
        private ItemData item;
        public ItemData Item => item;

        [ShowIf(nameof(stepType), Steps.StepType.Item)]
        [SerializeField]
        private NPCData deliverTo;
        public NPCData DeliverTo => deliverTo;
        
        // Shared
        [ShowIf(nameof(stepType), StepType.Item)]
        [SerializeField]
        private int numberOfItem = 1;
        public int NumberOfItem => numberOfItem;
        
        // TODO - This
        // [Header("Location Step")]
        //
        // [Header("Item Step")]
        //
        // [SerializeField]
        // private int a = 0;

        public override string ToString()
        {
            string s = $"{stepType} - ";
            s += StepType switch
            {
                StepType.None => "",
                StepType.Enemy => $"{enemy} x{numberOfEnemies}",
                StepType.NPC => $"{npc}",
                // StepType.Location => expr,
                StepType.Item => $"{item} x{numberOfItem}",
                _ => "",
            };

            return s;
        }

        public void OnBeforeSerialize()
        {
            name = ToString();
        }

        public void OnAfterDeserialize() { }
    }
}
