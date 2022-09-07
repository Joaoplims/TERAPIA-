using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SurroundSensor :MonoBehaviour
{
    public LayerMask targetsMask;
    public Transform Target { get; private set; }

    [SerializeField] private float radius = 7f;

    private Collider[] checkColliders;
    private void Start()
    {
        checkColliders = new Collider[5];
        InvokeRepeating("CheckSurround", 0f,0.1f);
    }


    public void CheckSurround(){
        int targets = Physics.OverlapSphereNonAlloc(transform.position, radius, checkColliders, targetsMask);
        if(targets > 0){
            if(checkColliders[0].CompareTag("Player")){
                Target = checkColliders[0].transform;
            }
            else{
                Target = null;
            }
        }
        else{
            Target = null;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color= Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void RemoveTargetAfter()
    {
        Target = null;
    }

}
