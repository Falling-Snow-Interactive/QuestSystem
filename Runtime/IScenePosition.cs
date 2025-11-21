using UnityEngine;

namespace Fsi.QuestSystem
{
    public interface IScenePosition
    {
        public bool TryGetScenePosition(out Vector3 position);
    }
}