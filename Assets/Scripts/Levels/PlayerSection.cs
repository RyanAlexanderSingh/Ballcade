using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSection : MonoBehaviour
{
    #region Vars

    [SerializeField]
    private GoalCollider _goalCollider;

    [SerializeField]
    private Transform _startingPosition;

    public Transform StartingPosition => _startingPosition;

    #endregion

    #region Initialise

    public void Setup(int goalOwnerPlayerIdx)
    {
        _goalCollider.Setup(goalOwnerPlayerIdx);
    }

    #endregion

}
