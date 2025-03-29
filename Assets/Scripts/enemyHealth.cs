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
        flashSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        Init();

        managerGO = GameObject.Find("gameManager");
        health = maxHealth;
        die = destruction;
        GameObject newHealthBar = GameObject.Instantiate<GameObject>(healthBar, healthBarLocation.transform);
        newHealthBar.GetComponent<HealthBar>().host = thisUnit;
    }

    #region damage Flash
        [SerializeField] private Color flashColor = Color.white;
        [SerializeField] private float flashTime = 0.25f;
        private SpriteRenderer[] flashSpriteRenderers;
        private Material[] flashMaterials;

        private IEnumerator damageFlash() {
            SetFlashColor();

            float currentFlash = 0f;
            float timerFlash = 0f;
            while (timerFlash < flashTime) {
                timerFlash += Time.deltaTime;
                currentFlash = Mathf.Lerp(1f, 0f, timerFlash/flashTime);

                for (int i = 0; i < flashMaterials.Length; i++) {
                    flashMaterials[i].SetFloat("_flashVal", currentFlash);
                }

                yield return null;
            }
        }

        private void SetFlashColor() {
            for (int i = 0; i < flashMaterials.Length; i++) {
                flashMaterials[i].SetColor("_flashColorWhite", flashColor);
            }
        }

        private void Init() {
            flashMaterials = new Material[flashSpriteRenderers.Length];

            for (int i = 0; i < flashSpriteRenderers.Length; i++) {
                flashMaterials[i] = flashSpriteRenderers[i].material;
            }
        }
    #endregion

    void Update()
    {
        if (health <= 0)
        {
            die();
        }
        health += regen * Time.deltaTime;
        if (health > maxHealth) health = maxHealth;
    }
    public void Damage(float amount,Vector3 dir)
    {
        health -= amount;
        StartCoroutine(damageFlash());
        //napravi knockback kak god zelis
        gameObject.GetComponent<EnemyMove>().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
       // Vector2 dir = gameObject.transform.position - hitTrigger.transform.position;
        gameObject.GetComponent<Rigidbody2D>().AddForce(dir * 5, ForceMode2D.Impulse);
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
