using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyProjectileAttack : MonoBehaviour
{
    private EnemyMove enemyMove;
    public float damage;
    public float timeBetweenAttack;
    public GameObject projectile;
    public Transform projectileSpawnPoint;
    private bool canAttack = true;
    public Projectile projectiletype;
    void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
    }

    void Update()
    {
        if (enemyMove.isInSight && enemyMove.hasDetected && canAttack)
        {
            GameObject spawnedProjectile = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            spawnedProjectile.GetComponent<ProjectileBehaviour>().thisProjectile = projectiletype;
            spawnedProjectile.GetComponent<ProjectileBehaviour>().damage = damage;
            Vector3 direction = enemyMove.player.position - projectileSpawnPoint.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            spawnedProjectile.transform.rotation = Quaternion.Euler(0, 0, angle-90);


            canAttack = false;

            StartCoroutine(ResetAttack(timeBetweenAttack));
        }
    }

    public IEnumerator ResetAttack(float tm)
    {
        yield return new WaitForSeconds(tm);
        canAttack = true;
    }
}