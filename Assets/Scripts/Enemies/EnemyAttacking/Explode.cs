using UnityEngine;

public class Explode : MonoBehaviour
{
    private EnemyMove enemyMove;
    public GameObject ExplosionParticles;
    public float damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(enemyMove.isInInteractionRadius);
        if (enemyMove.isInInteractionRadius)
        {
            Debug.Log("a");
            Instantiate(ExplosionParticles, transform.position, transform.rotation);
            GameObject.Find("gameManager").GetComponent<manager>().getDamage(damage,false,Vector2.zero);
            Destroy(transform.parent.gameObject);
        }
    }
}
