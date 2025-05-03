using System.Collections;
using UnityEngine;

public class ShamanAttack : MonoBehaviour
{
    public GameObject[] zombies = { }; // Array of zombie prefabs
    private EnemyMove enemyMove;
    public float timeBetweenAttack;
    private bool canAttack = true;
    public float spawnRadius = 2f; // How far zombies spawn from shaman
    public GameObject spawnParticles;
    void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
    }

    void Update()
    {
        if (enemyMove.isInSight && enemyMove.hasDetected && canAttack)
        {
            canAttack = false;
            SpawnZombies();
            StartCoroutine(ResetAttack(timeBetweenAttack));
        }
    }

    void SpawnZombies()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector2 spawnOffset = Random.insideUnitCircle * spawnRadius; 
            Vector2 spawnPosition = (Vector2)transform.position + spawnOffset;
            if(spawnPosition.x>14 || spawnPosition.x<-14 || spawnPosition.y>14 || spawnPosition.y < -14)
            {
                i--;
                continue;
            }

            Instantiate(zombies[Random.Range(0, zombies.Length)], spawnPosition, Quaternion.identity);
            Instantiate(spawnParticles, spawnPosition, Quaternion.identity);
        }
    }

    public IEnumerator ResetAttack(float tm)
    {
        yield return new WaitForSeconds(tm);
        canAttack = true;
    }
}