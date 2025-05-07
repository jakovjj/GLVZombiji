using UnityEngine;

public class GasMove : MonoBehaviour
{
    public float speed = 4f;
    public float deceleration = 0.0001f;
    private float lifetime = 1.7f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        Vector3 movement = transform.right * speed * Time.deltaTime;
        transform.position += movement;
        if (speed > 0)
        {
            //Debug.Log(speed);
            speed -= deceleration * Time.deltaTime;
            if (speed < 0) speed = 0;
        }

    }
}