using Fsi.Characters.Gameplay;
using Fsi.QuestSystem.Tracker;
using UnityEngine;

namespace Fsi.QuestSystem.Ui.Hud
{
    public class QuestIndicatorWidget : MonoBehaviour
    {
        private QuestInstance quest;
        private bool hasPosition;
        private Vector3 position;

        [SerializeField]
        private Vector3 offset;
        
        [Header("References")]

        [SerializeField]
        private GameObject indicatorObject;

        [SerializeField]
        private new Camera camera;

        private void Awake()
        {
            indicatorObject?.gameObject.SetActive(false);
        }
        
        private void OnEnable()
        {
            QuestTracker.QuestPinned += ShowQuest;
            QuestTracker.QuestUnpinned += HideQuest;

            CharacterManager.Changed += UpdatePosition;
        }

        private void OnDisable()
        {
            QuestTracker.QuestPinned -= ShowQuest;
            QuestTracker.QuestUnpinned -= HideQuest;
            
            CharacterManager.Changed -= UpdatePosition;
        }

        private void Start()
        {
            if (QuestTracker.Instance
                && QuestTracker.Instance.PinnedQuest != null)
            {
                ShowQuest(QuestTracker.Instance.PinnedQuest);
            }

            if (!camera)
            {
                camera = Camera.main;
            }
        }

        private void LateUpdate()
        { 
            if (hasPosition)
            {
                Vector3 screen = camera.WorldToScreenPoint(position + offset);
                indicatorObject.transform.position = screen;
            }
        }

        private void UpdatePosition(CharacterManager manager)
        {
            hasPosition = quest.TryGetScenePosition(out position);
            indicatorObject.gameObject.SetActive(hasPosition);
        }
        
        private void ShowQuest(QuestInstance quest)
        {
            this.quest = quest;
            hasPosition = quest.TryGetScenePosition(out position);
            indicatorObject.gameObject.SetActive(hasPosition);
        }

        private void HideQuest(QuestInstance quest)
        {
            this.quest = null;
            hasPosition = false;
            indicatorObject.gameObject.SetActive(hasPosition);
        }
    }
}
