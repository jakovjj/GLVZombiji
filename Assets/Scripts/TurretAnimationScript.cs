using UnityEngine;

public class TurretAnimationScript : MonoBehaviour
{
    private Animator animator;
    private EnemyMove enemyMove;
    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyMove = GetComponent<EnemyMove>();
    }
    private void Update()
    {
        if (enemyMove.isInSight && enemyMove.hasDetected)
        {
            animator.SetBool("ChargeUp", true);
        }
        else
        {
            animator.SetBool("ChargeUp", false);
        }
    }
}
