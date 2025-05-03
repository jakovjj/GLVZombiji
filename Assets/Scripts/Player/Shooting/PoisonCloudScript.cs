using UnityEngine;
using UnityEngine.Rendering;

public class PoisonCloudScript : MonoBehaviour
{
    public float dmg;

    void OnTriggerStay2D(Collider2D other)
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
}