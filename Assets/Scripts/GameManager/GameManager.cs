using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    #region Vars

    // This will come in dynamically but with only one, I'm just assigning it for now
    [SerializeField]
    private GameLevelData _defaultGameLevelData;

    // This will come in another way but testing level data
    [SerializeField]
    private List<CharacterVisualData> _characterVisualDatas = new List<CharacterVisualData>();

    [SerializeField]
    private AIPlayerMananger _aiPlayerMananger;

    [Header("UI")]
    [SerializeField]
    private Canvas _canvas;

    [SerializeField]
    private ScoreboardUI _scoreboardUIPrefab;

    [SerializeField]
    private PlayerController _playerControllerPrefab;
    
    [SerializeField]
    private PlayerController _aiControllerPrefab;

    private List<Transform> _activeBallsInScene = new List<Transform>();

    private ScoreboardUI _scoreboardUI;

    private Dictionary<int, CharacterVisualData> _players = new Dictionary<int, CharacterVisualData>();

    private Level _activeLevel;

    #endregion

    #region Initialise

    void Start()
    {
        CreateLevelAndPlayers();

        CreateUI();
    }

    private void CreateLevelAndPlayers()
    {
        SceneManager.LoadScene(_defaultGameLevelData.LevelScene.ToString(), LoadSceneMode.Additive);

        _activeLevel = Instantiate(_defaultGameLevelData.Level, transform);
        _activeLevel.Setup(_defaultGameLevelData);

        CreateOpponents();
    }

    private void CreateOpponents()
    {
        // create the player first
        var playerController = Instantiate(_playerControllerPrefab, _activeLevel.PlayerStartingPosition, false);
        SetupOpponent(playerController);

        List<AIPlayerController> aiPlayerControllers = new List<AIPlayerController>();
        for (int i = _defaultGameLevelData.NumOfAIOpponents - 1; i >= 0; i--)
        {
            var aiController = Instantiate(_aiControllerPrefab, _activeLevel.AIStartingPositions[i], false) as AIPlayerController;
            SetupOpponent(aiController);
            aiPlayerControllers.Add(aiController);
        }

        _aiPlayerMananger.Initialise(aiPlayerControllers);
    }

    private void SetupOpponent(PlayerController playerController)
    {
       var randomData = _characterVisualDatas[Random.Range(0, _characterVisualDatas.Count - 1)];
       playerController.SetPlayerVisualData(randomData);
    }


    private void CreateUI()
    {
        _scoreboardUI = Instantiate(_scoreboardUIPrefab, _canvas.transform);

//        _scoreboardUI.SetupScoreboard();
    }

    #endregion


    #region Listeners

    public void OnGoalColliderEventHandler(GameObject collisionObj)
    {
        Ball ball = collisionObj.GetComponent<Ball>();

        if (ball == null)
            return;

        // remove the ball from active balls list for the ai to consider immediately
        _activeBallsInScene.Remove(ball.transform);
        _aiPlayerMananger.UpdateActiveBallsForAI(_activeBallsInScene);

        ball.Scored();
    }

    public void OnBallSpawnedEventHandler(GameObject ballGo)
    {
        _activeBallsInScene.Add(ballGo.transform);
        _aiPlayerMananger.UpdateActiveBallsForAI(_activeBallsInScene);
    }

    #endregion
}