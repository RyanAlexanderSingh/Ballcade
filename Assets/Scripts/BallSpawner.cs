using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallSpawner : MonoBehaviour
{
	#region Serialized Fields

	[SerializeField]
	private Transform _spawnPoint;

	[SerializeField]
	private Transform _spawnerTurntable;

	[SerializeField]
	private Transform _cannonTransform;

	[SerializeField]
	private BallSpawnData _ballSpawnData;

	[SerializeField]
	private BallSpawnedEvent onBallSpawnedSpawnedEvent;

	private Vector3 _startingDefaultSpawnRotation;

	#endregion


	void Awake()
	{
		_startingDefaultSpawnRotation = _spawnPoint.localRotation.eulerAngles;
	}


	#region Spawn

	public void FireBall()
	{
		float ballSpawnRandomArc = _ballSpawnData._ballSpawnRandomArc;

		float defaultSpawnYRot = _startingDefaultSpawnRotation.y;

		float randomYRotation =
			Random.Range(defaultSpawnYRot - ballSpawnRandomArc, defaultSpawnYRot + ballSpawnRandomArc);

		Vector3 newSpawnRotationVector =
			new Vector3(_startingDefaultSpawnRotation.x, randomYRotation, _startingDefaultSpawnRotation.z);
		MoveSpawnerToRotation(newSpawnRotationVector);
	}

	private void SpawnBall()
	{
		GameObject pooledBall = App.objectPooler.GetPooledObject(PoolableObjects.Ball);
		pooledBall.SetActive(true);

		pooledBall.transform.SetPositionAndRotation(_spawnPoint.position, _spawnerTurntable.rotation);
		Ball ball = pooledBall.GetComponent<Ball>();
		ball.Initialise();

		onBallSpawnedSpawnedEvent.Raise(ball);
	}

	private void MoveSpawnerToRotation(Vector3 targetRotation)
	{
		// save the cannons initial rotation to return to after applying some faked vertical recoil from shooting the ball
		Vector3 initialCannonRotation = _cannonTransform.localRotation.eulerAngles;
		Vector3 verticalCannonRecoilRotation = new Vector3(-35f, initialCannonRotation.y, initialCannonRotation.z);

		Sequence sequence = DOTween.Sequence();
		sequence.Append(_spawnerTurntable.DOLocalRotate(targetRotation, 0.2f).OnComplete(SpawnBall));
		sequence.Append(_cannonTransform.DOLocalRotate(verticalCannonRecoilRotation, 0.15f));
		sequence.Append(_cannonTransform.DOLocalRotate(initialCannonRotation, 0.15f));
	}

	#endregion
}