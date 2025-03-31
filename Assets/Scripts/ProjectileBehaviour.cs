using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public GameObject thisBullet;
    public Projectile thisProjectile;
    public string projectileName;
    private int currentPierce;
    enemyHealth targetHealthScript;
    playerHealth targetHealthScript1;
    public float damage;
    private AudioSource hit;
    public delegate void whenIHitSomething(GameObject target);
    public whenIHitSomething OnHit;
    [SerializeField] float lifetime;
    private Vector3 previousPosition;
    private Vector3 dir;
    [SerializeField] SpriteRenderer self;
    public GameObject chainLightningEffect;
    public virtual void Start()
    {
        if (projectileName == "lightning") chainLightningEffect = Resources.Load<GameObject>("Chain_Lightning_Prefab");
        hit = GameObject.Find("SoundManager").GetComponents<AudioSource>()[3];
        self.sprite = thisProjectile.projectileSprite;
        thisBullet.transform.localScale = new Vector2(thisProjectile.projectileSizeX, thisProjectile.projectileSizeY);
        lifetime = thisProjectile.projectileLifetime;
        currentPierce = thisProjectile.projectilePierce;
    }
    public virtual void FixedUpdate()
    {
        dir = (transform.position - previousPosition).normalized;

        
        previousPosition = transform.position;

        if (thisProjectile != null)
        {
            thisBullet.transform.Translate(0,thisProjectile.speed,0);
        }
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(thisBullet);
        }
    }
 
    void OnTriggerEnter2D(Collider2D target)
    {
        if (thisProjectile.projectileTargets.Contains(target.tag))
        {
            if (target.tag == "Obstacle")
            {
                hit.Play();
                Destroy(thisBullet);
            }
            else
            {
                if (target.tag == "Player") targetHealthScript1 = target.GetComponent<playerHealth>();
                else targetHealthScript = target.GetComponent<enemyHealth>();
                if (target.tag == "Enemy")
                {
                    targetHealthScript.Damage(damage, false,dir);
                    if (projectileName == "lightning") Instantiate(chainLightningEffect,target.transform.position, Quaternion.identity);
                }
                else targetHealthScript1.Damage(damage/*daj Vector2 koji predstavlja knockback na playera i izbrisi ovaj new vector2 Vector2 { }*/);
                //OnHit(target.gameObject);
                currentPierce -= 1;
                if (currentPierce < 0)
                {
                    Destroy(thisBullet);
                }
            }

        }
    }
}
