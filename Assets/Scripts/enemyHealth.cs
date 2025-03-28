using UnityEngine;

public class enemyHealth : Health
{
    [SerializeField] GameObject healthBar;
    public int maxHealth;
    public GameObject healthBarLocation;
    public GameObject coinDrop;
    [HideInInspector] public GameObject managerGO;
    public float health;
    [SerializeField] float regen;
    public AudioSource zombieHurt;
    public AudioSource zombieDeath;
    public void Awake()
    {
        managerGO = GameObject.Find("gameManager");
        health = maxHealth;
        die = destruction;
        GameObject newHealthBar = GameObject.Instantiate<GameObject>(healthBar, healthBarLocation.transform);
        newHealthBar.GetComponent<HealthBar>().host = thisUnit;
    }
    void Update()
    {
        if (health <= 0)
        {
            die();
        }
        health += regen * Time.deltaTime;
        if (health > maxHealth) health = maxHealth;
    }
    public override void Damage(float amount)
    {
        health -= amount;
        base.Damage(amount);
        //zombieHurt.Play();
    }
    public void destruction()
    {
        //zombieDeath.Play();
        Instantiate(coinDrop, transform.position, Quaternion.identity, managerGO.transform);
        Destroy(thisUnit);
    }
}
