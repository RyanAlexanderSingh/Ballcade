using System;
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
        
//        float h = Input.GetAxisRaw ("Horizontal");
        
//        Move(h);
    }


    #endregion

    #region Movement

    private void Move(float axis)
    {
        Vector3 dir = transform.right * axis;
        int dirXNorm = (int)dir.normalized.x;
        
        Debug.Log(dirXNorm);
        SetAnimBasedOnMovementDir(dirXNorm);
        
        if (dirXNorm == 0)
            return;
        
        transform.Translate(dir * Time.deltaTime * _playerData.playerSpeed, Space.World);

        transform.position = GetClampedTargetPosition(transform.position);
    }

    #endregion

    #region Listeners

    public void OnScreenTouchedEvent(ScreenTouchedData screenTouchedData)
    {
        float axis;
        switch (screenTouchedData.screenTouchSide)
        {
            case MovementInput.eScreenTouchSide.None:
                axis = 0f;
                break;
            case MovementInput.eScreenTouchSide.Left:
                axis = -1f;
                break;
            case MovementInput.eScreenTouchSide.Right:
                axis = 1f;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        Move(axis);
    }

    #endregion
}
