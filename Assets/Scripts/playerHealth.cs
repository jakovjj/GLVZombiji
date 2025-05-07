using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public GameObject thisUnit;
    public delegate void death();
    public death die;
    public manager mngr;
    public GameObject healthBarLocation;
    public static float PlayerHealth;
    public void Awake()
    {
      //mngr.hp = mngr.mhp;
    }
    void Update()
    {
        if (mngr.hp <= 0)
        {
            die();
        }
        mngr.hp += mngr.rgn * Time.deltaTime;
      //  if (mngr.hp > mngr.mhp) mngr.hp = mngr.mhp;
    }
    public void Damage(float amount/*,Vector2 knockback*/)
    {
        amount = mngr.calcDamage(amount);
        //dodaj sta oces da radi knockback
    }
}
