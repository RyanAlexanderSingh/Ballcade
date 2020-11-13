using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region Vars

    private float maxXLimit = 7.5f;
    private float minXLimit = -7.5f;

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

        Vector3 currentPosition = transform.position;
        
        if (h == 0f) 
            return;
        
        float movementSpeed = currentPosition.x + (h * _playerData.playerSpeed * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(movementSpeed, minXLimit, maxXLimit), currentPosition.y, currentPosition.z);
    }

    #endregion
    
}
