using System;
using Fsi.Enemies;
using UnityEngine;

namespace Fsi.QuestSystem.Steps
{
    [Serializable]
    public class EnemyStepInstance : StepInstance
    {
        [SerializeReference]
        private EnemyData enemyData;

        [SerializeField]
        private int remaining;
        
        public EnemyStepInstance(StepData data) : base(data)
        {
            enemyData = data.Enemy;
            remaining = data.NumberOfEnemy;
        }

        public override void Enable()
        {
            EnemyInstance.Defeated += OnEnemyDefeated;
        }

        public override void Disable()
        {
            EnemyInstance.Defeated -= OnEnemyDefeated;
        }

        public override string GetDescription()
        {
            string e = remaining > 1 ? Data.Enemy.Plural : Data.Enemy.Name;
            return $"Defeat {remaining} {e}";
        }

        private void OnEnemyDefeated(EnemyInstance enemy)
        {
            if (enemy.Data.ID == enemyData.ID)
            {
                remaining--;
                ShouldAdvance = remaining <= 0;
                HasChanged();
            }
        }
    }
}