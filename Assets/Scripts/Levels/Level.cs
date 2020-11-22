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

    private GameLevelData _gameLevelData;

    private bool isGameActive = true;

    public List<PlayerSection> PlayerSections => _playerSections;

    #endregion


    #region Initialise

    public void Setup(GameLevelData gameLevelData)
    {
        _gameLevelData = gameLevelData;

        StartCoroutine(CoSpawnBalls());
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