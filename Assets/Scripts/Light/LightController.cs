using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] SpriteRenderer lightSpriteRenderer;
    [SerializeField] Sprite lightOnSprite, lightOffSprite;


    private void Awake()
    {
        lightSpriteRenderer.sprite = lightOffSprite;  
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            lightSpriteRenderer.sprite = lightOnSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {     
            Invoke("FeneriKapat", .5f);
        }
    }

    void FeneriKapat()
    {
        lightSpriteRenderer.sprite = lightOffSprite;
    }
}
