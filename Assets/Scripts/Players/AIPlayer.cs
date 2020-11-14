using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIPlayer : Player, IPlayer
{
    #region Vars

    private List<Transform> _activeBalls = new List<Transform>();

    private Transform _activeTargetBall;

    #endregion


    #region Updates

    private void Start()
    {
        StartCoroutine(CoSetClosestActiveBall());
    }


    public void UpdateActiveBallsList(List<Transform> activeBalls)
    {
        _activeBalls = activeBalls;
    }

    public void ManualUpdate()
    {
        if (_activeTargetBall != null)
        {
            Debug.DrawRay(transform.position, _activeTargetBall.position - transform.position, Color.green);
        }

        UpdateMovement();
    }

    IEnumerator CoSetClosestActiveBall()
    {
        while (true)
        {
            if (_activeBalls.Any())
            {
                var activeBalls = _activeBalls;
                _activeTargetBall = GetClosestBallTransform(activeBalls);
            }

            yield return new WaitForSeconds(0.3f);
        }
    }


    public void UpdateMovement()
    {
        if (_activeTargetBall == null)
            return;

        Vector3 targetPos = GetClampedTargetPosition(_activeTargetBall.transform.position);
        transform.position =
            Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * _playerData.playerSpeed);
    }

    #endregion

    #region Ball Search

    Transform GetClosestBallTransform(List<Transform> balls)
    {
        Transform tMin = null;
        float minBallDist = Mathf.Infinity;
        float maxDistToLook = 10f;
        Vector3 currentPos = transform.position;

        foreach (Transform t in balls)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minBallDist && dist < maxDistToLook)
            {
                Vector3 targetDir = transform.position - t.position;
                float angle = Vector3.Angle(targetDir, t.forward);
                if (angle < 40f)
                {
                    Debug.Log(angle);
                    tMin = t;
                    minBallDist = dist;
                }
            }
        }

        return tMin;
    }

    #endregion
}