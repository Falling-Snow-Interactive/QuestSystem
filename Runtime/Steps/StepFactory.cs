using System;

namespace Fsi.QuestSystem.Steps
{
    public static class StepFactory
    {
        public static Step CreateStep(StepData data)
        {
            return data.StepType switch
            {
                StepType.Enemy => new EnemyStep(data),
                StepType.NPC => new NPCStep(data),
                StepType.Location => new LocationStep(data),
                StepType.Item => new ItemStep(data),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}