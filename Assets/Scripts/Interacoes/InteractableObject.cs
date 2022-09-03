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
    private void Awake()
    {
        col = GetComponent<Collider>();
        col.isTrigger = true;
    }
    public abstract void Interact();

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            focalPointMarker.SetActive(false);
            OnEnterInteractableArea?.Invoke();
        }
    }
    public virtual void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(Keyboard.current.eKey.wasPressedThisFrame);
            if(blockInteraction == false && Keyboard.current.eKey.wasPressedThisFrame == true)
            {
                blockInteraction = true;
                Interact();
                OnInteract?.Invoke();
            }
            OnStayInteractableArea?.Invoke();
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            blockInteraction = false;
            OnExitInteractableArea?.Invoke();
            focalPointMarker.SetActive(true);
        }
    }
}
