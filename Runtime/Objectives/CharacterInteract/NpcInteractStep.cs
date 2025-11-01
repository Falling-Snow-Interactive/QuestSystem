using UnityEngine;

namespace Fsi.QuestSystem.Objectives.CharacterInteract
{
    [CreateAssetMenu(menuName = "Fsi/Quest System/Quest/Step/Npc Interaction", fileName = "New Objective Data")]
    public class NpcInteractStep : QuestStep
    {
        public override ObjectiveDataId ObjectiveId => ObjectiveDataId.NpcInteract;

        [SerializeField]
        private string npcTag;
        public string NpcTag => npcTag;
    }
}