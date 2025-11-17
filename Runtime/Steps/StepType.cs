namespace Fsi.QuestSystem.Steps
{
    /// <summary>
    /// Defines the various types of actions or conditions that a quest step may require
    /// the player to complete.
    /// </summary>
    /// <remarks>
    /// Each value corresponds to a different type of objective used by <see cref="StepData"/>.
    /// Additional metadata is typically configured in <c>StepData</c> based on the step type.
    /// </remarks>
    public enum StepType
    {
        /// <summary>
        /// No step type has been assigned.
        /// </summary>
        None = 0,

        /// <summary>
        /// Requires the player to defeat a specific enemy type, possibly multiple times.
        /// </summary>
        /// <remarks>
        /// Configured using <c>Enemy</c> and <c>Amount</c> fields on <see cref="StepData"/>.
        /// </remarks>
        Enemy = 1,

        /// <summary>
        /// Requires the player to speak to or interact with a specific NPC.
        /// </summary>
        /// <remarks>
        /// Configured using the <c>Npc</c> field on <see cref="StepData"/>.
        /// </remarks>
        NPC = 2,

        /// <summary>
        /// Requires the player to reach or enter a specific location.
        /// </summary>
        /// <remarks>
        /// This is typically implemented through trigger volumes or area markers.
        /// </remarks>
        Location = 3,

        /// <summary>
        /// Requires the player to deliver or obtain a specific item, possibly in a given quantity.
        /// </summary>
        /// <remarks>
        /// Configured using <c>Item</c>, <c>DeliverTo</c>, and <c>Amount</c> fields on <see cref="StepData"/>.
        /// </remarks>
        Item = 4,
    }
}