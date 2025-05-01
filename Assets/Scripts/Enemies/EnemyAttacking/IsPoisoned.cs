using UnityEngine;

public class IsPoisoned : MonoBehaviour
{
    public float poisonDamagePerTick;
    public float poisonDuration;

    void Start()
    {
        InvokeRepeating("ApplyPoisonDamage", 0f, 0.4f); // Calls the function every 0.2s
        Destroy(this, poisonDuration); // Removes poison effect after duration ends
    }

    void ApplyPoisonDamage()
    {
        GameObject.Find("gameManager").GetComponent<manager>().getDamage(poisonDamagePerTick, false, Vector2.zero);
    }
}