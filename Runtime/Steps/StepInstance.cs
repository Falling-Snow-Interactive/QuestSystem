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
    public class StepInstance
    {
        /// <summary>
        /// Invoked whenever the step's progress value changes.
        /// </summary>
        public event Action Changed;
        
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
        public QuestStatus Status => status;
        
        /// <summary>
        /// Gets the current progress toward completing this step.
        /// </summary>
        /// <remarks>
        /// For steps with amounts (Enemy, Item), this is clamped between 0 and <see cref="StepData.Amount"/>.
        /// For steps without amounts (NPC, Location), this is typically unused.
        /// </remarks>
        public int Value => value;
        
        #endregion
        
        /// <summary>
        /// Creates a new runtime step instance with the given data and starting status.
        /// </summary>
        /// <param name="data">The static step configuration data.</param>
        /// <param name="status">The initial quest status for this step.</param>
        public StepInstance(StepData data, QuestStatus status)
        {
            this.data = data;
            this.status = status;
            value = 0;
        }

        /// <summary>
        /// Increases the step's progress value and triggers a change notification.
        /// </summary>
        /// <param name="inc">Amount to increment the progress by.</param>
        /// <returns>
        /// <c>true</c> if progress increased,
        /// <c>false</c> if the step was already at maximum progress.
        /// </returns>
        /// <remarks>
        /// Value is clamped between <c>0</c> and <see cref="StepData.Amount"/>.
        /// If the value hits its maximum, further increments return <c>false</c>.
        /// </remarks>
        public bool Increment(int inc)
        {
            if (value == data.Amount)
            {
                return false;
            }
            
            value += inc;
            value = Math.Clamp(value, 0, data.Amount);

            Changed?.Invoke();
            return true;
        }
        
        /// <summary>
        /// Returns a human-readable description of the step's state and objective.
        /// </summary>
        /// <remarks>
        /// TODO: Localize these strings. - Kira
        /// </remarks>
        /// <returns>A string describing the current progress of this step.</returns>
        public string GetDescription()
        {
            return data.StepType switch
            {
                StepType.None => "None",
                StepType.Enemy => $"Defeat {value}/{data.Amount} {data.Enemy.Name}",
                StepType.NPC => $"Talk to {data.Npc.Name}",
                StepType.Location => $"Go to location.name",
                StepType.Item => $"Bring {value}/{data.Amount} {data.Item.Name} to {data.Npc}",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}