using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject thisUnit;
    public delegate void death();
    public death die;
    public virtual void Damage(float amount)
    {

    }

}
