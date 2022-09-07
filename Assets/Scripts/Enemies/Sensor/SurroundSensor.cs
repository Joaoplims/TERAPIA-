using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class SurroundSensor :MonoBehaviour
{
    public Transform Target { get; private set; }

    [SerializeField] private float radius = 7f;

    private SphereCollider sphereCollider;
    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>( );
        sphereCollider.radius = radius;
        sphereCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Target = other.transform;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            Invoke("RemoveTargetAfter",1f);
    }

    private void RemoveTargetAfter()
    {
        Target = null;
    }

}
