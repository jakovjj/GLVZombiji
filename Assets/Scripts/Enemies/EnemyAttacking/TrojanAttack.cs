using UnityEngine;
using UnityEngine.AI;

public class TrojanAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject[] zombies = {};
    public int amount;
    /*public float limitYNeg;
    public float limitYPos;
    public float limitXNeg;
    public float limitXPos;*/
    public void spawnOnDeath()
    {
        for (int i = 0; i < amount; i++)
        {
            float xOffset = Random.Range(0f, 1f); // Random offset between 0 and 1
            float yOffset = Random.Range(0f, 1f); // Random offset between 0 and 1
            Vector3 spawnPosition = transform.position + new Vector3(xOffset, yOffset, 0);
            NavMeshHit hit;
            bool isOnNavMesh = NavMesh.SamplePosition(spawnPosition, out hit, 0.5f, NavMesh.AllAreas);
            if (!isOnNavMesh)
            {
                i--;
                continue;
            }
            Instantiate(zombies[Random.Range(0, zombies.Length)], spawnPosition, Quaternion.identity);
        }
    }
}
