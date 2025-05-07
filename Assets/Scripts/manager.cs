using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class manager : MonoBehaviour
{
    [HideInInspector] public float hp;
    public static float MaxPlayerHealth;
    [HideInInspector] public float rgn;
    [HideInInspector] public float dmg;
    [HideInInspector] public int coin;
    [HideInInspector] public bool play;
    [HideInInspector] public int equippedWeapon;
    [HideInInspector] public float[] weaponCooldown = { };
    public Projectile[] weaponProjectile = { };
    public GameObject[] bulletPrefabs= { };
    public PlayerMovement PlayerMovement;

    //enemy prefabs
    public GameObject[] zombie = { };

    public GameObject mainUI;
    public GameObject shopUI;

    [HideInInspector] public int wave;
    [HideInInspector] public int timer;

    public Text texthp;
    public Text textwave;
    public Text texttime;
    public Text textcoin;
    public Text textcoinshop;
    public Text wavetextshop;
    public Slider hpbar;
    public GameObject lucko;

    void Start() {
        lucko = GameObject.Find("Lucko");
        PlayerMovement = lucko.transform.GetChild(0).GetComponent<PlayerMovement>();
        equippedWeapon = 0;
        hp = MaxPlayerHealth;
        dmg = 1f; coin = 0;
        wave = 0; timer = 30;
        weaponCooldown = new float[] { 0.2f, 0.35f, 0.2f, 0.2f, 0.2f };

        newWave();
    }

    public void pickAbility(int index) {
       if(LaserTemporary.isLaserOn==false) equippedWeapon = index;
    }
    
    private IEnumerator summonZombie() {
        while (timer > 0) {
            float posX = Random.Range(-14.0f, 14.0f);
            float posY = Random.Range(-14.0f, 14.0f); 

            int rng = Random.Range(0, zombie.Length);
            //Instantiate(zombie[rng-1], new Vector3(posX, posY, 0f), Quaternion.identity);
            if (rng == 7)
            {
                for (int i = 0; i < 8; i++)
                {
                    float xOffset = Random.Range(0f, 1f); // Random offset between 0 and 1
                    float yOffset = Random.Range(0f, 1f); // Random offset between 0 and 1
                    Vector3 cur=new Vector3(posX, posY, 0f);
                    Vector3 spawnPosition = cur+  new Vector3(xOffset, yOffset, 0);
                    if (spawnPosition.y < -14|| spawnPosition.y > 14 || spawnPosition.x < -14 || spawnPosition.x > 14)
                    {
                        i--;
                        continue;
                    }
                    Instantiate(zombie[rng], spawnPosition, Quaternion.identity);
                }
            }
            else
            {
                Instantiate(zombie[rng], new Vector3(posX, posY, 0f), Quaternion.identity);
            }
            

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
        PlayerMovement.speedSlowMultiplier = 0f;
    }

    public void newWave() {
        mainUI.SetActive(true);
        shopUI.SetActive(false);
        play = true;

        timer = 50;
        PlayerMovement.speedSlowMultiplier = 1f;
        wave = wave+1;
        StartCoroutine(summonZombie());
        StartCoroutine(tickTimer());
    }

    void Update() {
        //Debug.Log(equippedWeapon);
        texthp.text = hp.ToString()+"/"+MaxPlayerHealth.ToString();
        textcoin.text = coin.ToString();
        textcoinshop.text = coin.ToString();
        hpbar.value = hp/MaxPlayerHealth;
        wavetextshop.text = "Start Wave "+wave.ToString();

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
