using System;
using Fsi.QuestSystem.Tracker;
using UnityEngine;

namespace Fsi.QuestSystem.Ui.Hud
{
    public class QuestIndicatorWidget : MonoBehaviour
    {
        [Header("References")]

        [SerializeField]
        private GameObject indicatorObject;

        private void Awake()
        {
            indicatorObject?.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            QuestTracker.QuestPinned += OnQuestPinned;
            QuestTracker.QuestUnpinned += OnQuestUnpinned;
        }

        private void OnDisable()
        {
            QuestTracker.QuestPinned -= OnQuestPinned;
            QuestTracker.QuestUnpinned -= OnQuestUnpinned;
        }

        private void OnQuestPinned(QuestInstance quest)
        {
            
        }

        private void OnQuestUnpinned(QuestInstance quest)
        {
            
        }

        private void LateUpdate()
        {
            
        }
    }
}
