using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    #region Vars

    [SerializeField] private List<BallSpawner> _ballSpawners = new List<BallSpawner>();
    [SerializeField] private float _spawnDelay;
    [SerializeField] private AIPlayerMananger _aiPlayerMananger;

    private List<Transform> _activeBallsInScene = new List<Transform>();

    #endregion

    #region Consts

    private const string _kArenaAdditiveScene = "Arena_01";

    #endregion


    #region Initialise

    void Start()
    {
        SceneManager.LoadScene(_kArenaAdditiveScene, LoadSceneMode.Additive);
        
        StartCoroutine(CoSpawnBalls());
    }

    #endregion


    #region Coroutines

    private IEnumerator CoSpawnBalls()
    {
        int numSpawners = _ballSpawners.Count;

        while (true)
        {
            yield return new WaitForSeconds(_spawnDelay);

            int randomIdx = Random.Range(0, numSpawners);
            _ballSpawners[randomIdx].SpawnBall();
        }
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