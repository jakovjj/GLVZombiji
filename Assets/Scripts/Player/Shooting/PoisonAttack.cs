using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PoisonAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnPoint;
    public float projectileSpeed = 1.5f;
    public float timeBetweenShots = 0.7f;
    public float cldwn;
    //public float projectileSpeed;
    void Update()
    {
        cldwn -= Time.deltaTime * MasterControllerScript.playerAttackSpeed;
        if (Input.GetMouseButtonDown(0) && GameObject.Find("gameManager").GetComponent<manager>().equippedWeapon == 2 && EventSystem.current.IsPointerOverGameObject() == false && cldwn<=0) 
        {
            ShootConeAttack();
            cldwn = timeBetweenShots;
        }
    }

    void ShootConeAttack()
    {
        SoundManager.PlaySound(SoundEffect.GasAttack, 1, 0.33);
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