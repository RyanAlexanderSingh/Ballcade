using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private Transform _spawnPoint;

    #endregion

    #region Vars

    public bool spawnBall;

    #endregion

    #region Events

    public event Action<Transform> OnBallSpawnedEvent;

    #endregion


    #region Update

    private void Update()
    {
        if (spawnBall)
        {
            spawnBall = false;
            SpawnBall();
        }
    }

    #endregion


    #region Spawn

    public void SpawnBall()
    {
        GameObject pooledBall = ObjectPoolManager.instance.GetPooledObject(PoolableObjects.Ball);
        pooledBall.SetActive(true);
        pooledBall.transform.SetPositionAndRotation(_spawnPoint.position, _spawnPoint.rotation);
        Ball ball = pooledBall.GetComponent<Ball>();
        ball.ApplySpawnForce();

        OnBallSpawnedEvent?.Invoke(ball.transform);
    }

    #endregion
}