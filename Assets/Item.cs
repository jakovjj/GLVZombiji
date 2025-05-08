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

    public bool money() {
        return manager.coin >= price;
    }

    public void i_burek() {if (money()) {MasterControllerScript.playerHealth += 10f; buyItem();}}
    public void i_forneti() {if (money()) {MasterControllerScript.playerRegen += 0.5f; buyItem();}}
    public void i_mathbook() {if (money()) { MasterControllerScript.redLaserDamage += 7f; buyItem();}}
    public void i_physicsbook() {if (money()) { MasterControllerScript.lighningDamage += 2f; buyItem();}}
    public void i_biobook() {if (money()) { MasterControllerScript.playerPoisonDmg += 2f; buyItem();}}
    public void i_logicbook() {if (money()) { MasterControllerScript.hyperbeamDamage += 5f; buyItem();}}
    public void i_informaticsbook() {if (money()) { MasterControllerScript.binaryDamageMult += 0.4f; buyItem();}}
    public void i_shoes() {if (money()) { MasterControllerScript.playerSpeed += 0.9f; buyItem();}}
    public void i_coffee() {if (money()) { MasterControllerScript.playerAttackSpeed += 0.1f; buyItem();}}
    public void i_glvcrown() {if (money()) { MasterControllerScript.playerHealth += 4f; MasterControllerScript.playerAttackSpeed += 0.04f;MasterControllerScript.redLaserDamage += 1f;MasterControllerScript.binaryDamageMult += 0.1f; MasterControllerScript.hyperbeamDamage += 0.7f;MasterControllerScript.playerPoisonDmg += 0.7f;MasterControllerScript.lighningDamage += 1f;MasterControllerScript.knockbackMult += 0.05f;MasterControllerScript.playerRegen += 0.1f; buyItem();}}
    public void i_pendulum() {if (money()) { MasterControllerScript.knockbackMult += 0.2f; buyItem();}}

    public void buyItem() {
        manager.coin -= price;
        manager.shopShuffle();
    }
}