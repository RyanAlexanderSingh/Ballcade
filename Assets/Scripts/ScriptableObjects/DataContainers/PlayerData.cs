using UnityEngine;

namespace Ballcade
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Ballcade/Player Data")]
    public class PlayerData : ScriptableObject
    {
        #region Vars

        public float playerSpeed = 10f;
        public float maxMovePosition = 7.5f;
        public float minMovePos = -7.5f;

        #endregion
    }
}