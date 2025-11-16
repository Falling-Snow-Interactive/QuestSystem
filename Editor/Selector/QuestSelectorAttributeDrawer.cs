using System.Collections.Generic;
using Fsi.DataSystem.Selectors;
using Fsi.QuestSystem.Data.Selector;
using Fsi.QuestSystem.Settings;
using UnityEditor;

namespace Fsi.QuestSystem.Selector
{
    [CustomPropertyDrawer(typeof(QuestSelectorAttribute))]
    public class QuestSelectorAttributeDrawer : SelectorAttributeDrawer<QuestData, string>
    {
        protected override List<QuestData> GetData() => QuestSystemSettings.Quests.Entries;
    }
}