using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] Transform zeminKontrol;
    [SerializeField] Animator normalAnim,swordAnim,spearAnim;
    [SerializeField] SpriteRenderer normalSprite, swordSprite,spearSprite;
    [SerializeField] GameObject SwordHitBox;
    public LayerMask zeminMask;

    Rigidbody2D rb2D;
    [SerializeField] GameObject normalPlayer, SwordPlayer,spearPlayer;
    [SerializeField] GameObject atilacakSpear;
    [SerializeField] Transform spearCikisNoktasi;

    public float moveSpeed;
    public float jumpPower;
    bool zemindeMi;
    bool doubleJumpOlsunMu;

    [SerializeField] float geriTepkiSuresi, geriTepkiGucu;
    float geriTepkiSayaci;
    bool yonSagdaMi;
    public bool playerCanVerdiMi;
    bool swordAttack;
    bool spearAttack;
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

            GeriTepkiDuzeltmeBirF(normalPlayer, normalSprite);
            GeriTepkiDuzeltmeBirF(SwordPlayer, swordSprite);
            GeriTepkiDuzeltmeBirF(spearPlayer, spearSprite);

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

            if (Input.GetKeyDown(KeyCode.X) && spearPlayer.activeSelf)
            {
                spearAnim.SetTrigger("mizrakAtti");
                Invoke("MizragiFirlat",0.15f);
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
        if (spearPlayer.activeSelf)
        {
            spearAnim.SetBool("zemindeMi", zemindeMi);
            spearAnim.SetFloat("hareketHizi", Mathf.Abs(rb2D.velocity.x));
        }

        if (swordAttack && SwordPlayer.activeSelf)
        {
            swordAnim.SetTrigger("SwordAttack");
        }
        if (spearAttack && spearPlayer.activeSelf)
        {
            spearAnim.SetTrigger("atakYapti");
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

    void MizragiFirlat()
    {
        GameObject spear = Instantiate(atilacakSpear, spearCikisNoktasi.position, spearCikisNoktasi.rotation);
        spear.transform.localScale = transform.localScale;
        spear.GetComponent<Rigidbody2D>().velocity = spearCikisNoktasi.right * transform.localScale.x * 8f;
        Invoke("CloseAllOpenNormal", 0.15f);
        Destroy(spear,3f);
    }

    public void GeriTepki()
    {
        geriTepkiSayaci = geriTepkiSuresi;
        
        GeriTepkiDuzeltme(normalPlayer,normalSprite);
        GeriTepkiDuzeltme(SwordPlayer, swordSprite);
        GeriTepkiDuzeltme(spearPlayer,spearSprite);

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
        if (spearPlayer.activeSelf)
        {
            spearAnim.SetTrigger("canVerdi");
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
        spearPlayer.SetActive(false);
        SwordPlayer.SetActive(true);
    }

   
    public void GeriTepkiDuzeltme(GameObject player,SpriteRenderer sprite)
    {
        if (player.activeSelf)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.6f);
        }
    }
    public void GeriTepkiDuzeltmeBirF(GameObject player, SpriteRenderer sprite)
    {
        if (player.activeSelf)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
        }
    }

    public void CloseAllOpenSpear()
    {
        normalPlayer.SetActive(false);
        SwordPlayer.SetActive(false);
        spearPlayer.SetActive(true);
    }

    public void CloseAllOpenNormal()
    {
        normalPlayer.SetActive(true);
        SwordPlayer.SetActive(false);
        spearPlayer.SetActive(false);

    }
}
