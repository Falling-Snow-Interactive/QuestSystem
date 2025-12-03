using System;
using UnityEngine;

namespace Fsi.QuestSystem.Steps
{
    /// <summary>
    /// Runtime instance of a single quest step, storing its current status
    /// and progress toward completion.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is created from a <see cref="StepData"/> definition when a quest begins.
    /// It tracks mutable state such as <see cref="Value"/> and <see cref="Status"/>,
    /// which are updated as the player performs actions related to the step.
    /// </para>
    /// <para>
    /// Unlike <see cref="StepData"/>, which is static authoring data,
    /// <see cref="StepInstance"/> represents the player's actual progression.
    /// </para>
    /// </remarks>
    [Serializable]
    public abstract class StepInstance
    {
        /// <summary>
        /// Invoked whenever the step's progress value changes.
        /// </summary>
        public event Action<StepInstance> Updated;
        
        #region Inspector Fields
        
        [Tooltip("The static step definition this instance is based on.")]
        [SerializeField]
        private StepData data;

        [Tooltip("The static step definition this instance is based on.")]
        [SerializeField]
        private QuestStatus status;

        [Tooltip("The static step definition this instance is based on.")]
        [SerializeField]
        private int value;
        
        #endregion
        
        #region Public Properties 
        
        /// <summary>
        /// Gets the static configuration data that defines this quest step.
        /// </summary>
        public StepData Data => data;

        /// <summary>
        /// Gets the current status of this step (e.g., Active, Completed, Failed).
        /// </summary>
        public QuestStatus Status
        {
            get => status;
            set
            {
                status = value;
                Updated?.Invoke(this);
            }
        }

        public bool ShouldAdvance { get; protected set; }
        
        /// <summary>
        /// Gets the current progress toward completing this step.
        /// </summary>
        /// <remarks>
        /// For steps with amounts (Enemy, Item), this is clamped between 0 and <see cref="StepData.Amount"/>.
        /// For steps without amounts (NPC, Location), this is typically unused.
        /// </remarks>
        [Obsolete]
        public int Value => value;
        
        #endregion
        
        /// <summary>
        /// Creates a new runtime step instance with the given data and starting status.
        /// </summary>
        /// <param name="data">The static step configuration data.</param>
        protected StepInstance(StepData data)
        {
            this.data = data;
            status = QuestStatus.None;
            value = 0;
        }
        
        public virtual void Enable() { }

        public virtual void Disable() { }

        /// <summary>
        /// Returns a human-readable description of the step's state and objective.
        /// </summary>
        /// <remarks>
        /// TODO: Localize these strings. - Kira
        /// </remarks>
        /// <returns>A string describing the current progress of this step.</returns>
        public abstract string GetDescription();

        protected void HasChanged()
        {
            Updated?.Invoke(this);
        }
    }
}