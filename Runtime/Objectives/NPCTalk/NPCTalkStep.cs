using Fsi.Characters.NPCs;
using Fsi.Characters.Selector;
using UnityEngine;

namespace Fsi.QuestSystem.Objectives.CharacterInteract
{
    [CreateAssetMenu(menuName = "Fsi/Quest System/Quest/Step/Npc Interaction", fileName = "New Objective Data")]
    public class NPCTalkStep : QuestStep
    {
        public override ObjectiveDataId ObjectiveId => ObjectiveDataId.NpcInteract;

        [NPCSelector]
        [SerializeField]
        private NPCData npcData;
        public NPCData NPCData => npcData;
        
        // TODO - It'll need to have the conversation, if you're given anything, and 
    }
}