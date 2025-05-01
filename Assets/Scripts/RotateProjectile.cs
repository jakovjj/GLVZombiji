using UnityEngine;


public class RotateProjectile : MonoBehaviour
{
    public float rotationSpeed = 720f; // Degrees per second

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
