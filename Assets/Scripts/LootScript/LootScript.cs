using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour
{
    [SerializeField] bool kilicMi, mizrakMi;


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
            Destroy(gameObject);
        }
    }
}
