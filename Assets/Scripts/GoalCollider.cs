using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCollider : MonoBehaviour
{
    #region Vars

    [SerializeField] private BoxCollider _collider;
    
    [SerializeField] private GameEvent OnGoalScoredEvent;

    #endregion


    #region Collisions

    private void OnTriggerEnter(Collider other)
    {
        OnGoalScoredEvent.Raise(other.gameObject);
    }

    #endregion
}
