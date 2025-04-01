using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class EnemyMove : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public Animator animator;
    public float DetectRadius;
    public float InteractionRadius;
    public LayerMask whatIsPlayer;
    public LayerMask obstaclesLayer;
    private List<Transform> waypoints = new List<Transform>();
    private int currentWaypointIndex = 0;
    public float IdleSpeed;
    public float ChaseSpeed;
    public bool hasDetected;
    public bool isInInteractionRadius;
    public bool isInSight;
    public bool hasBeenHit;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = true;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.autoBraking = false;
        agent.acceleration = 10000000000000000;
    }
    //adasd

    void Update()
    {
        if (player == null) player = GameObject.Find("Lucko").transform.GetChild(0);

         hasDetected = Physics2D.OverlapCircle(transform.position, DetectRadius, whatIsPlayer);
        //Debug.Log(hasDetected);
         isInInteractionRadius = Physics2D.OverlapCircle(transform.position, InteractionRadius, whatIsPlayer);
        //Debug.Log(isInInteractionRadius);
         isInSight = true;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.position - transform.position).normalized, DetectRadius, obstaclesLayer | whatIsPlayer);
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player")) isInSight = true;
        else isInSight = false;
        if (hasBeenHit)
        {
            deleteWaypoints();
            agent.speed = ChaseSpeed;
            agent.SetDestination(player.position);
           // StartCoroutine(stopChase(5));
        }
        if (isInInteractionRadius && isInSight)
        {
            hasBeenHit = false;
            agent.SetDestination(agent.transform.position);
        }
        else if (hasDetected && isInSight)
        {
            hasBeenHit = false;
            deleteWaypoints();
            agent.speed = ChaseSpeed;

            agent.SetDestination(player.position);


            if (agent.gameObject.transform.childCount > 0)
            {
                // agent.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        else if(!hasBeenHit)
        {
            agent.speed = IdleSpeed;
            //Transform target=null;
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    GotoNextPoint();
                }
            }
       
        }
    }

    void GotoNextPoint()
    {
        if (waypoints.Count == 0)
        {
            CreateWaypoints();
        }

        agent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
    }
    void deleteWaypoints()
    {
        foreach (Transform waypoint in waypoints)
        {
            if (waypoint != null)
            {
                Destroy(waypoint.gameObject);
            }
        }
        waypoints.Clear();
    }
    void CreateWaypoints()
    {
        deleteWaypoints();


        Vector3[] waypointPositions = new Vector3[]
        {
            transform.position + Vector3.up*2,
            transform.position + Vector3.down*2,
            transform.position + Vector3.left*2,
            transform.position + Vector3.right * 2
        };

        foreach (var position in waypointPositions)
        {
            GameObject waypoint = new GameObject("Waypoint");
            waypoint.transform.parent = transform.parent;
            waypoint.transform.position = position;
            waypoints.Add(waypoint.transform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(gameObject.transform.position, new Vector3(0, 0, 1), DetectRadius);
    }
    IEnumerator stopChase(int time)
    {
        yield return new WaitForSeconds(time);
        hasBeenHit = false;
    }
}