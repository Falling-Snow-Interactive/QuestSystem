using System.Collections.Generic;
using Fsi.QuestSystem.Steps;
using Fsi.QuestSystem.Tracker;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fsi.QuestSystem.Ui.Hud
{
    public class PinnedQuestWidget : MonoBehaviour
    {
        private QuestInstance quest;

        [SerializeField]
        private RectTransform root;
        
        [SerializeField]
        private List<GameObject> content = new();
        
        [SerializeField]
        private TMP_Text titleText;

        [SerializeField]
        private Transform stepGroup;

        [SerializeField]
        private TMP_Text stepPrefab;

        private TMP_Text stepText;

        #region MonoBehaviour
        
        private void Awake()
        {
            ClearQuest();
            HideContent();
        }
        
        private void OnEnable()
        {
            QuestTracker.QuestPinned += OnQuestPinned;
            QuestTracker.QuestUnpinned += OnQuestUnpinned;

            QuestInstance.Updated += OnQuestUpdated;
        }

        private void OnDisable()
        {
            QuestTracker.QuestPinned -= OnQuestPinned;
            QuestTracker.QuestUnpinned -= OnQuestUnpinned;

            QuestInstance.Updated -= OnQuestUpdated;
        }
        
        #endregion
        
        #region Show Content
        
        private void ShowContent()
        {
            foreach (GameObject c in content)
            {
                c.SetActive(true);
            }
        }

        private void HideContent()
        {
            foreach (GameObject c in content)
            {
                c.SetActive(false);
            }
        }
        
        #endregion
        
        #region Update Quest

        private void Refresh()
        {
            ClearQuest();
            ShowContent();
            titleText.text = quest.Name;
            if (quest.TryGetActiveQuestStep(out StepInstance step))
            {
                stepText = Instantiate(stepPrefab, stepGroup);
                stepText.text = step.GetDescription();
            }
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(root);
        }

        private void ClearQuest()
        {
            titleText.text = "";
            for (int i = stepGroup.childCount - 1; i >= 0; i--)
            {
                Destroy(stepGroup.GetChild(i).gameObject);
            }

            HideContent();
        }
        
        #endregion

        #region Event callbacks
        
        private void OnQuestPinned(QuestInstance quest)
        {
            this.quest = quest;
            Refresh();
        }
        
        private void OnQuestUnpinned(QuestInstance quest)
        {
            if (this.quest.ID == quest.ID)
            {
                ClearQuest();
            }
        }

        private void OnQuestUpdated(QuestInstance quest)
        {
            if (this.quest.ID == quest.ID)
            {
                Refresh();
            }
        }
        
        #endregion
    }
}
