using UnityEngine;

public class BinaryAttackMove : MonoBehaviour
{
    [HideInInspector] public float speed = 4f;
    private float lifetime = 1.7f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        Vector3 movement = transform.right * speed * Time.deltaTime;
        transform.position += movement;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.tag);
        if (other.CompareTag("Enemy"))
        {
            if (!other.gameObject.GetComponent<WasStruckByBinaryAttack>())
            {
                other.gameObject.AddComponent<WasStruckByBinaryAttack>();
            }
            else
            {
                Destroy(other.gameObject.GetComponent<WasStruckByBinaryAttack>());
                other.gameObject.AddComponent<WasStruckByBinaryAttack>();
            }
        }
        if (other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}