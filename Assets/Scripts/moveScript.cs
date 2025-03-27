using UnityEngine;

public class moveScript : MonoBehaviour
{
    public Transform target;
    public float step;
    public float speed;

    void Update()
    {
        step = speed*Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}