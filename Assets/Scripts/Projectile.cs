using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newProjectile", menuName = "Projectile", order = 2)]
public class Projectile : ScriptableObject
{
    public string projectileTargets;

    public Sprite projectileSprite;

    public float damage;
    public int damageType;
    public float speed;
    public float projectileLifetime;
    public float projectileSizeX;
    public float projectileSizeY;
    public int projectilePierce;

}
