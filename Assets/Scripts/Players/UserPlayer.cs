using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPlayer : Player, IPlayer
{
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

        transform.position = GetClampedTargetPosition(transform.position);
    }

    #endregion
    
}
