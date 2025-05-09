using UnityEngine;

public class MasterControllerScript : MonoBehaviour
{
    [Header("Player basics")]
    public static float playerSpeed;
    public static float playerRegen;
    public static float playerHealth;

    [Header("Crveni Laser")]
    public static float redLaserDamage;
    public static float binaryDamageMult;
    public static float lighningDamage;
    public static float playerPoisonDmg;
    public static float hyperbeamDamage;
    public static float playerAttackSpeed;
    public static float knockbackMult;

    private void Start()
    {
        playerHealth = 50f;
        redLaserDamage = 25f;
        binaryDamageMult = 2f;
        lighningDamage = 9f;
        playerPoisonDmg = 9f;
        hyperbeamDamage = 20f;
        playerAttackSpeed = 1f;
        playerSpeed = 6f;
        knockbackMult = 1f;
    }

    private void Update()
    {
        PlayerMovement.PlayerSpeed = playerSpeed;
    }
}
