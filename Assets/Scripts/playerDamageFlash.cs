using UnityEngine;
using System.Collections;
public class playerDamageFlash : MonoBehaviour
{
    public GameObject thisUnit;
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float flashTime = 0.25f;
    public SpriteRenderer[] flashSpriteRenderers;
    public Material[] flashMaterials;
    
    
    void Start() {
        gameObject.transform.GetChild(3).gameObject.SetActive(true);
        gameObject.transform.GetChild(4).gameObject.SetActive(true);
        gameObject.transform.GetChild(5).gameObject.SetActive(true);
        flashSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        Init();
        gameObject.transform.GetChild(3).gameObject.SetActive(false);
        gameObject.transform.GetChild(4).gameObject.SetActive(false);
        gameObject.transform.GetChild(5).gameObject.SetActive(false);
        for (int i = 0; i < flashMaterials.Length; i++) {flashMaterials[i].SetFloat("_flashBinaryVal", 1f);}
    }

    public void takeDamage()
    {
       // Debug.Log("aa");
        StartCoroutine(damageFlash());
    }
    #region damage Flash

    private IEnumerator damageFlash()
    {
        SetFlashColor();

        float currentFlash = 0f;
        float timerFlash = 0f;
        while (timerFlash < flashTime)
        {
            timerFlash += Time.deltaTime;
            currentFlash = Mathf.Lerp(1f, 0f, timerFlash / flashTime);

            for (int i = 0; i < flashMaterials.Length; i++)
            {
                flashMaterials[i].SetFloat("_flashVal", currentFlash);
            }

            yield return null;
        }
    }

    private void SetFlashColor()
    {
        for (int i = 0; i < flashMaterials.Length; i++)
        {
            flashMaterials[i].SetColor("_flashColorWhite", flashColor);
        }
    }

    public void Init()
    {
        flashMaterials = new Material[flashSpriteRenderers.Length];

        for (int i = 0; i < flashSpriteRenderers.Length; i++)
        {
            flashMaterials[i] = flashSpriteRenderers[i].material;
        }
    }
    #endregion
}
