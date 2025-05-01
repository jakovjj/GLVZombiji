using UnityEngine;
public class IsFrozen : MonoBehaviour
{
    public float freezePercentage;
    public float freezeDuration;
    private void Start()
    {
        Destroy(this,freezeDuration);
    }
}
