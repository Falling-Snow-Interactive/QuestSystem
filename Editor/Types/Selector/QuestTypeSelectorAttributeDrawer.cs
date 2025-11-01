using System.Collections.Generic;
using Fsi.DataSystem.Selectors;
using Fsi.QuestSystem.Settings;
using UnityEditor;

namespace Fsi.QuestSystem.Types.Selector
{
    [CustomPropertyDrawer(typeof(QuestTypeSelectorAttribute))]
    public class QuestTypeSelectorAttributeDrawer : SelectorAttributeDrawer<QuestType, QuestTypeId>
    {
        protected override List<QuestType> GetData() => QuestSystemSettings.Types;
    }
}