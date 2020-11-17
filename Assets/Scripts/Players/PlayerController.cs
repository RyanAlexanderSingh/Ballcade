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

    #endregion

    public void SetPlayerVisualData(CharacterVisualData characterVisualData)
    {
        _puckMeshRenderer.material = characterVisualData.CharacterMaterial;
        _characterMeshRenderer.material = characterVisualData.PuckMaterial;
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
}
