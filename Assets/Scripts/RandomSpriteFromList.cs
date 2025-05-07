using UnityEngine;

public class RandomSpriteFromList : MonoBehaviour
{
    public Sprite[] spriteList; 
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteList.Length > 0 && spriteRenderer != null)
        {
            //promijeni sprite
            int index = Random.Range(0, spriteList.Length);
            spriteRenderer.sprite = spriteList[index];
            //ako je trostruka banana povecaj damage
            if (index == 2)
            {
                gameObject.transform.parent.GetComponent<ProjectileBehaviour>().damage *= 2;
            }
        }
    }

}
