using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public bool lerp = true;
    public Transform Target;
    public Vector3 Offset;
    public float followSpeed;


    private void LateUpdate()
    {
        if(lerp)
            transform.position = Vector3.Lerp(transform.position, Target.position + Offset, Time.deltaTime * followSpeed);
        else
        {
            transform.position = Target.position + Offset;
        }
    }
}
