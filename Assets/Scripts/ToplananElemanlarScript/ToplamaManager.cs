using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToplamaManager : MonoBehaviour
{
    [SerializeField] 
    bool coinMi;

    [SerializeField] GameObject coinEffect;

    bool toplandiMi;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !toplandiMi)
        {
            toplandiMi = true;
            Destroy(this.gameObject);
            Instantiate(coinEffect, transform.position, Quaternion.identity);
        }
    }
}
