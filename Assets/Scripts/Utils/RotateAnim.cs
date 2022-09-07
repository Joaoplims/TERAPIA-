using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnim :MonoBehaviour
{
    [SerializeField] private Vector3 axis;
    [SerializeField] private float rotationSpeed;  



    void Update()
    {
        transform.Rotate(axis * rotationSpeed * Time.deltaTime);
    }
}
