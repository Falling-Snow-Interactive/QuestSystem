using Fsi.Characters;
using Fsi.Characters.Selector;
using UnityEngine;

namespace Fsi.QuestSystem.Objectives.DefeatEnemies
{
    [CreateAssetMenu(menuName = "Fsi/Quest System/Quest/Step/Defeat Enemy", fileName = "New Quest Step")]
    public class DefeatEnemyStep : QuestStep
    {
        public override ObjectiveDataId ObjectiveId => ObjectiveDataId.DefeatEnemies;
        
        [EnemySelector]
        [SerializeField]
        private EnemyData enemy;
        public EnemyData Enemy => enemy;

        [Min(1)]
        [SerializeField]
        private int amount = 1;
        public int Amount => amount;
    }
}