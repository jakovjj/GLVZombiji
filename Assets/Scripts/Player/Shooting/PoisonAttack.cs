using UnityEngine;
using System.Collections;

public class PoisonAttack : MonoBehaviour
{
    public static float playerPoisonDmg=10f;
    public GameObject projectilePrefab;
    public Transform spawnPoint;
    public float projectileSpeed = 1.5f;
    public float timeBetweenShots = 0.7f;
    //public float projectileSpeed;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameObject.Find("gameManager").GetComponent<manager>().equippedWeapon == 2) 
        {
            ShootConeAttack();
        }
    }

    void ShootConeAttack()
    {
        
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            Vector3 direction = (mousePosition - spawnPoint.position).normalized;
        float currentProjectileSpeed = projectileSpeed;
        for (int i = 0; i < 3; i++)
        {
            ShootProjectile(direction, 0,currentProjectileSpeed);
            ShootProjectile(direction, -15f,currentProjectileSpeed);
            ShootProjectile(direction, 15f,currentProjectileSpeed);
            currentProjectileSpeed -= 1.5f;
        }
    }

    void ShootProjectile(Vector3 baseDirection, float angleOffset,float projectileSpeed)
    {
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        projectile.GetComponent<GasMove>().speed = projectileSpeed;
        float angle = Mathf.Atan2(baseDirection.y, baseDirection.x) * Mathf.Rad2Deg + angleOffset;
        projectile.transform.rotation = Quaternion.Euler(0, 0, angle);

        Vector3 movement = projectile.transform.up * projectileSpeed * Time.deltaTime;
        projectile.transform.position += movement;
    }
}