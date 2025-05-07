using UnityEngine;

public class EnemyPoisonAttack : MonoBehaviour
{
    public float poisonDamagePerTick;
    public float poisonDuration;
    public void Poison(GameObject player)
    {
        IsPoisoned existingComponent = player.GetComponent<IsPoisoned>();
        if (existingComponent == null)
        {
            // If the component does not exist, add a new one
            IsPoisoned newComponent = player.AddComponent<IsPoisoned>();
            newComponent.poisonDamagePerTick =poisonDamagePerTick;
            newComponent.poisonDuration = poisonDuration;
        }
        else
        {
            // If the component exists, replace it with a new instance
            Destroy(existingComponent);
            IsPoisoned newComponent = player.AddComponent<IsPoisoned>();
            newComponent.poisonDamagePerTick =poisonDamagePerTick;
            newComponent.poisonDuration = poisonDuration;
        }
    }
}
