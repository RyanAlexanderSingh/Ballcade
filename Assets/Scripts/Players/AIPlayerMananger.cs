using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerMananger : MonoBehaviour
{
    private List<AIPlayerController> _aiPlayers = new List<AIPlayerController>();

    public void Initialise(List<AIPlayerController> aiPlayers)
    {
        _aiPlayers = aiPlayers;
    }

    public void UpdateActiveBallsForAI(List<Transform> activeBalls)
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
}
