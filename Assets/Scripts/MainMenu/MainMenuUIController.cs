using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ballcade
{
    public class MainMenuUIController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _battleButton;

        public Action OnBattleButtonClicked;
        
        public void SetUIVisibility(bool visible)
        {
            if (visible)
            {
                _canvasGroup.ShowImmediate();
            }
            else
            {
                _canvasGroup.HideImmediate();
            }
        }
    
        protected void OnEnable()
        {
            _battleButton.onClick.AddListener(OnBattleClicked);
        }

        protected void OnDisable()
        {
            _battleButton.onClick.RemoveListener(OnBattleClicked);
        }

        private void OnBattleClicked()
        {
            OnBattleButtonClicked?.Invoke();
        }
    }
}
