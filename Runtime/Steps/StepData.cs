using System;
using Fsi.Characters.Data;
using Fsi.Characters.Data.Selector;
using Fsi.Enemies;
using Fsi.Enemies.Libraries.Selectors;
using Fsi.Gameplay;
using UnityEngine;
using UnityEngine.Serialization;

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

        [SerializeField]
        private StepType type;
        
        #region Inspector Fields
        
        [ShowIf(nameof(type), StepType.NPC)]
        [Tooltip("NPC to interact with.")]
        [CharacterSelector]
        [SerializeField]
        private CharacterData npc;
        
        [FormerlySerializedAs("instanceId")]
        [ShowIf(nameof(type), StepType.NPC)]
        [Tooltip("NPC instance ID.")]
        [SerializeField]
        private int npcInstanceID;

        [ShowIf(nameof(type), StepType.Enemy)]
        [EnemyLibrary]
        [SerializeField]
        private EnemyData enemy;

        [ShowIf(nameof(type), StepType.Enemy)]
        [SerializeField]
        private int numberOfEnemy = 5;
        
        // [SerializeField]
        // private 
        
        #endregion
        
        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public StepType Type => type;
        
        /// <summary>
        /// NPC to interact with as part of this step.
        /// </summary>
        /// <remarks>
        /// Used when <see cref="StepType"/> is <see cref="Steps.StepType.NPC"/>.
        /// </remarks>
        public CharacterData Npc => npc;

        /// <summary>
        /// 
        /// </summary>
        public int NPCInstanceID => npcInstanceID;

        public EnemyData Enemy => enemy;

        public int NumberOfEnemy => numberOfEnemy;
        
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
            return type switch
                   {
                       StepType.None => "",
                       StepType.Enemy => enemy ? $"Defeat {numberOfEnemy} {enemy.ID}" : "No enemy",
                       StepType.NPC => npc ? $"Talk to {npc.ID}_{npcInstanceID}" : "No npc",
                       StepType.Location => "",
                       StepType.Item => "",
                       _ => throw new ArgumentOutOfRangeException()
                   };
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
