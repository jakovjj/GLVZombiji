using UnityEngine;

public class FreezeAttack : MonoBehaviour
{
    public float freezePercentage;
    public float freezeDuration;
        public void Freeze(GameObject player)
        {
            IsFrozen existingComponent = player.GetComponent<IsFrozen>();

            if (existingComponent == null)
            {
                // If the component does not exist, add a new one
                IsFrozen newComponent=player.AddComponent<IsFrozen>();
                newComponent.freezePercentage = freezePercentage;
                newComponent.freezeDuration = freezeDuration;
            }
            else
            {
                // If the component exists, replace it with a new instance
                Destroy(existingComponent);
                IsFrozen newComponent = player.AddComponent<IsFrozen>();
                newComponent.freezePercentage = freezePercentage;
                newComponent.freezeDuration = freezeDuration;   
            }
        }
}
