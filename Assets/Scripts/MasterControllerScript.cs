using UnityEngine;

public class MasterControllerScript : MonoBehaviour
{
    [Header("Player basics")]
    public float playerSpeed;
    public float playerHealth;

    [Header("Crveni Laser")]
    public static float redLaserDamage=1;

    private void Update()
    {
        PlayerMovement.PlayerSpeed = playerSpeed;
        manager.MaxPlayerHealth= playerHealth;
    }
}
