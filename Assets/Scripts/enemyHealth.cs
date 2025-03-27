using UnityEngine;

public class enemyHealth : Health
{
    [SerializeField] GameObject healthBar;
    public int maxHealth;
    public GameObject healthBarLocation;
    public float health;
    [SerializeField] float regen;
    public void Awake()
    {
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
    }
    public void destruction()
    {
        Destroy(thisUnit);
    }
}
