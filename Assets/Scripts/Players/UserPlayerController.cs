using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPlayerController : PlayerController, IPlayer
{

    #region Updates

    void Update()
    {
        UpdateMovement();
    }

    public void UpdateMovement()
    {
        if (!CanMove)
            return;
        
        float h = Input.GetAxisRaw ("Horizontal");

        Vector3 dir = transform.right * h;
        int dirXNorm = (int)dir.normalized.x;
        SetAnimBasedOnMovementDir(dir.normalized.x);
        
        if (dirXNorm == 0)
            return;
        
        transform.Translate(dir * Time.deltaTime * _playerData.playerSpeed, Space.World);

        transform.position = GetClampedTargetPosition(transform.position);
    }

    #endregion
}
