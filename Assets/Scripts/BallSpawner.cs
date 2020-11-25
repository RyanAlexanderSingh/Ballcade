using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallSpawner : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField]
    private Transform _spawnPoint;

    [SerializeField]
    private Transform _spawnerTurntable;

    [SerializeField]
    private BallSpawnData _ballSpawnData;
    
    [SerializeField]
    private BallSpawnedEvent onBallSpawnedSpawnedEvent;

    public bool testSpawn;

    #endregion


    #region Update

    private void Update()
    {
        if (testSpawn)
        {
            FireBall();
            testSpawn = false;
        }
    }
    

    #endregion


    #region Spawn

    public void FireBall()
    {
        float ballSpawnRandomArc = _ballSpawnData._ballSpawnRandomArc;

        Vector3 defaultSpawnPointRotation = _spawnPoint.localRotation.eulerAngles;
        float defaultSpawnYRot = defaultSpawnPointRotation.y;

        float randomYRotation = Random.Range(defaultSpawnYRot - ballSpawnRandomArc, defaultSpawnYRot + ballSpawnRandomArc);

        Vector3 newSpawnRotationVector = new Vector3(defaultSpawnPointRotation.x, randomYRotation, defaultSpawnPointRotation.z);
        MoveSpawnerToRotation(newSpawnRotationVector);
    }

    private void SpawnBall()
    {
        GameObject pooledBall = ObjectPoolManager.instance.GetPooledObject(PoolableObjects.Ball);
        pooledBall.SetActive(true);
        
        pooledBall.transform.SetPositionAndRotation(_spawnPoint.position, _spawnerTurntable.rotation);
        Ball ball = pooledBall.GetComponent<Ball>();
        ball.ApplySpawnForce();

        onBallSpawnedSpawnedEvent.Raise(ball);
    }

    private void MoveSpawnerToRotation(Vector3 targetRotation)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_spawnerTurntable.DOLocalRotate(targetRotation, 0.2f));
        sequence.OnComplete(SpawnBall);

    }

    #endregion
}