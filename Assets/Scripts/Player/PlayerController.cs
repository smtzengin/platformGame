using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] Transform zeminKontrol;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer sr;
    public LayerMask zeminMask;

    Rigidbody2D rb2D;

    public float moveSpeed;
    public float jumpPower;
    bool zemindeMi;
    bool doubleJumpOlsunMu;
    [SerializeField] float geriTepkiSuresi, geriTepkiGucu;
    float geriTepkiSayaci;
    bool yonSagdaMi;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        instance = this;
    }

    private void Update()
    {
        if (geriTepkiSayaci <= 0)
        {
            Move();
            Jump();
            Flip();
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        }
        else
        {
            geriTepkiSayaci -= Time.deltaTime;
            if (yonSagdaMi)
            {
                rb2D.velocity = new Vector2(-geriTepkiGucu, rb2D.velocity.y);
            }
            else
            {
                rb2D.velocity = new Vector2(geriTepkiSayaci,rb2D.velocity.y);
            }
        }

        anim.SetBool("zemindeMi", zemindeMi);
        anim.SetFloat("hareketHizi", Mathf.Abs(rb2D.velocity.x));

    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        rb2D.velocity = new Vector2(h*moveSpeed,rb2D.velocity.y);
        
    }

    void Flip()
    {
        if (rb2D.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            yonSagdaMi = false;
        }
        else if (rb2D.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            yonSagdaMi = true;
        }
    }
    void Jump()
    {
        zemindeMi = Physics2D.OverlapCircle(zeminKontrol.position, 0.2f, zeminMask);

        if(Input.GetButtonDown("Jump") && (zemindeMi || doubleJumpOlsunMu))
        {
            if (zemindeMi)
            {
                doubleJumpOlsunMu = true;
            }
            else
            {
                doubleJumpOlsunMu = false;
            }
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpPower);   
        }
    }

    public void GeriTepki()
    {
        geriTepkiSayaci = geriTepkiSuresi;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.8f);
        rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        
    }


}
