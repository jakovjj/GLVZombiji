using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BinaryAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign a projectile prefab in Unity
    public float projectileSpeed = 10f;
    public float increasePlayerDamageBy;
    public float timeBetweenAttacks;
    private bool canAttack;
    private void Start()
    {
        canAttack = true;
    }
    void Update()
    {
        if (canAttack && Input.GetMouseButtonDown(0) && GameObject.Find("gameManager").GetComponent<manager>().equippedWeapon == 4 && EventSystem.current.IsPointerOverGameObject() == false) // Left mouse button
        {
            ShootProjectile();
            canAttack = false;
            StartCoroutine(resetCanAttack());
        }
    }
    public IEnumerator resetCanAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(timeBetweenAttacks);
        canAttack = true;
    }
    void ShootProjectile()
    {
        SoundManager.PlaySound(SoundEffect.BinaryThing2, 0.6f, 1.15);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ensure the projectile stays in 2D space

        Vector3 direction = (mousePosition - transform.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
        projectile.GetComponent<BinaryAttackMove>().speed = projectileSpeed;
    }
}