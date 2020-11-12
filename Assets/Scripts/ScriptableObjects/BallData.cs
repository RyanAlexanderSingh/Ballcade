using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BallData", menuName = "Ballcade/Ball Data")]
public class BallData : ScriptableObject
{
    #region Vars

    [SerializeField] private float _initalInpulsePower = 10f;

    #endregion


    #region Physics

    public Vector3 GetInitialSpawnForce(Transform ballTransform)
    {
        return ballTransform.transform.forward * _initalInpulsePower;
    }

    #endregion
}