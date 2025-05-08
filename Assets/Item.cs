using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class Item : MonoBehaviour
{
    public int price;
    public manager manager;

    void Start() {
        this.gameObject.transform.GetChild(6).GetComponent<Text>().text = price.ToString();
        manager = GameObject.Find("gameManager").GetComponent<manager>();
    }


    public void i_burek() {manager.MaxPlayerHealth += 10; buyItem();}
    public void i_forneti() {manager.MaxPlayerHealth += 10; buyItem();}
    public void i_mathbook() {manager.MaxPlayerHealth += 10; buyItem();}
    public void i_physicsbook() {manager.MaxPlayerHealth += 10; buyItem();}
    public void i_biobook() {manager.MaxPlayerHealth += 10; buyItem();}
    public void i_logicbook() {manager.MaxPlayerHealth += 10; buyItem();}
    public void i_informaticsbook() {manager.MaxPlayerHealth += 10; buyItem();}
    public void i_shoes() {manager.MaxPlayerHealth += 10; buyItem();}
    public void i_coffee() {manager.MaxPlayerHealth += 10; buyItem();}
    public void i_glvcrown() {manager.MaxPlayerHealth += 10; buyItem();}
    public void i_pendulum() {manager.MaxPlayerHealth += 10; buyItem();}

    public void buyItem() {
        manager.coin -= price;
        manager.shopShuffle();
    }
}