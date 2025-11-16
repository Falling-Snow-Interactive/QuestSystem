using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.QuestSystem.Tracker
{
    public abstract class QuestTrackerEditor : EditorWindow
    {
        private const string EnabledGroupId = "enabled_group";
        private const string DisabledGroupId = "disabled_group";
        private const string QuestDropdownId = "quest_dropdown";
        private const string QuestAddButtonId = "quest_add_button";
        private const string QuestListId = "quest_list";

        protected virtual bool Enabled => Application.isPlaying;
        
        [SerializeField]
        private VisualTreeAsset document = default;
        
        // UI Elements
        private VisualElement enabledGroup;
        private VisualElement disabledGroup;
        private DropdownField questDropdown;
        private Button questAddButton;
        private MultiColumnListView questList;

        // Example Menu: [MenuItem("Window/FSI/Quest System/Quest Tracker")]
        // protected static void OpenWindow()
        // {
        //     QuestTrackerEditor wnd = GetWindow<QuestTrackerEditor>();
        //     wnd.titleContent = new GUIContent("Quest Tracker");
        // }

        protected virtual void OnEnable()
        {
            QuestTracker.Initialized += OnInitialized;
            AddEvents();
        }

        protected virtual void OnDisable()
        {
            QuestTracker.Initialized -= OnInitialized;
            RemoveEvents();
        }

        private void AddEvents()
        {
            if (Enabled)
            {
                QuestTracker.Changed += OnTrackerChanged;
            }
        }

        private void RemoveEvents()
        {
            if (Enabled)
            {
                QuestTracker.Changed -= OnTrackerChanged;
            }
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;
            document.CloneTree(root);
            
            // Get the different object references
            enabledGroup = root.Q<VisualElement>(EnabledGroupId);
            disabledGroup = root.Q<VisualElement>(DisabledGroupId);
            questDropdown = root.Q<DropdownField>(QuestDropdownId);
            questAddButton = root.Q<Button>(QuestAddButtonId);
            questList = root.Q<MultiColumnListView>(QuestListId);

            questAddButton.clicked += AddButtonClicked;

            List<string> ids = GetQuestIDs();
            ids.Insert(0, "Select Quest");
            questDropdown.choices = ids;
            
            SetupColumns();

            if (Enabled)
            {
                OnInitialized(QuestTracker.Instance);
            }
            
            Refresh();
        }
        
        private void SetupColumns()
        {
            Columns cols = questList.columns;

            cols["id"].makeCell = () => new Label();
            cols["id"].bindCell = (ve, rowIndex) =>
            {
                QuestInstance qi = (QuestInstance)questList.itemsSource[rowIndex];
                ((Label)ve).text = qi.ID;
            };
            
            cols["name"].makeCell = () => new Label();
            cols["name"].bindCell = (ve, rowIndex) =>
            {
                QuestInstance qi = (QuestInstance)questList.itemsSource[rowIndex];
                ((Label)ve).text = qi.Name;
            };
            
            cols["data"].makeCell = () => new ObjectField();
            cols["data"].bindCell = (ve, rowIndex) =>
            {
                QuestInstance qi = (QuestInstance)questList.itemsSource[rowIndex];
                ((ObjectField)ve).value = qi.Data;
            };
            
            cols["status"].makeCell = () => new Label();
            cols["status"].bindCell = (ve, rowIndex) =>
            {
                QuestInstance qi = (QuestInstance)questList.itemsSource[rowIndex];
                ((Label)ve).text = qi.Status.ToString();
            };
        }
        
        protected virtual void Refresh()
        {
            UpdateActiveGroup();

            if (questList != null)
            {
                if (!Enabled)
                {
                    questList.itemsSource = null;
                    return;
                }

                questList.Clear();
                questList.itemsSource = QuestTracker.Instance.Quests.ToList();
                questList.Rebuild();
            }
        }

        private void UpdateActiveGroup()
        {
            if (enabledGroup != null)
            {
                enabledGroup.style.display = Enabled ? DisplayStyle.Flex : DisplayStyle.None;
            }

            if (disabledGroup != null)
            {
                disabledGroup.style.display = !Enabled ? DisplayStyle.Flex : DisplayStyle.None;
            }
        }
        
        #region Tracker
        
        public void OnInitialized(QuestTracker questTracker)
        {
            RemoveEvents();
            AddEvents();
            Refresh();
        }
        
        private void OnTrackerChanged(QuestTracker questTracker)
        {
            questList?.RefreshItems();
        }
        
        #endregion
        
        #region Quests

        private void AddButtonClicked()
        {
            string questDropdownVal = questDropdown.value;
            Debug.Log(questDropdownVal);
            QuestTracker.Instance.Add(questDropdownVal);
        }

        protected abstract List<string> GetQuestIDs();
        
        #endregion
    }
}
