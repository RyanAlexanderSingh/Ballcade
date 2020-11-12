using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCollider : MonoBehaviour
{
    #region Vars

    [SerializeField] private BoxCollider _collider;

    #endregion


    #region Events

    public event Action<GameObject> OnCollisionEnterEvent;

    #endregion

    #region Initialise


    #endregion


    #region Collisions

    private void OnTriggerEnter(Collider other)
    {
        OnCollisionEnterEvent?.Invoke(other.gameObject);
    }

    #endregion
}
