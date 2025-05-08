using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.AI;

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

    public zombieSpawn[] zombie = { };
    public zombieSpawn[] wave1 = { };
    public  zombieSpawn[] wave2 = { };
    [SerializeField] zombieSpawn[] wave3 = { };
    [SerializeField] zombieSpawn[] wave4 = { };
    [SerializeField] zombieSpawn[] wave5 = { };
    [SerializeField] zombieSpawn[] wave6 = { };
    [SerializeField] zombieSpawn[] wave7 = { };
    [SerializeField] zombieSpawn[] wave8 = { };
    [SerializeField] zombieSpawn[] wave9 = { };
    [SerializeField] zombieSpawn[] wave10= { };
    [SerializeField] zombieSpawn[] wave11= { };
    [SerializeField] zombieSpawn[] wave12 = { };
    [SerializeField] zombieSpawn[] wave13 = { };
    [SerializeField] zombieSpawn[] wave14 = { };
    [SerializeField] zombieSpawn[] wave15 = { };

    public GameObject mainUI;
    public GameObject shopUI;
    public GameObject pauseUI;
    public GameObject gameOverUI;

    [HideInInspector] public int wave;
    [HideInInspector] public int timer;

    public Text texthp;
    public Text textwave;
    public Text texttime;
    public Text textcoin;
    public Text textcoinshop;
    public Text wavetextshop;
    public Text waveendtext;
    public Slider hpbar;
    public GameObject lucko;

    void Start() {
        lucko = GameObject.Find("Lucko");
        MaxPlayerHealth = 20; hp = 20;
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
            float posX = Random.Range(-19f, 16.5f);
            float posY = Random.Range(-12.8f, 3.5f); 

            int rng = Random.Range(0, zombie.Length);
            //Instantiate(zombie[rng-1], new Vector3(posX, posY, 0f), Quaternion.identity);
            for (int i = 0; i < zombie[rng].cnt; i++)
            {
                float xOffset = Random.Range(0f, 1f); // Random offset between 0 and 1
                float yOffset = Random.Range(0f, 1f); // Random offset between 0 and 1
                Vector3 cur=new Vector3(posX, posY, 0f);
                Vector3 spawnPosition = cur+  new Vector3(xOffset, yOffset, 0);
                NavMeshHit hit;
                bool isOnNavMesh = NavMesh.SamplePosition(spawnPosition, out hit, 0.5f, NavMesh.AllAreas);

                if (!isOnNavMesh)
                {
                    break;
                }

                /*if (spawnPosition.y < -14|| spawnPosition.y > 14 || spawnPosition.x < -14 || spawnPosition.x > 14)
                {
                    i--;
                    continue;
                }*/
                Instantiate(zombie[rng].zombieType, spawnPosition, Quaternion.identity);
            }
            

            yield return new WaitForSeconds(zombie[rng].downtime);
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

    public void playerDie() {
        play = false;
        waveendtext.text = "You made it to wave "+wave.ToString();
        gameOverUI.SetActive(true);
    }

    public void playAgain() {
        SceneManager.LoadScene("zombeki");
    }

    public void newWave() {
        mainUI.SetActive(true);
        shopUI.SetActive(false);
        play = true;

        timer = 50;
        PlayerMovement.speedSlowMultiplier = 1f;
        wave = wave+1;
        if (wave == 1) zombie = wave1;
        else if (wave == 2) zombie = wave2;
        else if (wave == 3) zombie = wave3;
        else if (wave == 4) zombie = wave4;
        else if (wave == 5) zombie = wave5;
        else if (wave == 6) zombie = wave6;
        else if (wave == 7) zombie = wave7;
        else if (wave == 8) zombie = wave8;
        else if (wave == 9) zombie = wave9;
        else if (wave == 10) zombie = wave10;
        else if (wave == 11) zombie = wave11;
        else if (wave == 12) zombie = wave12;
        else if (wave == 13) zombie = wave13;
        else if (wave == 14) zombie = wave14;
        else zombie = wave15;
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

        if (hp <= 0) {
            playerDie();
            hp = 0;
        }
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
