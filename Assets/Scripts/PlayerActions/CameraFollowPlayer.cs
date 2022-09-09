using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer :MonoBehaviour
{
    public bool lerp = true;
    public Transform Target;
    public Vector3 Offset;
    public float followSpeed;

    private bool stopTracking = false;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void LateUpdate()
    {
        if (stopTracking == true)
            return;

        if (lerp)
            transform.position = Vector3.Lerp(transform.position , Target.position + Offset , Time.deltaTime * followSpeed);
        else
        {
            transform.position = Target.position + Offset;
        }
    }

    public void FocusOn(Transform target)
    {
        Target = target;
        this.Invoke(() => { Target = player; } , 3f);

    }
}
