using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSection : MonoBehaviour
{
    #region Vars

    [SerializeField]
    private bool _isPlayerSection;

    [SerializeField]
    private GoalCollider _goalCollider;

    [SerializeField]
    private Transform _startingPosition;

    [SerializeField]
    private GameObject _sectionBarrier;

    public Transform StartingPosition => _startingPosition;

    public bool IsPlayerSection => _isPlayerSection;
    
    public bool IsSectionOccupied { get; private set; }

    private int _goalOwnerPlayerIdx;

    #endregion

    #region Initialise

    private void Awake()
    {
        SetBarrierState(false);
    }

    public void Setup(int goalOwnerPlayerIdx)
    {
        _goalOwnerPlayerIdx = goalOwnerPlayerIdx;
        _goalCollider.Setup(_goalOwnerPlayerIdx);
        IsSectionOccupied = true;
    }

    #endregion

    #region Section

    /*
     * This will typically be called when a player has died and no more balls should enter the section
     */

    public void OnPlayerDied(PlayerDiedData playerDiedData)
    {
        if (_goalOwnerPlayerIdx == playerDiedData.playerController.PlayerIdx)
        {
            SetBarrierState(true);
        }
    }
    
    public void SetBarrierState(bool setActive)
    {
        _sectionBarrier.SetActive(setActive);
    }

    #endregion

}
