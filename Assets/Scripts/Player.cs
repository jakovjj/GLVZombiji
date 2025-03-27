using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class PlayerAttack : MonoBehaviour
{
  //  [SerializeField] ItemInventory itemInventory;           hrcini zbagani itemi
    public PlayerAttack selfScript;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public Projectile ammoType;
    public float gunCooldown;
    public Transform bullet;

  //  public nostalgia testItem;           hrcini zbagani itemi

    [SerializeField] manager playerStats;

    public Transform gunPosition;
    Vector2 mousePosition;
    public Camera cam;


    public SpriteRenderer thisGunSprite;

    public GameObject self;
    public Rigidbody2D rb;

    private Vector2 moveDirection;
    private float defaultCameraSize;

    public delegate void playerShoot(int weapon);
    public playerShoot shoot;

    void shootDefault(int weapon)
    {             
        bullet = Instantiate(bulletPrefab.transform, firePoint.position, firePoint.rotation);
        Projectile bulletType = playerStats.weaponProjectile[weapon];
        bullet.GetComponent<ProjectileBehaviour>().thisProjectile = bulletType;
        bullet.GetComponent<ProjectileBehaviour>().damage = bulletType.damage;
        gunCooldown = playerStats.weaponCooldown[weapon];
        
  //      thisGunSprite.GetComponent<SpriteRenderer>().sprite = manager.weaponSprite[weapon];
    }
    void Awake()
    {
        shoot += shootDefault;
    }
    void Start()
    {
        defaultCameraSize = cam.orthographicSize;
        waveStart();

  //      testItem.Pickup();            hrcini zbagani itemi
    }

    public void waveStart()
    {
        rb.position = new Vector2(0,0);
    }
    void Update()
    {
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetButton("Fire1") & gunCooldown<=0)
        {
            shoot?.Invoke(playerStats.equippedWeapon);
        }
        cam.orthographicSize=defaultCameraSize;
        gunCooldown -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        Vector2 lookAt = mousePosition - (Vector2)gunPosition.position;

        float angle = Mathf.Atan2(lookAt.y, lookAt.x) * Mathf.Rad2Deg;
        if (angle > 90 || angle < -90)
        {
            angle -= 180;
            gunPosition.localScale = new Vector2(-1, 1);
        }
        else
        {
            gunPosition.localScale = new Vector2(1, 1);
        }
        gunPosition.rotation = Quaternion.Slerp(gunPosition.rotation, Quaternion.Euler(0, 0, angle), 1);

    }
    public void SwitchGun(int newWeapon)
    {
        gunCooldown = playerStats.weaponCooldown[newWeapon];
    }
}
