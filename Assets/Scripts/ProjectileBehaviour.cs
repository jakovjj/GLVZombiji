using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public GameObject thisBullet;
    public Projectile thisProjectile;
    private int currentPierce;
    Health targetHealthScript;
    public float damage;

    [SerializeField] float lifetime;

    [SerializeField] SpriteRenderer self;
    void Start()
    {
        self.sprite = thisProjectile.projectileSprite;
        thisBullet.transform.localScale = new Vector2(thisProjectile.projectileSizeX, thisProjectile.projectileSizeY);
        lifetime = thisProjectile.projectileLifetime;
        currentPierce = thisProjectile.projectilePierce;
    }
    void FixedUpdate()
    {
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
        //Debug.Log(target.tag);
        //Debug.Log(target);
        //Debug.Log(thisProjectile);
        //Debug.Log(thisProjectile.projectileTargets);
        if (thisProjectile.projectileTargets==target.tag)
        {
            if(target.tag=="Player")targetHealthScript = target.GetComponent<playerHealth>();
            else targetHealthScript = target.GetComponent<enemyHealth>();
            targetHealthScript.Damage(damage);
            currentPierce -= 1;
            if(currentPierce < 0)
            {
                Destroy(thisBullet);
            }
        }
    }
}
