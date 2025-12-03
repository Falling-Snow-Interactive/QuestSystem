using System;

namespace Fsi.QuestSystem.Steps
{
    public static class StepFactory
    {
        public static StepInstance CreateStep(StepData data)
        {
            return data.Type switch
                   {
                       StepType.Enemy => new EnemyStepInstance(data),
                       StepType.NPC => new NpcStepInstance(data),
                       StepType.Location or StepType.Item or StepType.None => throw new ArgumentOutOfRangeException(),
                       _ => throw new ArgumentOutOfRangeException()
                   };
        }
    }
}