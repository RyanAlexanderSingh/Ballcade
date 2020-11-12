using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region ScriptableObjects

    [SerializeField] private BallData _ballData;

    #endregion

    #region Vars

    [SerializeField] private Rigidbody _rigidbody;

    #endregion


    public void ApplySpawnForce()
    {
        _rigidbody.AddForce(_ballData.GetInitialSpawnForce(transform), ForceMode.Impulse);
    }
}
