using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class manager : MonoBehaviour
{
    [HideInInspector] public int hp;
    [HideInInspector] public int mhp;
    [HideInInspector] public int dmg;
    [HideInInspector] public int coin;

    [HideInInspector] public int wave; 
    [HideInInspector] public int timer;

    public Text texthp;
    public Text textwave;
    public Text texttime;
    public Text textcoin;
    public Slider hpbar;

    private IEnumerator tickvar;

    void Start() {
        mhp = 20; hp = mhp;
        dmg = 1; coin = 0;
        wave = 0; timer = 30;
        tickvar = tickTimer();

        newWave();
    }
   
    private IEnumerator tickTimer() {
        while (timer > 0) {
            yield return new WaitForSeconds(1);
            timer--;
        }

        newWave(); //kasnije treba prebaciti na neki shop ili nes
    }

    void newWave() {
        timer = 50;
        wave++;

        StartCoroutine(tickvar);
    }

    void Update() {
        texthp.text = hp.ToString()+"/"+mhp.ToString();
        textcoin.text = coin.ToString();
        hpbar.value = hp*100/mhp;

        textwave.text = "Wave "+wave.ToString();
        texttime.text = timer.ToString();
    }
}
