using UnityEngine;
using static UnityEngine.ParticleSystem;

public class IsPoisoned : MonoBehaviour
{
    public float poisonDamagePerTick;
    public float poisonDuration;
    public GameObject poisonParticles;
    private GameObject currentPoisonParticles;
    void Start()
    {
        poisonParticles = Resources.Load<GameObject>("PlayerPoisoned");
        currentPoisonParticles = Instantiate(poisonParticles, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        InvokeRepeating("ApplyPoisonDamage", 0f, 0.4f); // Calls the function every 0.2s
        Destroy(this, poisonDuration); // Removes poison effect after duration ends
    }
    private void OnDestroy()
    {
        Destroy(currentPoisonParticles);
    }
    void ApplyPoisonDamage()
    {
        GameObject.Find("gameManager").GetComponent<manager>().getDamage(poisonDamagePerTick, false, Vector2.zero);
    }
}