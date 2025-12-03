using System;
using Fsi.Characters.Data;
using Fsi.Characters.Gameplay;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Fsi.QuestSystem.Steps
{
    [Serializable]
    public class NpcStepInstance : StepInstance, IScenePosition
    {
        public string SceneID => $"{characterData.ID}_{instanceID}";
        
        [SerializeField]
        private CharacterData characterData;

        [SerializeField]
        private int instanceID;
        
        public NpcStepInstance(StepData data) : base(data)
        {
            characterData = data.Npc;
            instanceID = data.NPCInstanceID;
        }

        public bool TryGetScenePosition(out Vector3 position)
        {
            if (CharacterManager.Instance.Characters.TryGetValue(SceneID, out CharacterInstance character))
            {
                position = character.transform.position;
                return true;
            }

            position = Vector3.zero;
            return false;
        }

        public override string GetDescription()
        {
            return $"Talk to {Data.Npc.Name}";
        }
    }
}