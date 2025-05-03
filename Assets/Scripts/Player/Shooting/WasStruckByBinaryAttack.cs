using System.Collections;
using UnityEngine;


public class WasStruckByBinaryAttack : MonoBehaviour
{
    public float lengthOfEffect;
    void Start()
    {
        
        flashSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        Init();
        StartCoroutine(damageFlash());
        Destroy(this, 3f);
    }

  #region damage Flash
    [SerializeField] private Color flashColor = Color.magenta;
    [SerializeField] private float flashTime = 3f;
    private SpriteRenderer[] flashSpriteRenderers;
    private Material[] flashMaterials;

    private IEnumerator damageFlash()
    {
        
        SetFlashColor();

        float currentFlash = 0f;
        float timerFlash = 0f;
        
        while (timerFlash < flashTime)
        {
           // Debug.Log(timerFlash);

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
    // Update is called once per frame
    void Update()
    {
        
    }

}
