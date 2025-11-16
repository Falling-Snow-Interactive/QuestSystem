using System;
using UnityEngine;

namespace Fsi.QuestSystem.Steps
{
    [Serializable]
    public class StepInstance
    {
        [SerializeField]
        private StepData data;
        public StepData Data => data;

        [SerializeField]
        private QuestStatus status;
        public QuestStatus Status => status;

        [SerializeField]
        private int amount;
        
        // TODO - Need to keep track of the step "value". Ex: items collected or enemies defeated. - Kira
        // Probably make some subclasses and a factory to handle that. 
        
        public StepInstance(StepData data, QuestStatus status)
        {
            this.data = data;
            this.status = status;

            switch (data.StepType)
            {
                case StepType.None:
                    break;
                case StepType.Enemy:
                    amount = data.NumberOfEnemies;
                    break;
                case StepType.NPC:
                    break;
                case StepType.Location:
                    break;
                case StepType.Item:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        /// <summary>
        /// Returns the description of the quest step based on the set properties.
        /// </summary>
        /// <returns></returns>
        public string GetDescription()
        {
            return data.StepType switch
            {
                // TODO - Localize this. - Kira
                StepType.None => "None",
                StepType.Enemy => $"Defeat {amount}/{data.NumberOfEnemies} {data.Enemy.Name}",
                StepType.NPC => $"Talk to {data.Npc.Name}",
                StepType.Location => $"Go to location.name",
                StepType.Item => $"Bring {amount}/{data.NumberOfItem} {data.Item.Name} to {data.Npc}",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}