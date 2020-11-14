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
    
    private int ballLayer;

    #endregion
    
    #region Consts

    private const string _kBallLayerName = "Ball";

    #endregion
   
    
    #region Initialise
    
    void Awake()
    {
        ballLayer = LayerMask.NameToLayer(_kBallLayerName);
    }

    void Start()
    {
        StartCoroutine(CoSpawnBalls());
        AddListeners();
    }

    private void Update()
    {
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

    #endregion


    #region Events

    private void AddListeners()
    {
        AddGoalColliderListeners();
    }
    
    private void RemoveListeners()
    {
        RemoveGoalColliderListeners();
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

    #endregion

    #region Destroy

    private void OnDestroy()
    {
        RemoveListeners();
    }

    #endregion
}
