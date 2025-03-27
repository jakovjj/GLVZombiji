using UnityEngine;

public class playerHealth : Health
{
    public manager mngr;
    public GameObject healthBarLocation;
    public float health;
    public void Awake()
    {
        mngr.hp = mngr.mhp;
    }
    void Update()
    {
        if (mngr.hp <= 0)
        {
            die();
        }
        mngr.hp += mngr.rgn * Time.deltaTime;
        if (mngr.hp > mngr.mhp) mngr.hp = mngr.mhp;
    }
    public override void Damage(float amount)
    {
        amount = mngr.calcDamage(amount);
        base.Damage(amount);
    }
}
