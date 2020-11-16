using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameLevelData", menuName = "Ballcade/Game Level Data")]
public class GameLevelData : ScriptableObject
{
    #region Vars

    [SerializeField] private int _numOfLives;

    [SerializeField] private int _ballSpawnDelay;

    public int NumberOfStartingLives => _numOfLives;
    
    #endregion

    /*
     * In future, this will not just be a standard number but will probably increase over time with a curve
     */
    public int GetNextBallSpawnDelay()
    {
        return _ballSpawnDelay;
    }
}