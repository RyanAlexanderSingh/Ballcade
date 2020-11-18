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

    private Dictionary<int, Sprite> _players = new Dictionary<int, Sprite>();

    private Level _activeLevel;

    private Dictionary<int, CharacterVisualData> _uniqueCharacterDataDict = new Dictionary<int, CharacterVisualData>();

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
        // assign random idx values to a dictionary so each player has a different skin
        List<CharacterVisualData> _randomCharacterVisualDatas = _characterVisualDatas;
        _randomCharacterVisualDatas.Shuffle();

        for (int i = 0; i < _randomCharacterVisualDatas.Count; i++)
        {
            _uniqueCharacterDataDict[i] = _randomCharacterVisualDatas[i];
        }

        int opponentIdx = 0;
        // create the player first
        CreateAndSetupOpponent(_playerControllerPrefab, _activeLevel.UserPlayerSection.StartingPosition, opponentIdx);
        _activeLevel.UserPlayerSection.Setup(opponentIdx);

        List<AIPlayerController> aiPlayerControllers = new List<AIPlayerController>();
        for (int i = 0; i < _defaultGameLevelData.NumOfAIOpponents; i++)
        {
            // increment opponent idx first as player is default 0
            ++opponentIdx;

            var AISection = _activeLevel.AISections[i];
            var aiController =
                CreateAndSetupOpponent(_aiControllerPrefab, AISection.StartingPosition, opponentIdx) as
                    AIPlayerController;
            aiPlayerControllers.Add(aiController);
            AISection.Setup(opponentIdx);
        }

        _aiPlayerMananger.Initialise(aiPlayerControllers);
    }

    private PlayerController CreateAndSetupOpponent(PlayerController playerControllerPrefab,
        Transform playerParentTransform, int idx)
    {
        var playerController = Instantiate(playerControllerPrefab, playerParentTransform, false);

        _uniqueCharacterDataDict.TryGetValue(idx, out var visualData);
        if (visualData == null)
            return null;

        playerController.SetPlayerVisualData(visualData);

        _players[idx] = visualData.CharacterPortraitSprite;

        return playerController;
    }

    private void CreateUI()
    {
        _scoreboardUI = Instantiate(_scoreboardUIPrefab, _canvas.transform);

        _scoreboardUI.SetupScoreboard(_players, _defaultGameLevelData.NumberOfStartingLives);
    }

    #endregion


    #region Listeners

    public void OnGoalColliderEventHandler(BallScoredData ballScoredData)
    {
        Ball ball = ballScoredData.ball;

        // remove the ball from active balls list for the ai to consider immediately
        _activeBallsInScene.Remove(ball.transform);
        _aiPlayerMananger.UpdateActiveBallsForAI(_activeBallsInScene);

        ball.Scored();

        _scoreboardUI.DeductPlayerScore(ballScoredData.playerIdx, ball.PointsValueForGoal);
    }

    public void OnBallSpawnedEventHandler(GameObject ballGo)
    {
        _activeBallsInScene.Add(ballGo.transform);
        _aiPlayerMananger.UpdateActiveBallsForAI(_activeBallsInScene);
    }

    #endregion
}