using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField]
    private Transform _spawnPoint;

    [SerializeField]
    private BallSpawnData _ballSpawnData;
    
    [SerializeField]
    private BallSpawnedEvent onBallSpawnedSpawnedEvent;

    #endregion


    #region Spawn

    public void SpawnBall()
    {
        GameObject pooledBall = ObjectPoolManager.instance.GetPooledObject(PoolableObjects.Ball);
        pooledBall.SetActive(true);

        float ballSpawnRandomArc = _ballSpawnData._ballSpawnRandomArc;

        Vector3 defaultSpawnPointRotation = _spawnPoint.rotation.eulerAngles;
        float defaultSpawnYRot = defaultSpawnPointRotation.y;

        float randomYRotation = Random.Range(defaultSpawnYRot - ballSpawnRandomArc, defaultSpawnYRot + ballSpawnRandomArc);
        Quaternion randomSpawnRotation = Quaternion.Euler(new Vector3(defaultSpawnPointRotation.x, randomYRotation, defaultSpawnPointRotation.z));
        
        pooledBall.transform.SetPositionAndRotation(_spawnPoint.position, randomSpawnRotation);
        Ball ball = pooledBall.GetComponent<Ball>();
        ball.ApplySpawnForce();

        onBallSpawnedSpawnedEvent.Raise(ball);
    }

    #endregion
}