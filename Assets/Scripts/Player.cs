using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region Vars


    [SerializeField] private PlayerData _playerData;

    #endregion
    

    #region Updates

    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        float h = Input.GetAxisRaw ("Horizontal");

        if (h == 0f) 
            return;

        Vector3 dir = transform.right * h;
        transform.Translate(dir * Time.deltaTime * _playerData.playerSpeed);

        Vector3 unClampedNewPos = transform.position;
        transform.position = new Vector3(Mathf.Clamp(unClampedNewPos.x, _playerData.minXLimit, _playerData.maxXLimit), unClampedNewPos.y, unClampedNewPos.z);
    }

    #endregion
    
}
