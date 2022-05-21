using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] BoxCollider2D swordAttackBox;
    [SerializeField] GameObject parlamaEfekti;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (swordAttackBox.IsTouchingLayers(LayerMask.GetMask("DusmanLayer")))
        {
            if (other.CompareTag("Orumcek"))
            {
                if (parlamaEfekti)
                {
                    Instantiate(parlamaEfekti, other.transform.position, Quaternion.identity);
                }
                StartCoroutine(other.GetComponent<SpiderController>().GeriTepkiFNC());
                
            }
        }

        if (swordAttackBox.IsTouchingLayers(LayerMask.GetMask("DusmanLayer")))
        {
            if (other.CompareTag("Bat"))
            {
                if (parlamaEfekti)
                {
                    Instantiate(parlamaEfekti, other.transform.position, Quaternion.identity);
                }
                other.GetComponent<BatController>().CaniAzalt();
            }
        }
    }
}
