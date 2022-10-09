using System.Collections.Generic;
using UnityEngine;

namespace Ballcade
{
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

        public void SetupScoreboard(List<PlayerController> players)
        {
            for(int i = 0; i < players.Count; i++)
            {
                var player = players[i];
                int playerKey = player.PlayerIdx;
                //will probably contain this better but just here for now
                Transform parent = (playerKey == 0 || playerKey == 1) ? _leftContainer : _rightContainer;

                PlayerUI playerUI;
                if (i < _playerUIs.Count)
                {
                    playerUI = _playerUIs[i];
                }
                else
                {
                    playerUI = Instantiate(_playerUIPrefab, parent);    
                }
            
                playerUI.Setup(playerKey, player.CurrentLives, player.VisualData.CharacterPortraitSprite);

                _playerUIs.Add(playerUI);
            }
        }

        #endregion

        #region Update

        public void SetPlayerLives(int playerID, int currentLives)
        {
            foreach (var playerUI in _playerUIs)
            {
                if (playerUI.PlayerId != playerID) 
                    continue;
            
                playerUI.SetPlayerLives(currentLives);
                break;
            }
        }

        #endregion
    }
}
