using System.Collections;
using System.Collections.Generic;
using Terapia;
using UnityEngine;
using UnityEngine.AI;

public class ChaserMonster :MonoBehaviour, IEnableInput
{
    public Transform NoiseTarget { get; set; }
    public bool LockInput { get; set; }

    [SerializeField] private MonsterStates currentState = MonsterStates.None;
    [Space]
    [SerializeField] private SurroundSensor surroundSensor;
    [SerializeField] private float fovAngle;
    [SerializeField] private float maxSightDistance;
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask obstructionMask;
    [SerializeField] private LayerMask targetMask;
    [Space]
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private int currentWaypoint = 0;

    private float timerStayInState = 0f;



    private bool lockNextWPSelection = false;
    private Collider[] rangeChecks;


    private Vector3 playerMonsterDir;
    private NavMeshAgent agent;

    private void Start()
    {
        LockInput = false;
        agent = GetComponent<NavMeshAgent>( );
        currentState = MonsterStates.Wandering;
        rangeChecks = new Collider[10];

    }
    private void Update()
    {
        if (LockInput)
        {
            agent.isStopped = true;
            return;
        }
        // handle states
        switch (currentState)
        {
            case MonsterStates.Chassing:
            PerformChaseState( );
            break;

            // Wander State
            case MonsterStates.Wandering:
            PerformWanderState( );
            break;

            // Looking for state
            case MonsterStates.LookingFor:
            PerformLookingForState( );
            break;

            case MonsterStates.FollowingNoise:
            PerformFollowNoiseState( );
            break;
        }

    }


    public void ChangeState(MonsterStates newState)
    {
        if (newState == currentState)
            return;

        // on Exit current state


        currentState = newState;

        // On Enter NewState
        switch (currentState)
        {
            case MonsterStates.None:
            break;
            case MonsterStates.Wandering:
            break;
            case MonsterStates.Chassing:
            //int rand = Random.Range(0 , 2);
            //switch (rand)
            //{
            //    case 1:
            //    AudioManager.Instancia.PlaySfx(1);
            //    break;
            //    case 2:
            //    AudioManager.Instancia.PlaySfx(2);
            //    break;
            //    default:
            //    break;
            //}
            break;
            case MonsterStates.LookingFor:
            break;
            case MonsterStates.FollowingNoise:
            int randx = Random.Range(1 , 2);
            switch (randx)
            {
                case 1:
                AudioManager.Instancia.PlaySfx(3);
                break;
                case 2:
                AudioManager.Instancia.PlaySfx(4);
                break;
                default:
                break;
            }
            break;
            default:
            break;
        }
    }

    private void PerformFollowNoiseState()
    {
        CheckAround( );

        agent.SetDestination(NoiseTarget.position);
        if (NoiseTarget != null && Vector3.Distance(transform.position , NoiseTarget.position) < 0.1f)
            ChangeState(MonsterStates.Wandering);
    }
    private void PerformLookingForState()
    {
        if (surroundSensor.Target != null)
            ChangeState(MonsterStates.Wandering);

        agent.isStopped = true;
        timerStayInState += Time.deltaTime;
        if (timerStayInState >= 5f)
        {
            ChangeState(MonsterStates.Wandering);
            timerStayInState = 0;
        }
    }
    private void PerformWanderState()
    {

        timerStayInState = 0f;
        agent.isStopped = false;

        // Check Transition
        CheckAround( );


        // Perform Action    
        if (lockNextWPSelection == false)
        {
            int nextWP = Random.Range(0 , waypoints.Count);

            if (nextWP != currentWaypoint)
            {
                agent.SetDestination(waypoints[nextWP].position);
                currentWaypoint = nextWP;
                lockNextWPSelection = true;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position , waypoints[currentWaypoint].position) <= 0.1f)
                lockNextWPSelection = false;
        }

    }
    private void PerformChaseState()
    {

        timerStayInState = 0f;
        agent.isStopped = false;
        //Check Transition
        CheckIfPlayerStillInSight( );

        lockNextWPSelection = false;
        agent.SetDestination(target.position);

    }
    private void CheckAround()
    {
        Vector3 pos2TargetDir = Vector3.zero;

        // check if player is in the area that enemy can hear

        if (surroundSensor.Target != null)
        {
            pos2TargetDir = ( surroundSensor.Target.position - transform.position ).normalized;
            // transform.forward = pos2TargetDir;
            float distance = Vector3.Distance(transform.position , pos2TargetDir);
            if (Physics.Raycast(transform.position , pos2TargetDir , distance , obstructionMask) == false)
            {
                Debug.Log("Colidi com algo que nao obstrui visao");
                //currentState = MonsterStates.Chassing;
                ChangeState(MonsterStates.Chassing);

            }
            else
            {
                int colliderInSightDir = Physics.OverlapSphereNonAlloc(transform.position + ( pos2TargetDir * distance ) , 7f / 2f , rangeChecks , targetMask);
                if (colliderInSightDir > 0)
                {
                    if (rangeChecks[0].CompareTag("Player"))
                    {
                        ChangeState(MonsterStates.Chassing);
                    }
                }
                else
                {
                    ChangeState(MonsterStates.Wandering);
                }
            }
        }
        else
        {
            ChangeState(MonsterStates.Wandering);
        }
    }
    private void CheckIfPlayerStillInSight()
    {
        Debug.DrawRay(transform.position , ( target.transform.position - transform.position ).normalized * maxSightDistance , Color.red);
        if (Vector3.Distance(transform.position , target.position) < maxSightDistance)
        {
            //currentState = MonsterStates.Chassing;
            ChangeState(MonsterStates.Chassing);
        }
        else
        {
            //currentState = MonsterStates.LookingFor;
            ChangeState(MonsterStates.LookingFor);
        }
    }

    public enum MonsterStates
    {
        None,
        Wandering,
        Chassing,
        LookingFor,
        FollowingNoise
    }
}




