using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BallData", menuName = "Ballcade/Ball Data")]
public class BallData : ScriptableObject
{
    #region Vars

    [Header("Physics")]
    [SerializeField] private float _initalInpulsePower = 10f;
    [SerializeField] private float _minimumVelocity = 6f;
    [SerializeField] private int _pointValue = 1;
    [SerializeField] private float _despawnDelayAfterGoal = 0.5f;

    public int PointsValueForGoal => _pointValue;

    public float DespawnDelayAfterGoal => _despawnDelayAfterGoal;

    public float MinimumVelocity => _minimumVelocity;

    #endregion

    #region Physics

    public Vector3 GetInitialSpawnForce(Transform ballTransform)
    {
        return ballTransform.transform.forward * _initalInpulsePower;
    }

    #endregion
}