using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour, IPlayer
{

    #region Vars

    [SerializeField] private PlayerData _playerData;

    #endregion
    

    #region Updates

    void Update()
    {
        UpdateMovement();
    }

    public void UpdateMovement()
    {
        
    }

    #endregion
    
}