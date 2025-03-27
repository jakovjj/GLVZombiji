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

        bool hasDetected = Physics2D.OverlapCircle(transform.position, DetectRadius, whatIsPlayer);
        //Debug.Log(hasDetected);
        bool isInInteractionRadius = Physics2D.OverlapCircle(transform.position, InteractionRadius, whatIsPlayer);
        //Debug.Log(isInInteractionRadius);
        bool isInSight = true;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.position - transform.position).normalized, DetectRadius, obstaclesLayer | whatIsPlayer);
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player")) isInSight = true;
        else isInSight = false;
        if (isInInteractionRadius && isInSight)
        {
            agent.SetDestination(agent.transform.position);
        }
        else if (hasDetected && isInSight)
        {
            deleteWaypoints();
            agent.speed = ChaseSpeed;
            //ZA ROTIRANJE
            /*
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle); 
            */
            agent.SetDestination(player.position);


            if (agent.gameObject.transform.childCount > 0)
            {
                // agent.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        else
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
            //ZA ROTIRANJE
            /*if (target != null)
            {
                Vector3 direction = target.position - transform.position;
                direction.Normalize();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }*/
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
    }
}