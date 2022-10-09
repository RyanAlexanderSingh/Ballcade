using UnityEngine;

namespace Ballcade
{
    [CreateAssetMenu(fileName = "BallSpawnData", menuName = "Ballcade/Ball Spawner Data")]
    public class BallSpawnData : ScriptableObject
    {
        #region Vars

        public float _ballSpawnRandomArc = 15f;

        #endregion
    }
}