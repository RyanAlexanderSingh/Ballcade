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
        if (Input.GetKey("a"))
        {
            var xPos = transform.position.x + -_playerData.playerSpeed * Time.deltaTime;
            transform.position = new Vector3(Mathf.Clamp(xPos, minXLimit, maxXLimit), transform.position.y, transform.position.z);
        }
        if (Input.GetKey("d"))
        {
            var xPos = transform.position.x + _playerData.playerSpeed * Time.deltaTime;
            transform.position = new Vector3(Mathf.Clamp(xPos, minXLimit, maxXLimit), transform.position.y, transform.position.z);

        }
    }

    #endregion
   
}
