using UnityEngine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class Item : MonoBehaviour
{
    public int price;
    public string desc;
    public string name;
    public manager manager;

    void Start() {
        this.gameObject.transform.GetChild(5).GetComponent<Text>().text = name;
        this.gameObject.transform.GetChild(6).GetComponent<Text>().text = price.ToString();
        this.gameObject.transform.GetChild(7).GetComponent<Text>().text = desc;
    }


    void Burek() {

    }

}