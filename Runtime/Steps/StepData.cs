using System;
using Fsi.Characters.Data;
using Fsi.Characters.Data.Selector;
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
        
        [Tooltip("NPC to interact with.")]
        [CharacterSelector]
        [SerializeField]
        private CharacterData npc;
        
        [Tooltip("NPC instance ID.")]
        [SerializeField]
        private int instanceId;
        
        #endregion
        
        #region Public Properties
        
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
        public int InstanceID => instanceId;
        
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
            return npc ? $"Talk to {npc.ID}_{instanceId}" : "No npc";
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
