using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(BoxCollider))]
public class Trigger :MonoBehaviour
{
    [SerializeField] private string CollisionTag = "Player";

    public UnityEvent OnEnter;
    public UnityEvent OnStay;
    public UnityEvent OnExit;

    private void Start()
    {
        GetComponent<BoxCollider>( ).isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CollisionTag))
        {
            OnEnter?.Invoke( );
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(CollisionTag))
        {
            OnStay?.Invoke( );
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(CollisionTag))
        {
            OnExit?.Invoke( );
        }
    }

}
