using System.Collections.Generic;
using Fsi.DataSystem.Selectors;
using Fsi.QuestSystem.Settings;
using UnityEditor;

namespace Fsi.QuestSystem.Objectives.Selector
{
    [CustomPropertyDrawer(typeof(ObjectiveSelectorAttribute), true)]
    public class ObjectiveSelectorAttributeDrawer : SelectorAttributeDrawer<ObjectiveData, ObjectiveDataId>
    {
        protected override List<ObjectiveData> GetData() => QuestSystemSettings.Objectives;
    }
}