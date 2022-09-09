using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider))]
public abstract class InteractableObject :MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject focalPointMarker = null;
    [Space]
    public UnityEvent OnInteract;
    [Space]
    public UnityEvent OnEnterInteractableArea;
    public UnityEvent OnStayInteractableArea;
    public UnityEvent OnExitInteractableArea;

    protected bool blockInteraction = false;
    protected Collider col;
    protected bool enableInteraction = false;
    private void Awake()
    {
        col = GetComponent<Collider>( );
        col.isTrigger = true;
    }
    public abstract void Interact();

    protected void Update()
    {
        if (enableInteraction == true)
        {
            if (blockInteraction == false && Keyboard.current.eKey.wasPressedThisFrame == true)
            {
                blockInteraction = true;
                Interact( );
                OnInteract?.Invoke( );
            }
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            focalPointMarker.SetActive(true);
            OnEnterInteractableArea?.Invoke( );
            enableInteraction = true;
        }
    }
    public virtual void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            OnStayInteractableArea?.Invoke( );
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            blockInteraction = false;
            OnExitInteractableArea?.Invoke( );
            focalPointMarker.SetActive(false);
            enableInteraction = false;
        }
    }
}
