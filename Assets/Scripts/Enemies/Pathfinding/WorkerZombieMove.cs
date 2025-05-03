using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerZombieMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public GameObject turretPrefab;
    public float safeDistance = 5f; 
    public float arrivalThreshold = 0.5f;
    private bool isSpawning;

    void Start()
    {
        player = GameObject.Find("Lucko").transform.GetChild(0);
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        agent.updatePosition = true;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.autoBraking = false;
        agent.acceleration = float.MaxValue;

        MoveToRandomPoint();
    }

    private void Update()
    {
        if (!agent.hasPath && !isSpawning)
        {
            MoveToRandomPoint();
        }

        if (agent.remainingDistance <= arrivalThreshold && !agent.pathPending &&!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnTurretWithDelay()); // Start coroutine to wait before spawning
        }
    }

    void MoveToRandomPoint()
    {
        Vector3 randomPoint;
        do
        {
            randomPoint = new Vector3(Random.Range(-14f, 14f), Random.Range(-14f, 14f), 0f);
        }
        while (Vector3.Distance(randomPoint, player.position) < safeDistance);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    IEnumerator SpawnTurretWithDelay()
    {
        yield return new WaitForSeconds(2.5f);

        if (turretPrefab != null)
        {
            Instantiate(turretPrefab, transform.position, Quaternion.identity);
            MoveToRandomPoint();
        }
        else
        {
            Debug.LogWarning("Turret prefab is not assigned!");
        }
        isSpawning = false;
    }
}