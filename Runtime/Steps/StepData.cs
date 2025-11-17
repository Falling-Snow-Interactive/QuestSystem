using System;
using Fsi.Characters.Data;
using Fsi.Characters.Data.Selector;
using Fsi.Characters.Selector;
using Fsi.Gameplay;
using Fsi.Inventory.Items;
using Fsi.Inventory.Items.Selector;
using UnityEngine;

namespace Fsi.QuestSystem.Steps
{
    /// <summary>
    /// Serializable configuration for a single quest step, including its type and any parameters
    /// required to evaluate or describe the step at runtime.
    /// </summary>
    /// <remarks>
    /// This data is intended to be authored in the inspector and consumed by quest logic at runtime.
    /// It also implements <see cref="ISerializationCallbackReceiver"/> to keep the inspector name
    /// in sync with the current step configuration.
    /// </remarks>
    [Serializable]
    public class StepData : ISerializationCallbackReceiver
    {
        /// <summary>
        /// Backing label used to show a readable name for this step in the inspector.
        /// Populated via <see cref="OnBeforeSerialize"/>.
        /// </summary>
        [HideInInspector]
        [SerializeField]
        private string name;
        
        #region Inspector Fields
        
        [Tooltip("Type of quest step this entry represents.")]
        [SerializeField]
        private StepType stepType = StepType.None;
        
        [ShowIf(nameof(stepType), StepType.Enemy)]
        [Tooltip("Enemy type to be defeated.")]
        [EnemySelector]
        [SerializeField]
        private EnemyData enemy;
        
        [ShowIf(nameof(stepType), Steps.StepType.NPC)]
        [Tooltip("NPC to interact with.")]
        [NPCSelector]
        [SerializeField]
        private NPCData npc;
        
        [ShowIf(nameof(stepType), StepType.Item)]
        [Tooltip("Item to deliver.")]
        [ItemSelector]
        [SerializeField]
        private ItemData item;
        
        [ShowIf(nameof(stepType), Steps.StepType.Item)]
        [Tooltip("NPC to deliver item to.")]
        [NPCSelector]
        [SerializeField]
        private NPCData deliverTo;
        
        [Tooltip("Depending on Step Type:\n" +
                 "- Enemy: Number of enemies to defeat\n" +
                 " -Item: Number of items to be delivered to the chosen NPC")]
        [ShowIf(nameof(stepType), new []{StepType.Item, StepType.Enemy})]
        [SerializeField]
        private int amount = 1;
        
        #endregion
        
        #region Public Properties
        
        /// <summary>
        /// Type of quest step this entry represents.
        /// </summary>
        public StepType StepType => stepType;
        
        /// <summary>
        /// Enemy type that must be defeated to complete this step.
        /// </summary>
        /// <remarks>
        /// Used when <see cref="StepType"/> is <see cref="Steps.StepType.Enemy"/>.
        /// </remarks>
        public EnemyData Enemy => enemy;
        
        /// <summary>
        /// NPC to interact with as part of this step.
        /// </summary>
        /// <remarks>
        /// Used when <see cref="StepType"/> is <see cref="Steps.StepType.NPC"/>.
        /// </remarks>
        public NPCData Npc => npc;
        
        /// <summary>
        /// Item that must be delivered as part of this step.
        /// </summary>
        /// <remarks>
        /// Used when <see cref="StepType"/> is <see cref="Steps.StepType.Item"/>.
        /// </remarks>
        public ItemData Item => item;
        
        /// <summary>
        /// NPC that the item must be delivered to.
        /// </summary>
        /// <remarks>
        /// Used when <see cref="StepType"/> is <see cref="Steps.StepType.Item"/>.
        /// </remarks>
        public NPCData DeliverTo => deliverTo;
        
        /// <summary>
        /// Amount associated with this step.
        /// </summary>
        /// <remarks>
        /// <para>For <see cref="Steps.StepType.Enemy"/>: Number of enemies to defeat.</para>
        /// <para>For <see cref="Steps.StepType.Item"/>: Number of items to deliver to the target NPC.</para>
        /// <para>For other step types, this value is currently unused.</para>
        /// </remarks>
        public int Amount => amount;
        
        #endregion

        #region ToString
        
        /// <summary>
        /// Returns a human-readable description of the step, combining the step type and its key parameters.
        /// </summary>
        /// <returns>
        /// A formatted string describing this step, suitable for debugging and inspector display.
        /// </returns>
        public override string ToString()
        {
            string s = $"{stepType} - ";
            s += StepType switch
            {
                StepType.None => "",
                StepType.Enemy => $"{enemy} x{Amount}",
                StepType.NPC => $"{npc}",
                // TODO - Go to location. Will probably just be attached to a trigger area that says "Hey! I'm part of X quest at Y step". - Kira
                // StepType.Location => expr,
                StepType.Item => $"{item} x{Amount}",
                _ => "",
            };

            return s;
        }
        
        #endregion

        #region Serialization
        
        /// <summary>
        /// Called before Unity serializes this object.
        /// </summary>
        /// <remarks>
        /// Updates the hidden <see cref="name"/> field so that instances are labelled more clearly
        /// in the inspector, based on the current step configuration.
        /// </remarks>
        public void OnBeforeSerialize()
        {
            name = ToString();
        }

        /// <inheritdoc />
        public void OnAfterDeserialize() { }
        
        #endregion
    }
}
