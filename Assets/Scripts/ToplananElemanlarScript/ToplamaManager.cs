using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToplamaManager : MonoBehaviour
{
    [SerializeField] bool coinMi, iksirMi;

    [SerializeField] GameObject patlamaEffect;

    bool toplandiMi;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (coinMi)
        {
            if (other.CompareTag("Player") && !toplandiMi)
            {
                toplandiMi = true;
                GameManager.instance.toplananCoinAdet++;
                UIManager.instance.CoinAdetGuncelle();

                Destroy(this.gameObject);
                Instantiate(patlamaEffect, transform.position, Quaternion.identity);
            }
        }
        if (iksirMi)
        {
            if (other.CompareTag("Player") && !toplandiMi)
            {
                PlayerHealthController.instance.CaniArttir();

                Destroy(this.gameObject);
                Instantiate(patlamaEffect, transform.position, Quaternion.identity);
            }
        }
    }
}
