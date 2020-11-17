using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GameLevelData", menuName = "Ballcade/Game Level Data")]
public class GameLevelData : ScriptableObject
{
    #region Enums

    public enum AdditiveLevelScene
    {
        Arena_01
    }
    
    #endregion
    
    
    #region Vars

    [SerializeField]
    private Level _level;
    
    [SerializeField] 
    private AdditiveLevelScene levelScene;
    
    [Tooltip("Num of AI opponents. If it is a 4 way arena, this number should be 3")]
    [SerializeField] 
    private int _numOfAIOpponents;
    
    [SerializeField] 
    private int _numOfLives;

    [SerializeField] 
    private float _ballSpawnDelay;

    public Level Level => _level;

    public int NumOfAIOpponents => _numOfAIOpponents;

    public AdditiveLevelScene LevelScene => levelScene;
    
    public int NumberOfStartingLives => _numOfLives;

    
    #endregion

    /*
     * In future, this will not just be a standard number but will probably increase over time with a curve
     */
    public float GetNextBallSpawnDelay()
    {
        return _ballSpawnDelay;
    }
}