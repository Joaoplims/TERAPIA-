using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InclinationAdjust : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float angleInclination;
    private float finalInclination;
    private Vector3 worldDirection;

    private void Start()
    {
        // convert angleInclination to radians
        angleInclination = angleInclination*Mathf.Deg2Rad;
        finalInclination = Mathf.Tan(angleInclination);
        worldDirection = new Vector3(finalInclination, 1f, 0f);
    }

    private void Update()
    {
        Vector3 world2localDir = transform.InverseTransformDirection(worldDirection);
        float AngX = Mathf.Atan2(world2localDir.x, 1f);
        float AngZ = Mathf.Atan2(world2localDir.z, 1f);
    
        // converts angle to degrees;
        AngX = AngX * Mathf.Rad2Deg;
        AngZ = AngZ * Mathf.Rad2Deg;

        Vector3 targetRot = transform.eulerAngles;
        
        Quaternion finalRot = Quaternion.Euler(new Vector3(targetRot.x + AngX, targetRot.y, targetRot.z + AngZ));
        target.rotation = finalRot;
    }   
}
