using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameLevelData", menuName = "Ballcade/Game Level Data")]
public class GameLevelData : ScriptableObject
{
    #region Vars

    [SerializeField] private int _numOfLives;

    #endregion

    public int GetNumberOfLivesForLevel()
    {
        return _numOfLives;
    }
}