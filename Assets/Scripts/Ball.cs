using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour, IPooledObject
{
    #region ScriptableObjects

    [SerializeField] private BallData _ballData;

    #endregion

    #region Vars

    [SerializeField] private Rigidbody _rigidbody;

    #endregion
    
    void FixedUpdate()
    {
        if (_rigidbody.velocity.magnitude < 5f)
        {
            Debug.Log("speed too slow, speeding it up");
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

    public void StopPhysics()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
}
