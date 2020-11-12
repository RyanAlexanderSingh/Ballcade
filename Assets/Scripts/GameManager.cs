using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<BallSpawner> _ballSpawners = new List<BallSpawner>();
    [SerializeField] private float _spawnDelay;
    
    void Start()
    {
        StartCoroutine(CoSpawnBalls());
    }

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
}
