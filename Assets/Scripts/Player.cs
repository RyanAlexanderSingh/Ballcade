using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region Vars

    private float maxZLimit = 7.5f;
    private float minZLimit = -7.5f;

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
            var zPos = transform.position.z + _playerData.playerSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(zPos,minZLimit, maxZLimit));
        }
        if (Input.GetKey("d"))
        {
            var zPos = transform.position.z + -_playerData.playerSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(zPos,minZLimit, maxZLimit));
        }
    }

    #endregion
   
}
