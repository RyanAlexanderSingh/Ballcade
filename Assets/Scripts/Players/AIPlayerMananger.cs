using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerMananger : MonoBehaviour
{
    #region Vars

    private List<AIPlayerController> _aiPlayers = new List<AIPlayerController>();

    #endregion
    

    #region Initialise

    public void Initialise(List<AIPlayerController> aiPlayers)
    {
        _aiPlayers = aiPlayers;
    }

    #endregion
    

    #region Update

    public void UpdateActiveBalls(List<Transform> activeBalls)
    {
        foreach (AIPlayerController aiPlayer in _aiPlayers)
        {
            aiPlayer.UpdateActiveBallsList(activeBalls);
        }
    }

    public void Update()
    {
        foreach (AIPlayerController aiPlayer in _aiPlayers)
        {
            aiPlayer.ManualUpdate();
        }
    }

    #endregion

}
