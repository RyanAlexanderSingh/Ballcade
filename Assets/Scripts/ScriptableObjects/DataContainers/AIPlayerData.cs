using UnityEngine;

namespace Ballcade
{
    [CreateAssetMenu(fileName = "AIPlayerData", menuName = "Ballcade/AI Player Data")]
    public class AIPlayerData : PlayerData
    {
        #region Vars

        public float defaultReactionDelay = 0.1f;

        #endregion
    }
}