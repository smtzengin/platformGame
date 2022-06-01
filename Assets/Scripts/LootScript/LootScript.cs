using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour
{
    [SerializeField] bool kilicMi, mizrakMi,yayMi;


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {

            if (other != null && kilicMi)
            {
                other.GetComponent<PlayerController>().NormalToSword();
                
            }

            if (other != null && mizrakMi)
            {

                other.GetComponent<PlayerController>().CloseAllOpenSpear();
            }
            if (other != null && yayMi)
            {
                other.GetComponent<PlayerController>().CloseAllOpenBow();
            }
            Destroy(gameObject);
        }
    }
}
