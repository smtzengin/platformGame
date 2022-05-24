using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoarController : MonoBehaviour
{

    [SerializeField] float boarWalkSpeed,boarRunSpeed;
    [SerializeField] Slider boarSlider;

    Animator anim;
    Rigidbody2D rb;

    [SerializeField] float gorusMesafesi = 8f;

    [SerializeField] BoxCollider2D boarCollider;

    [SerializeField] GameObject bloodEffect;


    public bool olduMu;

    public LayerMask playerLayer;

    public int maxSaglik;
    int gecerliSaglik;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        olduMu = false; // halen yaþýyor..
        gecerliSaglik = maxSaglik;
        boarSlider.maxValue = maxSaglik;

    }

    private void Update()
    {
        if (olduMu)
            return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), gorusMesafesi, playerLayer);

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * gorusMesafesi, Color.red);
        transform.localScale = new Vector3(-1, 1, 1);

        if (hit.collider)
        {
            if (hit.collider.CompareTag("Player"))
            {
                rb.velocity = new Vector2(-boarRunSpeed, rb.velocity.y);
                anim.SetBool("kossunMu", true);
            }
            else
            {
                rb.velocity = new Vector2(-boarRunSpeed, rb.velocity.y);
                anim.SetBool("kossunMu", false);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (boarCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer")))
        {
            if (other.CompareTag("Player"))
            {
                anim.SetTrigger("atakYapti");
                other.GetComponent<PlayerController>().GeriTepki();
                other.GetComponent<PlayerHealthController>().CanAzaltFNC();
            }
        }
    }

    public void BoarDead()
    {
        gecerliSaglik--;
        if(gecerliSaglik <= 0)
        {
            olduMu = true;
            anim.SetTrigger("canVerdi");
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            boarSlider.gameObject.SetActive(false);

            Instantiate(bloodEffect, transform.position, transform.rotation);

            //içerisinde bulunan 2 adet collider'i kapatmasý için foreach döngüsü açtýk ve colliderlar içinde dolaþtýk.
            foreach (BoxCollider2D box in GetComponents<BoxCollider2D>())
            {
                box.enabled = false;
            }

            Destroy(gameObject, 2f);
        }
        
    }

    void SliderGuncelle()
    {
        boarSlider.value = gecerliSaglik;
    }
}
