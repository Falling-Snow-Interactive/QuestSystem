using UnityEngine;

namespace Fsi.QuestSystem
{
    /// <summary>
    /// Represents the current progress or resolution state of a quest.
    /// </summary>
    public enum QuestStatus
    {
        // Initialization States
        
        /// <summary>
        /// No status has been assigned. This usually indicates an uninitialized quest.
        /// </summary>
        None = 0,
        
        /// <summary>
        /// The quest exists and can be accepted, but has not been started.
        /// </summary>
        Available = 1,
        
        /// <summary>
        /// The quest is currently unavailable because prerequisites are not met.
        /// </summary>
        Locked = 2,
        
        /// <summary>
        /// The quest exists but is hidden from the player until specific conditions are met.
        /// </summary>
        Hidden = 3,
        
        // ----------
        
        // Active States
        
        /// <summary>
        /// The quest is currently active and in progress.
        /// </summary>
        Active = 4,
        
        /// <summary>
        /// The quest is waiting on an external or delayed action before progressing.
        /// </summary>
        Pending = 5,
        
        /// <summary>
        /// All objectives are complete; the player must turn in the quest.
        /// </summary>
        ReadyToComplete = 6,
        
        // ----------
        
        // End States
        
        /// <summary>
        /// The quest has been successfully completed.
        /// </summary>
        Complete = 7,
        
        /// <summary>
        /// The quest has failed and can no longer be completed.
        /// </summary>
        Failed = 8,
        
        /// <summary>
        /// The quest was abandoned by the player before completion.
        /// </summary>
        Abandoned = 9,
        
        /// <summary>
        /// The quest was intentionally bypassed, usually by game logic or a branching narrative.
        /// </summary>
        Skipped = 10,
        
        /// <summary>
        /// The quest can no longer be completed because its time limit has passed.
        /// </summary>
        Expired = 11,
        
        /// <summary>
        /// The quest can be repeated after completion.
        /// </summary>
        Replayable = 12,
    }
}
