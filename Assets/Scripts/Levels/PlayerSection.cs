using UnityEngine;

namespace Ballcade
{
    public class PlayerSection : MonoBehaviour
    {
        #region Vars

        [SerializeField]
        private bool _isPlayerSection;

        [SerializeField]
        private GoalCollider _goalCollider;

        [SerializeField]
        private Transform _startingPosition;

        [SerializeField]
        private Animator _ballBlockerAnimator;

        public Transform StartingPosition => _startingPosition;

        public bool IsPlayerSection => _isPlayerSection;
    
        public bool IsSectionOccupied { get; private set; }

        private int _goalOwnerPlayerIdx;
    
        private static readonly int Raise = Animator.StringToHash("Raise");

        #endregion

        #region Initialise

        public void Setup(int goalOwnerPlayerIdx)
        {
            _goalOwnerPlayerIdx = goalOwnerPlayerIdx;
            _goalCollider.Setup(_goalOwnerPlayerIdx);
            IsSectionOccupied = true;
        }

        #endregion

        #region Section

        /*
     * This will typically be called when a player has died and no more balls should enter the section
     */

        public void OnPlayerDied(PlayerDiedData playerDiedData)
        {
            if (_goalOwnerPlayerIdx == playerDiedData.playerController.PlayerIdx)
            {
                RaiseBallBlocker();
            }
        }
    
        private void RaiseBallBlocker()
        {
            _ballBlockerAnimator.SetTrigger(Raise);
        }

        #endregion

    }
}
