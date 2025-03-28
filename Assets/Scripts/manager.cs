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
    [HideInInspector] public float [] weaponCooldown = {};
    public Projectile[] weaponProjectile = { };

    //enemy prefabs
    public GameObject zombie; 

    [HideInInspector] public int wave; 
    [HideInInspector] public int timer;

    public Text texthp;
    public Text textwave;
    public Text texttime;
    public Text textcoin;
    public Slider hpbar;

    void Start() {
        equippedWeapon = 0;
        mhp = 20f; hp = mhp;
        dmg = 1f; coin = 0;
        wave = 0; timer = 30;
        weaponCooldown = new float[]{0.2f,0.2f,0.2f,0.2f,0.2f};
        

        newWave();
    }
    
    private IEnumerator summonZombie() {
        while (timer > 0) {
            yield return new WaitForSeconds(5);

            float posX = Random.Range(-14.0f, 14.0f);
            float posY = Random.Range(-14.0f, 14.0f); 

            Instantiate(zombie, new Vector3(posX, posY, 0f), Quaternion.identity);
        }
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
        StartCoroutine(summonZombie());
        StartCoroutine(tickTimer());
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
