using UnityEngine;

public class wasStruckByPlayerPoison : MonoBehaviour
{
    public float dmg;
    void Start()
    {
        dmg = PoisonAttack.playerPoisonDmg;
        gameObject.GetComponent<enemyHealth>().Damage(dmg, false, Vector2.zero);
        Destroy(this, 0.5f);
    }

 
}
