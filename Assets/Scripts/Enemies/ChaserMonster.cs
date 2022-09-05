using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaserMonster : MonoBehaviour
{
    [SerializeField] private MonsterStates currentState = MonsterStates.None;
    [SerializeField] private MonsterStates previousState = MonsterStates.None;
    [SerializeField] private float maxDistanceOfSight;
    [SerializeField] private Transform target;

    private Vector3 playerMonsterDir;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        // Field of view sensor
        playerMonsterDir = (target.position - transform.position).normalized;
        if(Vector3.Dot(playerMonsterDir, transform.forward) >= 0.38f && Vector3.Distance(target.position, transform.position) <= maxDistanceOfSight){
            Debug.Log("Dentro da area de visão!");
            currentState = MonsterStates.Chassing;
            agent.SetDestination(target.position);
        }
        
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * maxDistanceOfSight);
        Gizmos.DrawRay(transform.position, playerMonsterDir * maxDistanceOfSight);
        
    }
public enum MonsterStates{
    None,
    Wandering,
    Chassing,
    LookingFor
}
}




