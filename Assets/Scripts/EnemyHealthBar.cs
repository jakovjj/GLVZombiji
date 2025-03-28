using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject host = null;
    [SerializeField]GameObject enemyHealthBar;
    Vector2 defaultSize;
    // Update is called once per frame
    private void Start()
    {
        defaultSize = enemyHealthBar.transform.localScale;
    }
    void Update()
    {
        if(host != null)
        {
            enemyHealthBar.transform.localScale = new Vector2(defaultSize.x * host.GetComponent<enemyHealth>().health / host.GetComponent<enemyHealth>().maxHealth, enemyHealthBar.transform.localScale.y);
        }
    }
}
