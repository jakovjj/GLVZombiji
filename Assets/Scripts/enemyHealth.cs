using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class enemyHealth : MonoBehaviour
{
    public GameObject thisUnit;
    public delegate void death();
    public death die;
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
    public void Damage(float amount,GameObject hitTrigger)
    {
        health -= amount;
        //napravi knockback kak god zelis
        gameObject.GetComponent<EnemyMove>().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        Vector3 dir = gameObject.transform.position - hitTrigger.transform.position;
        gameObject.GetComponent<Rigidbody2D>().AddForce(dir * 15, ForceMode2D.Impulse);
        StartCoroutine(resetForce());
        //     zombieHurt.Play();
    }

    public IEnumerator resetForce()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        gameObject.GetComponent<EnemyMove>().enabled = true;
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
    }

    public void destruction()
    {
   //     zombieDeath.Play();
        Instantiate(coinDrop, transform.position, Quaternion.identity, managerGO.transform);
        Destroy(thisUnit);
    }
}
