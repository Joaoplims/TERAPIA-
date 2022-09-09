using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform player;
    void Start()
    {
        player = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt((player.position-transform.position).normalized);
    }
}
