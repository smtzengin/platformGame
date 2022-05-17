using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform zeminKontrol;
    [SerializeField] Animator anim;
    public LayerMask zeminMask;

    Rigidbody2D rb2D;

    public float moveSpeed;
    public float jumpPower;
    bool zemindeMi;
    bool doubleJumpOlsunMu;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        Jump();
        anim.SetBool("zemindeMi", zemindeMi);
        anim.SetFloat("hareketHizi", Mathf.Abs(rb2D.velocity.x));
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        rb2D.velocity = new Vector2(h*moveSpeed,rb2D.velocity.y);

        if(rb2D.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(rb2D.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
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
}
