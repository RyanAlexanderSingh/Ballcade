using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour, IPooledObject
{
    #region ScriptableObjects

    [SerializeField] private BallData _ballData;

    #endregion
    

    #region Vars

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private TrailRenderer _trailRenderer;

    #endregion
    
    void FixedUpdate()
    {
        EnsureVelocityMinimum();
    }

    private void EnsureVelocityMinimum()
    {
        if (_rigidbody.velocity.magnitude < 5f)
        {
            Vector2 v = _rigidbody.velocity;
            v = v.normalized;
            v *= 10f;
            _rigidbody.velocity = v;
        }
    }

    public void OnObjectSpawned()
    {
        StopPhysics();
    }

    public void ApplySpawnForce()
    {
        _rigidbody.AddForce(_ballData.GetInitialSpawnForce(transform), ForceMode.Impulse);
    }

    private void Deactivate()
    {
        StopPhysics();
        _trailRenderer.Clear();
    }

    public void Scored()
    {
        StartCoroutine(CoHandleScoredBallObj());
    }
    
    private IEnumerator CoHandleScoredBallObj()
    {
        yield return new WaitForSeconds(_ballData.GetDespawnDelayAfterBeingScored());
        
        Deactivate();

        ObjectPoolManager.instance.ReturnToPool(gameObject);
    }

    private void StopPhysics()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
}
