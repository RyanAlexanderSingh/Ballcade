using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ballcade
{
    public class PlayerUI : MonoBehaviour
    {
        #region Vars

        [SerializeField]
        private TextMeshProUGUI _playerScore;

        [SerializeField]
        private Image _playerImage;

        [SerializeField]
        private DOTweenAnimation _goalConceededAnimation;


        public int PlayerId { get; private set; }

        #endregion

        #region Initialise

        public void Setup(int playerId, int startingScore, Sprite playerSprite)
        {
            PlayerId = playerId;

            SetPlayerScoreText(startingScore);
            _playerImage.sprite = playerSprite;
        }

        #endregion


        #region Player Score

        private void SetPlayerScoreText(int score)
        {
            _playerScore.text = score.ToString().PadLeft(2, '0');
        }

        public void SetPlayerLives(int currentLives)
        {
            SetPlayerScoreText(currentLives);

            _goalConceededAnimation.DORestart();
        }

        #endregion
    }
}