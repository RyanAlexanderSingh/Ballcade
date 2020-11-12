using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
   [SerializeField] private Transform _spawnPoint;

   [SerializeField] private Ball _ballPrefab;

   public bool spawnBall;
   public float power;

   private void Update()
   {
      if (spawnBall)
      {
         spawnBall = false;
         SpawnBall();
      }
   }
   
   private void SpawnBall()
   {
      Ball ball = Instantiate(_ballPrefab, _spawnPoint);
      ball.ApplySpawnForce();
   }
   
}
