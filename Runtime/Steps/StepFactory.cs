using System;

namespace Fsi.QuestSystem.Steps
{
    public static class StepFactory
    {
        public static StepInstance CreateStep(StepData data, QuestStatus status)
        {
            return data.StepType switch
            {
                StepType.Enemy => new EnemyStepInstance(data, status),
                StepType.NPC => new NPCStepInstance(data, status),
                StepType.Location => new LocationStepInstance(data, status),
                StepType.Item => new ItemStepInstance(data, status),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}