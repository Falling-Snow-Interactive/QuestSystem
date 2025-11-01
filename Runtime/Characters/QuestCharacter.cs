using Fsi.Characters;
using UnityEngine;

namespace Fsi.QuestSystem.Characters
{
    /// <summary>
    /// Ths is the actual in scene objects for characters.
    /// They are all required to have a unique ID. A validator can be found in ProjectSettings/Fsi/Characters
    /// </summary>
    public class QuestCharacter : MonoBehaviour
    {
        [SerializeField]
        private CharacterData characterData;
        public CharacterData CharacterData => characterData;
    }
}