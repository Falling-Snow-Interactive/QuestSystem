// Copyright Falling Snow Interactive 2025
using System;
using System.Collections.Generic;
using System.Linq;
using Fsi.General;
using Fsi.QuestSystem.Data.Selector;
using Fsi.QuestSystem.Settings;
using Fsi.QuestSystem.Steps;
using UnityEngine;

namespace Fsi.QuestSystem.Tracker
{
    /// <summary>
    /// Base tracker responsible for managing all active quest instances, including adding, removing, and pinning
    /// quests. Emits events so that UI, systems, and gameplay logic can react to quest state changes.
    /// </summary>
    public abstract class QuestTracker : MbSingleton<QuestTracker>
    {
        #region Events
        
        /// <summary>
        /// Fired when the tracker completes initialization in <see cref="Awake"/>.
        /// Provides the initialized tracker instance.
        /// </summary>
        public static event Action<QuestTracker> Initialized;
        
        /// <summary>
        /// Fired whenever anything in the Quest Tracker changed. (Ex: tracker initialized, quest added or pinned).
        /// </summary>
        public static event Action<QuestTracker> Changed;

        /// <summary>
        /// Fired when a quest instance is successfully added to the tracker.
        /// Provides the added <see cref="QuestInstance"/>.
        /// </summary>
        public static event Action<QuestInstance> QuestAdded;
            
        /// <summary>
        /// Fired when a quest instance is removed from the tracker.
        /// Provides the removed <see cref="QuestInstance"/>.
        /// </summary>
        public static event Action<QuestInstance> QuestRemoved;
        
        /// <summary>
        /// Fired when a quest becomes the currently pinned quest.
        /// </summary>
        public static event Action<QuestInstance> QuestPinned;
        
        /// <summary>
        /// Fired when the currently pinned quest is unpinned.
        /// </summary>
        public static event Action<QuestInstance> QuestUnpinned;
        
        #endregion
        
        #region Inspector Fields
        
        [Header("Quests")]
        
        [Tooltip("The quest currently pinned for quick reference (e.g., HUD tracking).")]
        [SerializeField]
        private string pinnedID;

        [Tooltip("The full list of active quests currently tracked by the system.")]
        [SerializeField]
        private List<QuestInstance> quests;
        
        #endregion
        
        #if UNITY_EDITOR

        [Header("Debug")]

        [QuestSelector]
        [SerializeField]
        private List<QuestData> addOnStart;
        
        #endif
        
        #region Quests Properties

        /// <summary>
        /// The quest currently pinned for quick reference (e.g., HUD tracking).
        /// </summary>
        public QuestInstance PinnedQuest => TryGetQuest(pinnedID, out QuestInstance quest) ? quest : null;
        
        /// <summary>
        /// The full list of active quests currently tracked by the system.
        /// </summary>
        public IReadOnlyList<QuestInstance> Quests => quests;
        
        #endregion
        
        #region MonoBehaviour Events

        /// <summary>
        /// Initializes the singleton tracker and notifies listeners.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            quests ??= new List<QuestInstance>();
            
            Initialized?.Invoke(this);
            Changed?.Invoke(this);
        }

        /// <summary>
        /// Start the quest tracker. First check if something is pinned.
        /// </summary>
        private void Start()
        {
            #if UNITY_EDITOR

            foreach (QuestData q in addOnStart)
            {
                Add(q);
            }
            
            #endif
            
            if (TryGetQuest(pinnedID, out QuestInstance quest))
            {
                PinQuest(quest);
            }
        }
        
        #endregion

        #region Quest Management
        
        #region Add
        
        /// <summary>
        /// Attempts to add a quest instance to the tracker.
        /// </summary>
        /// <param name="quest">The quest instance to add.</param>
        /// <returns>
        /// True if the quest was successfully added;
        /// false if a quest with the same ID is already tracked.
        /// </returns>
        public bool Add(QuestInstance quest)
        {
            if (!TryGetQuest(quest.ID, out QuestInstance _))
            {
                Debug.Log($"Quest Tracker | Adding quest ({quest.ID}).");
                quests.Add(quest);
                OnAdd(quest);
                QuestAdded?.Invoke(quest);

                if (quest.Data.PinOnAccept)
                {
                    PinQuest(quest);
                }

                Changed?.Invoke(this);
                return true;
            }
            
            return false;
        }
        
        /// <summary>
        /// Instantiates and adds a quest using the given quest data.
        /// Returns true if added; false if a quest with the same ID already exists.
        /// </summary>
        /// <param name="data">The quest data used to create the quest instance.</param>
        /// <returns>
        /// The quest instance added to the quest list.
        /// </returns>
        public QuestInstance Add(QuestData data)
        {
            QuestInstance quest = new(data);
            Add(quest);
            return quest;
        }

        public QuestInstance Add(string questID)
        {
            return QuestSystemSettings.Quests.TryGetEntry(questID, out QuestData data) 
                       ? Add(data) 
                       : null;
        }

        public bool TryAdd(QuestData data, out QuestInstance quest)
        {
            if (TryGetQuest(data.ID, out quest))
            {
                Debug.LogWarning($"Quest ({quest.ID}) has already been added.");
                return false;
            }

            quest = Add(data);
            return true;
        }

