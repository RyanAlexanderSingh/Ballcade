using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ballcade
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private MainMenuUIController _mainMenuUIController;
        [SerializeField] private VoidEvent.VoidEvent _onBattleButtonPressedEvent;

        protected void OnEnable()
        {
            _mainMenuUIController.OnBattleButtonClicked += StartBattle;
        }

        protected void OnDisable()
        {
            _mainMenuUIController.OnBattleButtonClicked += StartBattle;
        }

        private void StartBattle()
        {
            _mainMenuUIController.SetUIVisibility(false);
            App.instance.LoadScene("AppCore", LoadSceneMode.Additive);
        }
    }
}
