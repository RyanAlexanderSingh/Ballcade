using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerMananger : MonoBehaviour
{
    public List<AIPlayer> _aiPlayers = new List<AIPlayer>();

    public void Initialise()
    {
        
    }

    public void UpdateActiveBallsForAI(List<Transform> activeBalls)
    {
        foreach (AIPlayer aiPlayer in _aiPlayers)
        {
            aiPlayer.UpdateActiveBallsList(activeBalls);
        }
    }

    public void Update()
    {
        foreach (AIPlayer aiPlayer in _aiPlayers)
        {
            aiPlayer.ManualUpdate();
        }
    }
}