        /// <summary>
        /// Attempts to add a quest by ID using the global <see cref="QuestSystemSettings"/>.
        /// </summary>
        /// <param name="id">The ID of the quest to add.</param>
        /// <param name="quest"></param>
        /// <returns>
        /// True if the quest was found and added;
        /// false if a quest with the same ID exists or if the ID does not match any quest.
        /// </returns>
        public bool TryAdd(string id, out QuestInstance quest)
        {
            if (!TryGetQuest(id, out quest) && QuestSystemSettings.Quests.TryGetEntry(id, out QuestData data))
            {
                return TryAdd(data, out quest);
            }

            return false;
        }

        /// <summary>
        /// [Optional] callback for derived classes when a quest is added.
        /// </summary>
        /// <param name="added">The quest instance that was added.</param>
        protected virtual void OnAdd(QuestInstance added) { }
        
        #endregion
        
        #region Remove

        /// <summary>
        /// Removes a quest instance from the tracker.
        /// </summary>
        /// <param name="quest">The quest instance to remove.</param>
        /// <returns>
        /// True if the quest was successfully removed;
        /// false if the quest was not found.
        /// </returns>
        public bool Remove(QuestInstance quest)
        {
            if (TryGetQuest(quest.ID, out QuestInstance q))
            {
                Debug.Log($"Quest Tracker | Removing quest ({quest.ID}).");
                quests.Remove(q);
                OnRemove(q);
                
                QuestRemoved?.Invoke(q);
                Changed?.Invoke(this);
                
                return true;
            }
            
            return false;
        }
        
        /// <summary>
        /// [Optional] callback for derived classes when a quest is removed.
        /// </summary>
        /// <param name="quest">The quest instance that was removed.</param>
        protected virtual void OnRemove(QuestInstance quest){}
        
        #endregion
        
        #region Contains

        /// <summary>
        /// Returns true if a quest with the given ID exists in the tracker.
        /// </summary>
        /// <param name="questID">The quest ID to search for.</param>
        /// <returns>
        /// True if a quest with the specified ID is currently tracked;
        /// false otherwise.
        /// </returns>
        public bool Contains(string questID)
        {
            IEnumerable<QuestInstance> q = Quests.Where(x => x.ID == questID);
            return q.Any();
        }
        
        #endregion
        
        #region Get

        public QuestInstance GetOrCreateQuest(string questID)
        {
            if (TryGetQuest(questID, out QuestInstance quest))
            {
                return quest;
            }

            if (TryAdd(questID, out quest))
            {
                return quest;
            }
            
            Debug.LogError($"Quest ({questID}) could not be created.", gameObject);
            return null;
        }
        
        /// <summary>
        /// Attempts to find a quest with the given ID.
        /// </summary>
        /// <param name="questID">The quest ID to search for.</param>
        /// <param name="quest">
        /// When this method returns, contains the quest instance if found;
        /// otherwise, <see langword="null"/>.
        /// </param>
        /// <returns>
        /// True if a quest with the specified ID was found; false otherwise.
        /// </returns>
        public bool TryGetQuest(string questID, out QuestInstance quest)
        {
            quest = quests.FirstOrDefault(x => x.ID == questID);
            return quest != null;
        }
        
        #endregion
        
        #region Pin

        /// <summary>
        /// Pins the specified quest if it exists in the tracker, unpinning any previously pinned quest.
        /// </summary>
        /// <param name="quest">The quest instance to pin.</param>
        public void PinQuest(QuestInstance quest)
        {
            if (pinnedID != quest.ID && Contains(quest.ID))
            {
                UnpinQuest();
                Debug.Log($"Quest Tracker | Pinning quest ({quest.ID}).");
                pinnedID = quest.ID;
                
                QuestPinned?.Invoke(quest);
                Changed?.Invoke(this);
            }
        }

        /// <summary>
        /// Unpins the currently pinned quest (if any) and notifies listeners.
        /// </summary>
        public void UnpinQuest()
        {
            if (TryGetQuest(pinnedID, out QuestInstance p))
            {
                Debug.Log($"Quest Tracker | Unpinning quest ({p.ID}).");
                pinnedID = default;
                
                QuestUnpinned?.Invoke(p);
                Changed?.Invoke(this);
            }
        }
        
        #endregion
        
        #region Status

        // public bool TryGetQuestStatus(string questID, out QuestInstance quest)
        // {
        //     return TryGetQuest(questID, out quest);
        // }

        public bool SetQuestStatus(string questID, QuestStatus status, out QuestInstance quest)
        {
            quest = GetOrCreateQuest(questID);
            quest.Status = status;
            return true;
        }

        #region Step
        
        public virtual QuestStatus GetQuestStepStatus(string questID, int index)
        {
            if (TryGetQuest(questID, out QuestInstance quest)
                && quest.TryGetStep(index, out StepInstance step))
            {
                return step.Status;
            }

            return QuestStatus.None;
        }

        public virtual bool SetQuestStepStatus(string questID, int index, QuestStatus status, out QuestInstance quest, out StepInstance step)
        {
            quest = GetOrCreateQuest(questID);
            if (quest.TryGetStep(index, out step))
            {
                step.Status = status;
                return true;
            }

            return false;
        }

        public bool AdvanceStep(string questID, int index)
        {
            if (TryGetQuest(questID, out QuestInstance quest)
                && quest.StepIndex == index)
            {
                quest.AdvanceStep();
                return true;
            }

            return false;
        }
        
        #endregion
        
        #endregion
        
        #endregion
    }
}