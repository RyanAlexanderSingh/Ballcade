using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPlayer : MonoBehaviour, IPlayer
{

    #region Vars

    [SerializeField] private PlayerData _playerData;

    #endregion
    

    #region Updates

    void Update()
    {
        UpdateMovement();
    }

    public void UpdateMovement()
    {
        float h = Input.GetAxisRaw ("Horizontal");

        if (h == 0f) 
            return;
        
        Vector3 dir = transform.right * h;
        transform.Translate(dir * Time.deltaTime * _playerData.playerSpeed, Space.World);

        Vector3 unClampedNewPos = transform.position;
        Vector3 clampedPos = unClampedNewPos;
        
        if (Mathf.Abs(transform.right.x) == 1f)
        {
            clampedPos = new Vector3(Mathf.Clamp(unClampedNewPos.x, _playerData.minMovePos, _playerData.maxMovePosition), unClampedNewPos.y, unClampedNewPos.z);
        }
        else
        {
            clampedPos = new Vector3(unClampedNewPos.x, unClampedNewPos.y, Mathf.Clamp(unClampedNewPos.z, _playerData.minMovePos, _playerData.maxMovePosition));
        }

        transform.position = clampedPos;
    }

    #endregion
    
}
