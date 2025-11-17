using System.Collections.Generic;
using Fsi.QuestSystem.Steps;
using Fsi.QuestSystem.Tracker;
using TMPro;
using UnityEngine;

namespace Fsi.QuestSystem.Ui.Hud
{
    public class PinnedQuestWidget : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> content = new();
        
        [SerializeField]
        private TMP_Text titleText;

        [SerializeField]
        private Transform stepGroup;

        [SerializeField]
        private TMP_Text stepPrefab;

        #region MonoBehaviour
        
        private void Awake()
        {
            ClearQuest();
            HideContent();
        }
        
        private void OnEnable()
        {
            QuestTracker.QuestPinned += OnQuestQuestPinned;
            QuestTracker.QuestUnpinned += OnQuestQuestUnpinned;
        }

        private void OnDisable()
        {
            QuestTracker.QuestPinned -= OnQuestQuestPinned;
            QuestTracker.QuestUnpinned -= OnQuestQuestUnpinned;
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

        private void ShowQuest(QuestInstance quest)
        {
            ShowContent();
            titleText.text = quest.Name;
            if (quest.TryGetActiveQuestStep(out StepInstance step))
            {
                TMP_Text stepText = Instantiate(stepPrefab, stepGroup);
                stepText.text = step.GetDescription();
            }
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
        
        private void OnQuestQuestPinned(QuestInstance quest)
        {
            ShowQuest(quest);
        }
        
        private void OnQuestQuestUnpinned(QuestInstance quest)
        {
            ClearQuest();
        }
        
        #endregion
    }
}
