using UnityEngine;
using UnityEngine.Rendering;

public class PoisonCloudScript : MonoBehaviour
{
    public float dmg;
    public float speed = 4f;
    public float deceleration = 0.0001f;
   // private float lifetime = 1.7f;
    public Collider2D gasColliders;
    private void Start()
    {
       // Destroy(gameObject, lifetime);
        InvokeRepeating(nameof(ToggleCollider), 0.6f, 0.6f);

    }
    private void Update()
    {
        /*Vector3 movement = transform.right * speed * Time.deltaTime;
        transform.position += movement;
        if (speed > 0)
        {
            //Debug.Log(speed);
            speed -= deceleration * Time.deltaTime;
            if (speed < 0) speed = 0;
        }*/
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.tag);
        if (other.CompareTag("Enemy"))
        {
            if (!other.gameObject.GetComponent<wasStruckByPlayerPoison>())
            {
                other.gameObject.AddComponent<wasStruckByPlayerPoison>();
                //other.gameObject.GetComponent<wasStruckByPlayerPoison>().dmg = dmg;
            }
        }
    }
    private void ToggleCollider()
    {
        gasColliders.enabled = false;
        gasColliders.enabled = true;
    }
    private void OnDestroy()
    {
        CancelInvoke(nameof(ToggleCollider));
    }
}