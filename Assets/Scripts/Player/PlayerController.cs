using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] Transform zeminKontrol;
    [SerializeField] Animator normalAnim,swordAnim;
    [SerializeField] SpriteRenderer normalSprite, swordSprite;
    [SerializeField] GameObject SwordHitBox;
    public LayerMask zeminMask;

    Rigidbody2D rb2D;
    [SerializeField] GameObject normalPlayer, SwordPlayer;

    public float moveSpeed;
    public float jumpPower;
    bool zemindeMi;
    bool doubleJumpOlsunMu;

    [SerializeField] float geriTepkiSuresi, geriTepkiGucu;
    float geriTepkiSayaci;
    bool yonSagdaMi;
    public bool playerCanVerdiMi;
    bool swordAttack;
    public float attackTimer;
    public float attackCoolDown;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        swordAttack = false;
        instance = this;
        playerCanVerdiMi = false;
        attackTimer = 0;
        attackCoolDown = 0.5f;
        SwordHitBox.SetActive(false);
    }

    private void Update()
    {
        if (playerCanVerdiMi)
            return;
        
        if (geriTepkiSayaci <= 0)
        {
            Move();
            Jump();
            Flip();

            if (normalPlayer.activeSelf)
            {
                normalSprite.color = new Color(normalSprite.color.r, normalSprite.color.g, normalSprite.color.b, 1f);
            }
            if (SwordPlayer.activeSelf)
            {
                swordSprite.color = new Color(swordSprite.color.r, swordSprite.color.g, swordSprite.color.b, 1f);

            }

            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            if(attackTimer < 0)
            {
                attackTimer = 0;
            }

            if (Input.GetMouseButton(0) && SwordPlayer.activeSelf)
            {
                if(attackTimer == 0)
                {
                    swordAttack = true;
                    SwordHitBox.SetActive(true);
                    attackTimer = attackCoolDown;
                }

            }
            else
            {
                swordAttack = false;
            }



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

        if (normalPlayer.activeSelf)
        {
            normalAnim.SetBool("zemindeMi", zemindeMi);
            normalAnim.SetFloat("hareketHizi", Mathf.Abs(rb2D.velocity.x));
        }

        if (SwordPlayer.activeSelf)
        {
            swordAnim.SetBool("zemindeMi", zemindeMi);
            swordAnim.SetFloat("hareketHizi", Mathf.Abs(rb2D.velocity.x));   
            
        }

        if (swordAttack && SwordPlayer.activeSelf)
        {
            swordAnim.SetTrigger("SwordAttack");
        }

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
        if (normalPlayer.activeSelf)
        {
            normalSprite.color = new Color(normalSprite.color.r, normalSprite.color.g, normalSprite.color.b, 0.8f);
        }
        if (SwordPlayer.activeSelf)
        {
            swordSprite.color = new Color(swordSprite.color.r, swordSprite.color.g, swordSprite.color.b, 0.8f);
        }

        rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        
    }
    public void PlayerCanVerdi()
    {
        rb2D.velocity = Vector2.zero;
        playerCanVerdiMi = true;
        if (normalPlayer.activeSelf)
        {
            normalAnim.SetTrigger("canVerdi");
        }
        if (SwordPlayer.activeSelf)
        {
            swordAnim.SetTrigger("canVerdi");
        }

        StartCoroutine(PlayerYokEtSahneYenile());
    }

    IEnumerator PlayerYokEtSahneYenile()
    {
        yield return new WaitForSeconds(2f);

        GetComponentInChildren<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NormalToSword()
    {
        normalPlayer.SetActive(false);
        SwordPlayer.SetActive(true);
    }

   
}
