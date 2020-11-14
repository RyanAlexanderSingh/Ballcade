using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    #region Vars

    [SerializeField] private List<BallSpawner> _ballSpawners = new List<BallSpawner>();
    [SerializeField] private List<GoalCollider> _goalColliders = new List<GoalCollider>();
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _ballReturnToPoolDelay = 2f;
    [SerializeField] private AIPlayerMananger _aiPlayerMananger;

    private List<Transform> _activeBallsInScene = new List<Transform>();
    
    #endregion
    
    #region Consts

    private const string _kBallLayerName = "Ball";

    #endregion


    #region Initialise
    

    void Start()
    {
        StartCoroutine(CoSpawnBalls());
        AddListeners();
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
        
        scoredBall.StopPhysics();
        
        ObjectPoolManager.instance.ReturnToPool(scoredBall.gameObject);
    }

    #endregion

    
    #region Listeners

    private void OnGoalColliderEventHandler(GameObject collisionObj)
    {
        Ball ball = collisionObj.GetComponent<Ball>();
        
        if (ball == null)
            return;

        StartCoroutine(CoHandleScoredBallObj(ball));
    }

    private void OnBallSpawnedEventHandler(Transform ballTransform)
    {
        _activeBallsInScene.Add(ballTransform.transform);
        _aiPlayerMananger.UpdateActiveBallsForAI(_activeBallsInScene);
    }

    #endregion


    #region Events

    private void AddListeners()
    {
        AddGoalColliderListeners();
        AddBallSpawnerListeners();
    }
    
    private void RemoveListeners()
    {
        RemoveGoalColliderListeners();
        RemoveBallSpawnerListener();
    }

    private void AddGoalColliderListeners()
    {
        foreach (var goalCollider in _goalColliders)
        {
            goalCollider.OnCollisionEnterEvent += OnGoalColliderEventHandler;
        }
    }

    private void RemoveGoalColliderListeners()
    {
        foreach (var goalCollider in _goalColliders)
        {
            goalCollider.OnCollisionEnterEvent -= OnGoalColliderEventHandler;
        }
    }

    private void AddBallSpawnerListeners()
    {
        foreach (var ballSpawner in _ballSpawners)
        {
            ballSpawner.OnBallSpawnedEvent += OnBallSpawnedEventHandler;
        }
    }

    private void RemoveBallSpawnerListener()
    {
        foreach (var ballSpawner in _ballSpawners)
        {
            ballSpawner.OnBallSpawnedEvent -= OnBallSpawnedEventHandler;
        }
    }

    #endregion

    #region Destroy

    private void OnDestroy()
    {
        RemoveListeners();
    }

    #endregion
}
