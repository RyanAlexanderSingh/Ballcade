using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ballcade
{
    public class AIPlayerController : PlayerController, IPlayer
    {
        #region Vars

        private List<Ball> _activeBalls = new List<Ball>();

        private Transform _activeTargetBall;

        private AIPlayerData _aiPlayerData;

        #endregion


        #region Updates

        private void Start()
        {
            _aiPlayerData = _playerData as AIPlayerData;
            if (_aiPlayerData == null)
            {
                Debug.LogError("Incorrect PlayerData has been provided to this AIPlayer");
            }

            StartCoroutine(CoSetClosestActiveBall());
        }


        public void UpdateActiveBallsList(List<Ball> activeBalls)
        {
            _activeBalls = activeBalls;
        }

        public void ManualUpdate()
        {
            UpdateMovement();
        }

        IEnumerator CoSetClosestActiveBall()
        {
            while (IsPlayerAlive)
            {
                if (_activeBalls.Any())
                {
                    var activeBalls = _activeBalls;
                    _activeTargetBall = GetClosestBallTransform(activeBalls);
                }

                yield return new WaitForSeconds(_aiPlayerData.defaultReactionDelay);
            }
        }


        public void UpdateMovement()
        {
            if (!IsPlayerAlive)
                return;

            if (_activeTargetBall == null)
                return;

            Vector3 currentPosition = transform.position;

            Debug.DrawRay(currentPosition, _activeTargetBall.position - currentPosition, Color.green);

            Vector3 targetPos = GetClampedTargetPosition(_activeTargetBall.transform.position);

            transform.position = Vector3.MoveTowards(currentPosition, targetPos, Time.deltaTime * _playerData.playerSpeed);
        }

        #endregion


        #region Ball Search

        Transform GetClosestBallTransform(List<Ball> balls)
        {
            Transform tMin = null;
            float minBallDist = Mathf.Infinity;
            float maxDistToLook = 10f;
            Vector3 currentPos = transform.position;

            foreach (var ball in balls)
            {
                Transform t = ball.transform;
                float dist = Vector3.Distance(t.position, currentPos);
                if (dist < minBallDist && dist < maxDistToLook)
                {
//                Vector3 targetDir = transform.position - t.position;
//                float angle = Vector3.Angle(targetDir, t.forward);
//                if (angle < 40f)
//                {
//                    Debug.Log(angle);
                    tMin = t;
                    minBallDist = dist;
//                }
                }
            }

            return tMin;
        }

        #endregion
    }
}