using UnityEngine;

public class ChainLightningScript : MonoBehaviour
{
    private Collider2D coll;

    public LayerMask enemyLayer;

    public GameObject chainLightningEffect;

    public GameObject beenStruck;
    public int amountToChain;
    private GameObject startObject; public GameObject endObject;
    private Animator ani;
    public ParticleSystem parti;
    private int singleSpawns;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        if (amountToChain == 0) Destroy(gameObject);
        coll = GetComponent<CircleCollider2D>();
        ani = GetComponent<Animator>();
        startObject =gameObject;
        singleSpawns = 1;
    }
    private void Update()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !ani.IsInTransition(0))
        {
            Destroy(gameObject);
        }

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyLayer == (enemyLayer | (1 << collision.gameObject.layer)) && !collision.GetComponentInChildren<EnemyStruck>()) {
            
            if (singleSpawns != 0)
            {


                endObject = collision.gameObject;
                amountToChain -= 1;
                Instantiate(chainLightningEffect, collision.gameObject.transform.position, Quaternion.identity/*, collision.gameObject.transform*/);
                Instantiate(beenStruck, collision.gameObject.transform);

                collision.gameObject.GetComponent<enemyHealth>().Damage(MasterControllerScript.lighningDamage,false, Vector3.zero);
                ani.StopPlayback();
                coll.enabled = false;
                singleSpawns--;
                parti.Play();

                var emitParams = new ParticleSystem.EmitParams(); 

                emitParams.position = startObject.transform.position;
                parti.Emit(emitParams, 1);
                emitParams.position = endObject.transform.position;
                parti.Emit(emitParams, 1);

                emitParams.position=(startObject.transform.position+endObject.transform.position)/2;
                parti.Emit(emitParams, 1);
                Destroy(gameObject, 1f);
             
            }
        }
    }
}
