using UnityEngine;
public class IsFrozen : MonoBehaviour
{
    public float freezePercentage;
    public float freezeDuration;
    public GameObject freezeParticles;
    private GameObject currentFreezeParticles;
    private void Start()
    {
        freezeParticles = Resources.Load<GameObject>("FreezeParticles");

        currentFreezeParticles = Instantiate(freezeParticles, gameObject.transform.position, Quaternion.identity, gameObject.transform);

        Destroy(this,freezeDuration);
    }
    private void OnDestroy()
    {
        Destroy(currentFreezeParticles);
    }
}
