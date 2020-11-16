using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BallData", menuName = "Ballcade/Ball Data")]
public class BallData : ScriptableObject
{
    #region Vars

    [SerializeField] private float _initalInpulsePower = 10f;
    [SerializeField] private int _pointValue = 1;
    [SerializeField] private float _despawnDelayAfterGoal = 0.5f;

    #endregion

    public int GetPointsValueForGoal()
    {
        return _pointValue;
    }

    public float GetDespawnDelayAfterBeingScored()
    {
        return _despawnDelayAfterGoal;
    }
    

    #region Physics

    public Vector3 GetInitialSpawnForce(Transform ballTransform)
    {
        return ballTransform.transform.forward * _initalInpulsePower;
    }

    #endregion
}