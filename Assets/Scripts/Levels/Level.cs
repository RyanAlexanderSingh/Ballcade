using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    #region Vars

    [SerializeField]
    private List<PlayerSection> _playerSections = new List<PlayerSection>();

    [SerializeField]
    private List<BallSpawner> _ballSpawners = new List<BallSpawner>();

    [SerializeField]
    private Transform _winnerPositionAnchor;

    private GameLevelData _gameLevelData;

    private bool _isGameActive;

    private int _numActiveBalls;

    private Coroutine _coBallSpawning;

    public Transform WinnerPositionAnchor => _winnerPositionAnchor;

    #endregion


    #region Initialise

    public void Setup(GameLevelData gameLevelData)
    {
        _gameLevelData = gameLevelData;

        _isGameActive = true;

        _coBallSpawning = StartCoroutine(CoSpawnBalls());
    }

    public PlayerSection GetFreePlayerSection()
    {
        foreach (var section in _playerSections)
        {
            if (!section.IsSectionOccupied)
                return section;
        }

        Debug.LogError("Cannot find a free player section");
        return null;
    }

    public PlayerSection GetPlayerSection()
    {
        foreach (var section in _playerSections)
        {
            if (section.IsPlayerSection)
                return section;
        }

        Debug.LogError("Cannot find a player section in level player sections, set one in the inspector.");
        return null;
    }

    #endregion

    #region State

    public void FinishGame()
    {
        _isGameActive = false;
        StopCoroutine(_coBallSpawning);
    }

    #endregion

    #region Updates

    public void UpdateActiveBalls(int numActiveBalls)
    {
        _numActiveBalls = numActiveBalls;
    }

    #endregion


    #region Coroutines

    private IEnumerator CoSpawnBalls()
    {
        int numSpawners = _ballSpawners.Count;

        while (_isGameActive)
        {
            // if there are too many balls, wait until one has despawned before trying to spawn a new one
            if (_numActiveBalls < _gameLevelData.MaxNumActiveBalls)
            {
                // if there are less than the maximum number of balls per level then add a new ball to the level with appropriate delay
                yield return new WaitForSeconds(_gameLevelData.GetNextBallSpawnDelay());

                int randomIdx = Random.Range(0, numSpawners);
                _ballSpawners[randomIdx].SpawnBall();
            }

            yield return null;
        }
    }

    #endregion
}