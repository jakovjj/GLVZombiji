using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class manager : MonoBehaviour
{
    [HideInInspector] public float hp;
    [HideInInspector] public float mhp;
    [HideInInspector] public float rgn;
    [HideInInspector] public float dmg;
    [HideInInspector] public int coin;
    [HideInInspector] public int equippedWeapon;
    [HideInInspector] public int [] weaponCooldown= {1,1,1,1,1};
     public Projectile[] weaponProjectile = { };

    [HideInInspector] public int wave; 
    [HideInInspector] public int timer;

    public Text texthp;
    public Text textwave;
    public Text texttime;
    public Text textcoin;
    public Slider hpbar;

    private IEnumerator tickvar;

    void Start() {
        mhp = 20f; hp = mhp;
        dmg = 1f; coin = 0;
        wave = 0; timer = 30;
        tickvar = tickTimer();

        newWave();
    }
   
    private IEnumerator tickTimer() {
        while (timer > 0) {
            yield return new WaitForSeconds(1);
            timer--;
        }

        newWave(); //kasnije treba prebaciti na neki shop ili nes
    }

    void newWave() {
        timer = 50;
        wave++;

        StartCoroutine(tickvar);
    }

    void Update() {
        texthp.text = hp.ToString()+"/"+mhp.ToString();
        textcoin.text = coin.ToString();
        hpbar.value = hp*100/mhp;

        textwave.text = "Wave "+wave.ToString();
        texttime.text = timer.ToString();
    }

    public float calcDamage(float amount)
    {
        return amount;
    }
}
