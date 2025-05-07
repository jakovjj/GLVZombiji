using UnityEngine;

public class BinaryAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign a projectile prefab in Unity
    public float projectileSpeed = 10f;
    public float increasePlayerDamageBy;
    public FixedJoystick joystick;
    void Update()
    {
        if (joystick.Direction.magnitude>0.8f && GameObject.Find("gameManager").GetComponent<manager>().equippedWeapon == 4) // Left mouse button
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ensure the projectile stays in 2D space

        //Vector3 direction = (mousePosition - transform.position).normalized;
        Vector3 direction=joystick.Direction;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
        projectile.GetComponent<BinaryAttackMove>().speed = projectileSpeed;
    }
}