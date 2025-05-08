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


    public void i_burek() {MasterControllerScript.playerHealth += 10f; buyItem();}
    public void i_forneti() { MasterControllerScript.playerRegen += 0.1f; buyItem();}
    public void i_mathbook() { MasterControllerScript.redLaserDamage += 7f; buyItem();}
    public void i_physicsbook() { MasterControllerScript.lighningDamage += 2f; buyItem();}
    public void i_biobook() { MasterControllerScript.playerPoisonDmg += 2f; buyItem();}
    public void i_logicbook() { MasterControllerScript.hyperbeamDamage += 5f; buyItem();}
    public void i_informaticsbook() { MasterControllerScript.binaryDamageMult += 0.4f; buyItem();}
    public void i_shoes() { MasterControllerScript.playerSpeed += 0.9f; buyItem();}
    public void i_coffee() { MasterControllerScript.playerAttackSpeed += 0.1f; buyItem();}
    public void i_glvcrown() { MasterControllerScript.playerHealth += 4f; MasterControllerScript.playerAttackSpeed += 0.04f;MasterControllerScript.redLaserDamage += 1f;MasterControllerScript.binaryDamageMult += 0.1f; MasterControllerScript.hyperbeamDamage += 0.7f;MasterControllerScript.playerPoisonDmg += 0.7f;MasterControllerScript.lighningDamage += 1f;MasterControllerScript.knockbackMult += 0.05f;MasterControllerScript.playerRegen += 0.02f; buyItem();}
    public void i_pendulum() { MasterControllerScript.knockbackMult += 0.2f; buyItem();}

    public void buyItem() {
        manager.coin -= price;
        manager.shopShuffle();
    }
}