using System.Collections;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    private EnemyMove enemyMove;
    public float damage;
    public float timeBetweenAttack;
    private bool canAttack=true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyMove.isInInteractionRadius && canAttack)
        {
            Vector2 direction= GameObject.Find("Lucko").transform.position-gameObject.transform.position;
            direction=direction.normalized;
            GameObject.Find("gameManager").GetComponent<manager>().getDamage(damage,false,direction);
            canAttack = false;
            StartCoroutine(resetAttack(timeBetweenAttack));
        }
    }
    public IEnumerator resetAttack(float tm)
    {
        yield return new WaitForSeconds(tm);
        canAttack=true;
    }
}
