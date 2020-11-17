using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerMananger : MonoBehaviour
{
    private List<AiPlayerController> _aiPlayers = new List<AiPlayerController>();

    public void Initialise(List<AiPlayerController> aiPlayers)
    {
        _aiPlayers = aiPlayers;
    }

    public void UpdateActiveBallsForAI(List<Transform> activeBalls)
    {
        foreach (AiPlayerController aiPlayer in _aiPlayers)
        {
            aiPlayer.UpdateActiveBallsList(activeBalls);
        }
    }

    public void Update()
    {
        foreach (AiPlayerController aiPlayer in _aiPlayers)
        {
            aiPlayer.ManualUpdate();
        }
    }
}
