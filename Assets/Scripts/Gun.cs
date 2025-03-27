using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newGun", menuName = "gun", order = 1)]
public class Gun : ScriptableObject
{
    public string gunName;
    public string gunDescription;

    public Sprite gunArtwork;

    public Projectile gunProjectile;

    public float gunDelay;
    public float gunSpread;
    public int gunAmmo;

    public bool startingGun;
}
