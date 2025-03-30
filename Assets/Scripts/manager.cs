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
    [HideInInspector] public bool play;
    [HideInInspector] public int equippedWeapon;
    [HideInInspector] public float[] weaponCooldown = { };
    public Projectile[] weaponProjectile = { };
    public GameObject[] bulletPrefabs= { };

    //enemy prefabs
    public GameObject zombie;
    public GameObject firefly;

    public GameObject mainUI;
    public GameObject shopUI;

    [HideInInspector] public int wave;
    [HideInInspector] public int timer;

    public Text texthp;
    public Text textwave;
    public Text texttime;
    public Text textcoin;
    public Text textcoinshop;
    public Slider hpbar;
    public GameObject lucko;

    void Start() {
        lucko = GameObject.Find("Lucko");
        equippedWeapon = 0;
        mhp = 20f; hp = mhp;
        dmg = 1f; coin = 0;
        wave = 0; timer = 30;
        weaponCooldown = new float[] { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f };

        newWave();
    }
    
    private IEnumerator summonZombie() {
        while (timer > 0) {
            float posX = Random.Range(-14.0f, 14.0f);
            float posY = Random.Range(-14.0f, 14.0f); 

            int rng = Random.Range(1, 3);
            if (rng == 1) Instantiate(zombie, new Vector3(posX, posY, 0f), Quaternion.identity);
            if (rng == 2) Instantiate(firefly, new Vector3(posX, posY, 0f), Quaternion.identity);

            yield return new WaitForSeconds(5);
        }
    }

    private IEnumerator tickTimer() {
        while (timer > 0) {
            yield return new WaitForSeconds(1);
            timer--;
        }

        openShop();
    }

    public void openShop() {
        mainUI.SetActive(false);
        shopUI.SetActive(true);
        play = false;
    }

    public void newWave() {
        mainUI.SetActive(true);
        shopUI.SetActive(false);
        play = true;

        timer = 50;
        wave = wave+1;
        StartCoroutine(summonZombie());
        StartCoroutine(tickTimer());
    }

    void Update() {
        texthp.text = hp.ToString()+"/"+mhp.ToString();
        textcoin.text = coin.ToString();
        textcoinshop.text = coin.ToString();
        hpbar.value = hp/mhp;

        textwave.text = "Wave "+wave.ToString();
        texttime.text = timer.ToString();
    }

    public float calcDamage(float amount)
    {

        return amount;
    }
    public void getDamage(float amount, bool hasKnockBack, Vector2 dir)
    {
        /*if (hasKnockBack && canTakeKnockback)
        {
            lucko.transform.GetChild(0).GetComponent<Rigidbody2D>().AddForce(dir * 500, ForceMode2D.Impulse);
            canTakeKnockback = false;
            StartCoroutine(resetKnockback(0.01f));
        }*/
        hp -= amount;
    }
   /* public IEnumerator resetKnockback(float tm)
    {
        yield return new WaitForSeconds(tm);
        canTakeKnockback = true;
    }*/
}
