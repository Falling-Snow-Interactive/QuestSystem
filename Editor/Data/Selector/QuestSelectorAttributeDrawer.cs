using System.Collections.Generic;
using Fsi.DataSystem.Selectors;
using Fsi.QuestSystem.Settings;
using UnityEditor;

namespace Fsi.QuestSystem.Data.Selector
{
    [CustomPropertyDrawer(typeof(QuestSelectorAttribute))]
    public class QuestSelectorAttributeDrawer : SelectorAttributeDrawer<QuestData, QuestDataID>
    {
        protected override List<QuestData> GetData() => QuestSystemSettings.Quests;
    }
}