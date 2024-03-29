﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ballcade
{
    public class GameManager : MonoBehaviour
    {
        
        [Header("Dependencies")]
        // This will come in dynamically but with only one, I'm just assigning it for now
        [SerializeField] private GameLevelData _defaultGameLevelData;

        // This will come in another way but testing level data
        [SerializeField] private List<CharacterVisualData> _characterVisualDatas = new();
        [SerializeField] private Canvas _canvas;
        [SerializeField] private AIPlayerMananger _aiPlayerManager;

        [Header("Prefabs")]
        [SerializeField] private ScoreboardUI _scoreboardUIPrefab;
        [SerializeField] private PlayerController _playerControllerPrefab;
        [SerializeField] private PlayerController _aiControllerPrefab;

        private ScoreboardUI _scoreboardUI;

        private Dictionary<int, CharacterVisualData> _uniqueCharacterDataDict = new();

        private Level _activeLevel;

        private List<PlayerController> _playerControllers = new();
        private List<Ball> _activeBallsInScene = new();

        public void OnGoalColliderEventHandler(BallScoredData ballScoredData)
        {
            Ball ball = ballScoredData.ball;

            // remove the ball from active balls list for the ai to consider immediately
            _activeBallsInScene.Remove(ball);
            UpdateActiveBalls();

            ball.Scored();

            // update player lives left for both player controller & ui elements
            var goalOwner = _playerControllers.FirstOrDefault(x => x.PlayerIdx == ballScoredData.playerGoalIdx);
            if (goalOwner != null)
            {
                goalOwner.ReducePlayerLives(ball.PointsValueForGoal);
                _scoreboardUI.SetPlayerLives(ballScoredData.playerGoalIdx, goalOwner.CurrentLives);
            }
        }

        public void OnPlayerDied(PlayerDiedData playerDiedData)
        {
            List<PlayerController> alivePlayerControllers = _playerControllers.Where(x => x.IsPlayerAlive).ToList();
            if (alivePlayerControllers.Count > 1)
                return;

            // if there is only one player left, they're the winner and game should stop
            if (alivePlayerControllers.Count == 1)
            {
                FinishGame(alivePlayerControllers[0]);
            }
        }

        public void OnBallSpawnedEventHandler(Ball ball)
        {
            _activeBallsInScene.Add(ball);
            UpdateActiveBalls();
        }

        public void StartGame()
        {
            if (_activeLevel == null)
                SceneManager.LoadScene(_defaultGameLevelData.LevelScene.ToString(), LoadSceneMode.Additive);

            CreateLevelAndPlayers();
            LoadUI();
        }

        protected void Start()
        {
            StartGame();
        }

        private void UpdateActiveBalls()
        {
            _aiPlayerManager.UpdateActiveBalls(_activeBallsInScene);
            _activeLevel.UpdateActiveBalls(_activeBallsInScene.Count);
        }

        private void CreateLevelAndPlayers()
        {
            _activeLevel = Instantiate(_defaultGameLevelData.Level, transform);
            _activeLevel.Setup(_defaultGameLevelData);

            CreateOpponents();
        }

        private void CreateOpponents()
        {
            // assign random idx values to a dictionary so each player has a different skin
            List<CharacterVisualData> randomCharacterVisualDatas = _characterVisualDatas;
            randomCharacterVisualDatas.Shuffle();

            for (int i = 0; i < randomCharacterVisualDatas.Count; i++)
            {
                _uniqueCharacterDataDict[i] = randomCharacterVisualDatas[i];
            }

            int opponentIdx = 0;
            // create the player first
            var playerSection = _activeLevel.GetPlayerSection();

            var playerController =
                CreateAndSetupOpponent(_playerControllerPrefab, playerSection.StartingPosition, opponentIdx);
            _playerControllers.Add(playerController);

            playerSection.Setup(opponentIdx);

            List<AIPlayerController> aiPlayerControllers = new List<AIPlayerController>();
            for (int i = 0; i < _defaultGameLevelData.NumOfAIOpponents; i++)
            {
                // increment opponent idx first as player is default 0
                ++opponentIdx;

                var aiSection = _activeLevel.GetFreePlayerSection();
                var aiController =
                    CreateAndSetupOpponent(_aiControllerPrefab, aiSection.StartingPosition, opponentIdx) as
                        AIPlayerController;
                aiPlayerControllers.Add(aiController);
                aiSection.Setup(opponentIdx);
            }

            _playerControllers.AddRange(aiPlayerControllers);

            _aiPlayerManager.Initialise(aiPlayerControllers);
        }

        private PlayerController CreateAndSetupOpponent(PlayerController playerControllerPrefab,
            Transform playerParentTransform, int idx)
        {
            var playerController = Instantiate(playerControllerPrefab, playerParentTransform, false);

            _uniqueCharacterDataDict.TryGetValue(idx, out var visualData);
            if (visualData == null)
                return null;

            playerController.SetPlayerData(visualData, _defaultGameLevelData.NumberOfStartingLives, idx);

            return playerController;
        }

        private void LoadUI()
        {
            if (_scoreboardUI == null)
            {
                _scoreboardUI = Instantiate(_scoreboardUIPrefab, _canvas.transform);
                _scoreboardUI.transform.SetAsFirstSibling();
            }

            _scoreboardUI.SetupScoreboard(_playerControllers);
        }

        private void FinishGame(PlayerController winnerPlayerController)
        {
            winnerPlayerController.PlayerWon();

            PlayWinningSequence(winnerPlayerController);

            // clear up all the existing balls in the scene
            foreach (var ball in _activeBallsInScene)
            {
                ball.ReturnBallToPool();
            }

            _activeBallsInScene.Clear();

            _activeLevel.FinishGame();

            // stop the ai running updates for now, there is no point
            _aiPlayerManager.SetUpdateState(false);

            StartCoroutine(Co_DelayNewGame());
        }

        private IEnumerator Co_DelayNewGame()
        {
            yield return new WaitForSeconds(2f);

            ClearData();

            RestartGame();
        }

        private void RestartGame()
        {
            StartGame();
        }

        private void ClearData()
        {
            _uniqueCharacterDataDict.Clear();

            _playerControllers.Clear();

            Destroy(_activeLevel.gameObject);
        }

        private void PlayWinningSequence(PlayerController winnerPlayerController)
        {
            Transform winnerPlayerControllerTransform = winnerPlayerController.transform;

            Sequence winningSequence = DOTween.Sequence();
            Vector3 targetPosition =
                winnerPlayerControllerTransform.position + winnerPlayerControllerTransform.forward * 7.5f;
            winningSequence.Append(winnerPlayerControllerTransform.DOMove(targetPosition, 1.25f));
        }
    }
}