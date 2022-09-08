using System.Collections;
using System.Collections.Generic;
using Terapia;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Container :MonoBehaviour
{
    [SerializeField] private bool isTrap = true;
    [SerializeField] private GameObject reward;


    private void Start()
    {
        GetComponent<BoxCollider>( ).isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isTrap)
            {
                other.GetComponent<PlayerActions>( ).EndGame( );
            }
            else
            {
                GameObject realReward = Instantiate(reward , transform.position , transform.rotation);
            }

        }
    }

}
