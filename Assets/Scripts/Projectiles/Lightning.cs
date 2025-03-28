using UnityEngine;

public class Lightning : ProjectileBehaviour
{
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void Start()
    {
        base.Start();
        OnHit += Bounce;
    }
    public void Bounce(GameObject target)
    {

    }
}
