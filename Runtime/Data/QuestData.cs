using Fsi.DataSystem;
using UnityEngine;

namespace Fsi.QuestSystem.Data
{
    [CreateAssetMenu(menuName = "Fsi/Quest System/Quests/Data", fileName = "New Quest Data")]
    public class QuestData : ScriptableData<QuestDataID>
    {
        // TODO - Icon, Rewards
        // I don't think the data needs to know the steps. Those can be in the scene object I think.
    }
}
