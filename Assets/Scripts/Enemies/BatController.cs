using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    [SerializeField] float takipMesafesi = 8f;

    [SerializeField] float ucusHizi;
    [SerializeField] Transform hedefPlayer;
    [SerializeField] GameObject iksirPrefab;
    Animator anim;
    Rigidbody2D rb;
    BoxCollider2D batCollider;
    Vector2 hareketYonu;

    public float atakYapmaSuresi;
    float atakYapmaSayaci;
    float mesafe;

    public int maxSaglik;
    int gecerliSaglik;


    private void Awake()
    {
        hedefPlayer = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();    
        batCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        gecerliSaglik = maxSaglik;
    }
    private void Update()
    {
        if(atakYapmaSayaci < 0)
        {
            if(hedefPlayer && gecerliSaglik > 0 && !PlayerController.instance.playerCanVerdiMi)
            {
                mesafe = Vector2.Distance(transform.position, hedefPlayer.position);

                if (mesafe < takipMesafesi)
                {
                    anim.SetTrigger("ucusaGecti");

                    hareketYonu = hedefPlayer.position - transform.position;

                    if (transform.position.x > hedefPlayer.position.x)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else if (transform.position.x < hedefPlayer.position.x)
                    {
                        transform.localScale = Vector3.one;
                    }

                    rb.velocity = hareketYonu * ucusHizi;
                }
            }
      
        }
        else
        {
            atakYapmaSayaci -= Time.deltaTime;
        }
    }

    public void CaniAzalt()
    {
        gecerliSaglik--;
        atakYapmaSayaci = atakYapmaSuresi;

        rb.velocity = Vector2.zero;

        if(gecerliSaglik <= 0)
        {
            gecerliSaglik = 0;
            batCollider.enabled = false;
            anim.SetTrigger("canVerdi");
            Instantiate(iksirPrefab,transform.position, Quaternion.identity);
            Destroy(gameObject, 3f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, takipMesafesi);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (batCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer")))
        {
            if (other.CompareTag("Player"))
            {
                rb.velocity = Vector2.zero;
                atakYapmaSayaci = atakYapmaSuresi;
                anim.SetTrigger("atakYapti");

                other.GetComponent<PlayerController>().GeriTepki();
                other.GetComponent<PlayerHealthController>().CanAzaltFNC();
            }
        }
    }
}
