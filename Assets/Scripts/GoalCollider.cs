using Ballcade.BallScoredEvent;
using UnityEngine;

namespace Ballcade
{
    public class GoalCollider : MonoBehaviour
    {
        #region Vars

        [SerializeField] private BallScoredEvent.BallScoredEvent onGoalBaseScoredEvent;

        private int _goalOwnerPlayerIdx;

        #endregion

        #region Initialise

        public void Setup(int goalOwnerPlayerIdx)
        {
            _goalOwnerPlayerIdx = goalOwnerPlayerIdx;
        }

        #endregion

        #region Collisions

        private void OnTriggerEnter(Collider other)
        {
            Ball ball = other.GetComponent<Ball>();
            if (ball == null)
            {
                Debug.LogError($"Something other than a ball collided with the goal collider: {other}");
                return;
            }
        
            BallScoredData goalScoredData = new BallScoredData
            {
                ball = ball, playerGoalIdx = _goalOwnerPlayerIdx
            };
            onGoalBaseScoredEvent.Raise(goalScoredData);
        }

        #endregion
    }
}
