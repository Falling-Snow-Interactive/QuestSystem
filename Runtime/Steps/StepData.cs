using System;
using Fsi.Characters.Data;
using Fsi.Characters.Data.Selector;
using Fsi.Characters.Selector;
using Fsi.Gameplay;
using Fsi.Inventory.Items;
using UnityEngine;

namespace Fsi.QuestSystem.Steps
{
    /// <summary>
    /// Serializable data describing a single quest step and its required parameters.
    /// </summary>
    [Serializable]
    public class StepData : ISerializationCallbackReceiver
    {
        [HideInInspector]
        [SerializeField]
        private string name;
        
        /// <summary>
        /// Type of step.
        /// </summary>
        [SerializeField]
        private StepType stepType = StepType.None;
        public StepType StepType => stepType;
        
        /// <summary>
        /// Enemy type to be defeated.
        /// </summary>
        [ShowIf(nameof(stepType), StepType.Enemy)]
        [EnemySelector]
        [SerializeField]
        private EnemyDataEntry enemy;
        public EnemyDataEntry Enemy => enemy;

        /// <summary>
        /// Number of enemies of set type to be defeated.
        /// </summary>
        [ShowIf(nameof(stepType), StepType.Enemy)]
        [SerializeField]
        private int numberOfEnemies = 1;
        public int NumberOfEnemies => numberOfEnemies;

        /// <summary>
        /// NPC to interact with.
        /// </summary>
        [ShowIf(nameof(stepType), Steps.StepType.NPC)]
        [NPCSelector]
        [SerializeField]
        private NPCDataEntry npc;
        public NPCDataEntry Npc => npc;
        
        /// <summary>
        /// Item to deliver.
        /// </summary>
        [ShowIf(nameof(stepType), StepType.Item)]
        [SerializeField]
        private ItemDataEntry item;
        public ItemDataEntry Item => item;

        /// <summary>
        /// NPC to deliver item to.
        /// </summary>
        [ShowIf(nameof(stepType), Steps.StepType.Item)]
        [SerializeField]
        private NPCDataEntry deliverTo;
        public NPCDataEntry DeliverTo => deliverTo;
        
        /// <summary>
        /// Number of items to be delivered to chosen NPC.
        /// </summary>
        [ShowIf(nameof(stepType), StepType.Item)]
        [SerializeField]
        private int numberOfItem = 1;
        public int NumberOfItem => numberOfItem;

        #region ToString
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string s = $"{stepType} - ";
            s += StepType switch
            {
                StepType.None => "",
                StepType.Enemy => $"{enemy} x{numberOfEnemies}",
                StepType.NPC => $"{npc}",
                // TODO - Go to location. Will probably just be attached to a trigger area that says "Hey! I'm part of X quest at Y step". - Kira
                // StepType.Location => expr,
                StepType.Item => $"{item} x{numberOfItem}",
                _ => "",
            };

            return s;
        }
        
        #endregion

        #region Serialization
        
        /// <summary>
        /// 
        /// </summary>
        public void OnBeforeSerialize()
        {
            name = ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnAfterDeserialize() { }
        
        #endregion
    }
}
