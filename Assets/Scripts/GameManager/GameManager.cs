using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    #region Vars

    [SerializeField] private List<BallSpawner> _ballSpawners = new List<BallSpawner>();
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _ballReturnToPoolDelay = 2f;
    [SerializeField] private AIPlayerMananger _aiPlayerMananger;

    private List<Transform> _activeBallsInScene = new List<Transform>();

    #endregion


    #region Initialise

    void Start()
    {
        StartCoroutine(CoSpawnBalls());
    }

    #endregion


    #region Coroutines

    private IEnumerator CoSpawnBalls()
    {
        int numSpawners = _ballSpawners.Count;

        while (true)
        {
            yield return new WaitForSeconds(_spawnDelay);

            int randomIdx = Random.Range(0, numSpawners);
            _ballSpawners[randomIdx].SpawnBall();
        }
    }

    private IEnumerator CoHandleScoredBallObj(Ball scoredBall)
    {
        // remove the ball from active balls list immediately
        _activeBallsInScene.Remove(scoredBall.transform);
        _aiPlayerMananger.UpdateActiveBallsForAI(_activeBallsInScene);

        yield return new WaitForSeconds(_ballReturnToPoolDelay);

        scoredBall.Deactivate();

        ObjectPoolManager.instance.ReturnToPool(scoredBall.gameObject);
    }

    #endregion


    #region Listeners

    public void OnGoalColliderEventHandler(GameObject collisionObj)
    {
        Ball ball = collisionObj.GetComponent<Ball>();

        if (ball == null)
            return;

        StartCoroutine(CoHandleScoredBallObj(ball));
    }

    public void OnBallSpawnedEventHandler(GameObject ballGo)
    {
        _activeBallsInScene.Add(ballGo.transform);
        _aiPlayerMananger.UpdateActiveBallsForAI(_activeBallsInScene);
    }

    #endregion
}