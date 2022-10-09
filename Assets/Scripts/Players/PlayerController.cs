using UnityEngine;

namespace Ballcade
{
    public class PlayerController : MonoBehaviour
    {
        #region Vars

        [SerializeField]
        protected PlayerData _playerData;

        [SerializeField]
        private PlayerModel _playerModel;

        [SerializeField]
        private PlayerDiedEvent _playerDiedEvent;

        private static readonly int MovementDirAnimParameter = Animator.StringToHash("MovementDir");

        public int PlayerIdx { get; private set; }

        public bool IsPlayerAlive => CurrentLives > 0;

        public int CurrentLives { get; private set; }

        public CharacterVisualData VisualData { get; private set; }
    
        public bool CanMove { get; private set; }

        #endregion


        #region Initialise

        private void Awake()
        {
            CanMove = true;
        }

        public void SetPlayerData(CharacterVisualData characterVisualData, int startingLives, int playerIdx)
        {
            PlayerIdx = playerIdx;
            CurrentLives = startingLives;

            VisualData = characterVisualData;
            _playerModel.SetVisualdData(VisualData);
        }

        #endregion


        #region Helpers

        protected Vector3 GetClampedTargetPosition(Vector3 targetPosition)
        {
            Vector3 currentPosition = transform.position;
            Vector3 clampedPos;

            if (Mathf.Abs(transform.right.x) == 1f)
            {
                clampedPos = new Vector3(Mathf.Clamp(targetPosition.x, _playerData.minMovePos, _playerData.maxMovePosition),
                    currentPosition.y, currentPosition.z);
            }
            else
            {
                clampedPos = new Vector3(currentPosition.x, currentPosition.y,
                    Mathf.Clamp(targetPosition.z, _playerData.minMovePos, _playerData.maxMovePosition));
            }

            return clampedPos;
        }

        #endregion

        #region Player State

        public void ReducePlayerLives(int reduction)
        {
            if (!IsPlayerAlive)
                return;

            CurrentLives -= reduction;

            if (CurrentLives == 0)
            {
                KillPlayer();
            }
        }

        private void KillPlayer()
        {
            gameObject.SetActive(false);

            PlayerDiedData playerDiedData = new PlayerDiedData
            {
                playerController = this
            };
            _playerDiedEvent.Raise(playerDiedData);
        }

        public void PlayerWon()
        {
            CanMove = false;

            SetAnimBasedOnMovementDir(0);
        }

        #endregion


        #region Animations

        protected void SetAnimBasedOnMovementDir(float dir)
        {
            _playerModel.Animator.SetInteger(MovementDirAnimParameter, (int) dir);
        }

        #endregion
    }
}