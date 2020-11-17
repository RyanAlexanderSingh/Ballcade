using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardUI : MonoBehaviour
{
    #region Vars

    [SerializeField] 
    private PlayerUI _playerUIPrefab;

    [SerializeField]
    private Transform _leftContainer;
    
    [SerializeField]
    private Transform _rightContainer;
    
    private List<PlayerUI> _playerUIs = new List<PlayerUI>();

    #endregion

    #region Initialise

    public void CreateScoreboard(Dictionary<int, Sprite> players, int startingScore)
    {
        foreach (var player in players)
        {
            int playerKey = player.Key;
            //will probably contain this better but just here for now
            Transform parent = (playerKey == 0 || playerKey == 1) ? _leftContainer : _rightContainer;
            var playerUI = Instantiate(_playerUIPrefab, parent);
            playerUI.Setup(playerKey, startingScore, player.Value);

            _playerUIs.Add(playerUI);
        }
    }

    #endregion

    #region Update

    public void DeductPlayerScore(int playerID, int deduction)
    {
        foreach (var playerUI in _playerUIs)
        {
            if (playerUI.PlayerId != playerID) 
                continue;
            
            playerUI.GoalScoredAgainstPlayer(deduction);
            break;
        }
    }

    #endregion
}
