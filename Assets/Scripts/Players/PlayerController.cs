using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Vars

    [SerializeField] 
    protected PlayerData _playerData;

    [SerializeField]
    private MeshRenderer _puckMeshRenderer;
    
    [SerializeField]
    private MeshRenderer _characterMeshRenderer;

    [SerializeField]
    private PlayerDiedEvent _playerDiedEvent;

    #endregion
    
    public int PlayerIdx { get; private set; }

    protected bool IsPlayerAlive => CurrentLives > 0;

    public int CurrentLives { get; private set; }
    
    public CharacterVisualData VisualData { get; private set; }

    public void SetPlayerData(CharacterVisualData characterVisualData, int startingLives, int playerIdx)
    {
        PlayerIdx = playerIdx;
        CurrentLives = startingLives;
        
        VisualData = characterVisualData;
        _puckMeshRenderer.material = VisualData.CharacterMaterial;
        _characterMeshRenderer.material = VisualData.PuckMaterial;
    }

    protected Vector3 GetClampedTargetPosition(Vector3 targetPosition)
    {
        Vector3 currentPosition = transform.position;
        Vector3 clampedPos;
        
        if (Mathf.Abs(transform.right.x) == 1f)
        {
            clampedPos = new Vector3(Mathf.Clamp(targetPosition.x, _playerData.minMovePos, _playerData.maxMovePosition), currentPosition.y, currentPosition.z);
        }
        else
        {
            clampedPos = new Vector3(currentPosition.x, currentPosition.y, Mathf.Clamp(targetPosition.z, _playerData.minMovePos, _playerData.maxMovePosition));
        }

        return clampedPos;
    }

    public void ReducePlayerLives(int reduction)
    {
        if (!IsPlayerAlive)
            return;
        
        CurrentLives -= reduction;

        if (CurrentLives == 0)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        gameObject.SetActive(false);

        PlayerDiedData playerDiedData = new PlayerDiedData
        {
            playerController = this
        };
        _playerDiedEvent.Raise(playerDiedData);
    }
    
}
