using System.Collections;
using UnityEngine;

namespace Ballcade
{
	public class Ball : MonoBehaviour, IPooledObject
	{
		#region ScriptableObjects

		[SerializeField] private BallData _ballData;

		#endregion


		#region Vars

		[SerializeField] private Rigidbody _rigidbody;

		[SerializeField] private TrailRenderer _trailRenderer;

		private bool _constrainYVelocity;

		public int PointsValueForGoal => _ballData.PointsValueForGoal;

		#endregion


		private void FixedUpdate()
		{
			EnsureVelocityMinimum();

			if (!_constrainYVelocity)
				return;

			var clampedYVelocity = _rigidbody.velocity;
			clampedYVelocity = new Vector3(clampedYVelocity.x, 0f, clampedYVelocity.z);
			_rigidbody.velocity = clampedYVelocity;
		}

		private void LateUpdate()
		{
			Vector3 rotationAxis = Vector3.Cross(_rigidbody.velocity.normalized, Vector3.up);
			Debug.DrawLine(transform.position, transform.position + rotationAxis, Color.magenta);

			transform.RotateAround(transform.position, rotationAxis,
				-Mathf.Sin(_rigidbody.velocity.magnitude * 0.5f * 2f * Mathf.PI) * Mathf.Rad2Deg);
		}

		private void EnsureVelocityMinimum()
		{
			if (_rigidbody.velocity.magnitude >= _ballData.MinimumVelocity)
				return;

			Vector2 v = _rigidbody.velocity;
			v = v.normalized;
			v *= _ballData.MinimumVelocity;
			_rigidbody.velocity = v;
		}

		public void OnObjectSpawned()
		{
			StopPhysics();
		}

		IEnumerator CoDelayConstrainSettings()
		{
			yield return new WaitForSeconds(2f);

			_constrainYVelocity = true;
		}

		public void Initialise()
		{
			_rigidbody.AddForce(_ballData.GetInitialSpawnForce(transform), ForceMode.Impulse);
			StartCoroutine(CoDelayConstrainSettings());
		}

		private void Deactivate()
		{
			StopPhysics();
			_trailRenderer.Clear();

			_rigidbody.constraints = RigidbodyConstraints.None;
		}

		public void Scored()
		{
			StartCoroutine(CoHandleScoredBallObj());
		}

		private IEnumerator CoHandleScoredBallObj()
		{
			yield return new WaitForSeconds(_ballData.DespawnDelayAfterGoal);

			ReturnBallToPool();
		}

		public void ReturnBallToPool()
		{
			Deactivate();

			App.instance.StartCoroutine(CoSpawnConfetti());

			App.ObjectPool.ReturnToPool(gameObject);
		}

		IEnumerator CoSpawnConfetti()
		{
			GameObject pooledConfetti = App.ObjectPool.GetPooledObject(PoolableObjects.DefaultConfetti);
			pooledConfetti.SetActive(true);

			pooledConfetti.transform.SetPositionAndRotation(transform.position, Quaternion.identity);

			yield return new WaitForSeconds(2f);

			App.ObjectPool.ReturnToPool(pooledConfetti);
		}

		private void StopPhysics()
		{
			_rigidbody.velocity = Vector3.zero;
			_rigidbody.angularVelocity = Vector3.zero;
			_constrainYVelocity = false;
		}

		private void OnEnable()
		{
			OnObjectSpawned();
		}
	}
}