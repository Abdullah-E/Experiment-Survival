using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GravesController : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    public NavMeshAgent Agent {get=>agent;}

    [SerializeField]
    private string currentState;
    public PatrollingPath path;
    private GameObject player;
    public float sightRange = 10f;

    public float fov = 90f;
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        stateMachine.Initialize();
    }

    void Update()
    {
        PlayerVisible();
        // agent.SetDestination(player.position);
        currentState = stateMachine.currentState.ToString();
    }

    public bool PlayerVisible (){
        if(player == null) return false;
        //check distance:
        Vector3 diff = player.transform.position - transform.position;
        float distance = diff.magnitude;
        if(distance > sightRange) return false;

        //check angle:
        Vector3 forward = transform.forward;
        float angle = Vector3.Angle(diff, forward);
        if(angle >= -fov && angle <= fov){
            Debug.Log("Player is visible");
            //check env objects:
            Ray ray = new(transform.position, diff);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, sightRange)){
                if(hit.collider.gameObject == player){
                    return true;
                }
            }

        }
        return false;

    }
}
