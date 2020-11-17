using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    #region Vars

    [SerializeField]
    private Transform _playerStartingPosition;
    
    [SerializeField]
    private List<Transform> _aiStartingPositions = new List<Transform>();
    
    [SerializeField] 
    private List<BallSpawner> _ballSpawners = new List<BallSpawner>();
    
    private GameLevelData _gameLevelData;

    private bool isGameActive = true;

    public Transform PlayerStartingPosition => _playerStartingPosition;

    public List<Transform> AIStartingPositions => _aiStartingPositions;

    #endregion
    
    
    #region Initialise

    public void Setup(GameLevelData gameLevelData)
    {
        _gameLevelData = gameLevelData;
        
        StartCoroutine(CoSpawnBalls());
    }


    #endregion
    
    #region Coroutines

    private IEnumerator CoSpawnBalls()
    {
        int numSpawners = _ballSpawners.Count;

        while (isGameActive)
        {
            yield return new WaitForSeconds(_gameLevelData.GetNextBallSpawnDelay());

            int randomIdx = Random.Range(0, numSpawners);
            _ballSpawners[randomIdx].SpawnBall();
        }
    }


    #endregion
    
}
