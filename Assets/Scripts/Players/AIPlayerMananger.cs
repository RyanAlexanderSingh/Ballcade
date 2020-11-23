using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerMananger : MonoBehaviour
{
    #region Vars

    private List<AIPlayerController> _aiPlayers = new List<AIPlayerController>();

    private bool _shouldUpdate;

    #endregion
    

    #region Initialise

    public void Initialise(List<AIPlayerController> aiPlayers)
    {
        _aiPlayers = aiPlayers;

        SetUpdateState(true);
    }

    #endregion

    #region State

    public void SetUpdateState(bool shouldUpdate)
    {
        _shouldUpdate = shouldUpdate;
    }

    #endregion
    

    #region Update

    public void Update()
    {
        if (!_shouldUpdate)
            return;
        
        foreach (AIPlayerController aiPlayer in _aiPlayers)
        {
            aiPlayer.ManualUpdate();
        }
    }
    
    public void UpdateActiveBalls(List<Ball> activeBalls)
    {
        foreach (AIPlayerController aiPlayer in _aiPlayers)
        {
            aiPlayer.UpdateActiveBallsList(activeBalls);
        }
    }

    #endregion

}
