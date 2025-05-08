using UnityEngine;
using System.Collections;
public class playerDamageFlash : MonoBehaviour
{
    public GameObject thisUnit;
    
    public float damageMultiplier; void Start()
    {
        Init();
        flashSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < flashMaterials.Length; i++)
        {
            flashMaterials[i].SetFloat("_flashBinaryVal", 1f);
        }
    }

    // Update is called once per frame
    public void takeDamage()
    {
        StartCoroutine(damageFlash());
    }
    #region damage Flash
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float flashTime = 0.25f;
    private SpriteRenderer[] flashSpriteRenderers;
    private Material[] flashMaterials;

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

    private void Init()
    {
        flashMaterials = new Material[flashSpriteRenderers.Length];

        for (int i = 0; i < flashSpriteRenderers.Length; i++)
        {
            flashMaterials[i] = flashSpriteRenderers[i].material;
        }
    }
    #endregion
}
