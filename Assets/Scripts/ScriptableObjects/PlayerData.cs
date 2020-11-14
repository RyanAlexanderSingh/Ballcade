using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Ballcade/Player Data")]
public class PlayerData : ScriptableObject
{
    #region Vars

    public float playerSpeed = 10f;
    public float maxXLimit = 7.5f;
    public float minXLimit = -7.5f;

    #endregion
}