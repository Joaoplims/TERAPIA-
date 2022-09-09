using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door :MonoBehaviour
{
    [SerializeField] private Vector3 openRotation;
    [SerializeField] private Vector3 closeRotation;
    [SerializeField] private GameObject pivot;

    [ContextMenu("OpenDoor")]
    public void OpenDoor()
    {
        LeanTween.rotateLocal(pivot,openRotation,1f);
        GetComponent<Collider>().enabled = false;
    }


    public void CloseDoor()
    {
        LeanTween.rotateLocal(pivot,closeRotation,1f);
    }
}
