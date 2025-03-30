using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class light_follow : MonoBehaviour
{
    public Transform player;

    void Start() { player = this.transform.parent; }

    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime*10);
    }
}
