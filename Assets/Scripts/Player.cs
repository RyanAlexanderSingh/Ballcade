using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float maxZLimit = 7.5f;
    private float minZLimit = -7.5f;

    [SerializeField] private float zMoveAmount = 5f;
    
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKey("a"))
        {
            var zPos = transform.position.z + zMoveAmount * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(zPos,minZLimit, maxZLimit));
        }
        if (Input.GetKey("d"))
        {
            var zPos = transform.position.z + -zMoveAmount * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(zPos,minZLimit, maxZLimit));
        }
    }
}
