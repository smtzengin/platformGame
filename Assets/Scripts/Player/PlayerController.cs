using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] Transform zeminKontrol;
    [SerializeField] Animator normalAnim,swordAnim,spearAnim,bowAnim;
    [SerializeField] SpriteRenderer normalSprite, swordSprite,spearSprite,bowSprite;
    [SerializeField] GameObject SwordHitBox;
    public LayerMask zeminMask;

    Rigidbody2D rb2D;
    [SerializeField] GameObject normalPlayer, SwordPlayer,spearPlayer,bowPlayer;
    [SerializeField] GameObject atilacakSpear;
    [SerializeField] Transform spearCikisNoktasi;
    [SerializeField] GameObject atilacakOk;
    [SerializeField] Transform okCikisNoktasi;

    public float moveSpeed;
    public float jumpPower;
    bool zemindeMi;
    bool doubleJumpOlsunMu;

    [SerializeField] float geriTepkiSuresi, geriTepkiGucu;
    [SerializeField] float tirmanisHizi = 3f;
    [SerializeField] GameObject normalKamera, kilicKamera, okKamera, mizrakKamera;
    float geriTepkiSayaci;
    bool yonSagdaMi;
    public bool playerCanVerdiMi;
    bool swordAttack;
    bool spearAttack;
    bool bowAttack;
    bool okAtabilirMi;
    public float attackTimer;
    public float attackCoolDown;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        swordAttack = false;
        okAtabilirMi = true;
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
            GeriTepkiDuzeltmeBirF(bowPlayer, bowSprite);

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
            if (Input.GetKeyDown(KeyCode.X) && bowPlayer.activeSelf && okAtabilirMi)
            {
                bowAnim.SetTrigger("okAtti");
                StartCoroutine(DelayArrow());     
            }
            if (bowPlayer.activeSelf)
            {
                if (GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("TirmanmaLayer")))
                {
                    float h = Input.GetAxis("Vertical");

                    Vector2 tirmanisVector = new Vector2(rb2D.velocity.x, h * tirmanisHizi);
                    rb2D.velocity = tirmanisVector;
                    rb2D.gravityScale = 0f;
                    bowAnim.SetBool("tirmansinMi", true);
                    bowAnim.SetFloat("yukariHareketHizi", Mathf.Abs(rb2D.velocity.y));
                }
                else
                {
                    bowAnim.SetBool("tirmansinMi", false);
                    rb2D.gravityScale = 5f;
                }
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
        if (bowPlayer.activeSelf)
        {
            bowAnim.SetBool("zemindeMi", zemindeMi);
            bowAnim.SetFloat("hareketHizi", Mathf.Abs(rb2D.velocity.x));
        }

        if (swordAttack && SwordPlayer.activeSelf)
        {
            swordAnim.SetTrigger("SwordAttack");
        }
        if (spearAttack && spearPlayer.activeSelf)
        {
            spearAnim.SetTrigger("atakYapti");
        }
        if (bowAttack && bowPlayer.activeSelf)
        {
            bowAnim.SetTrigger("okAtti");
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

    IEnumerator DelayArrow()
    {
        okAtabilirMi = false;
        yield return new WaitForSeconds(0.5f);
        ArrowPoolManager.instance.ArrowFirlat(okCikisNoktasi, this.transform);
        okAtabilirMi = true;
    }
    

    public void GeriTepki()
    {
        geriTepkiSayaci = geriTepkiSuresi;
        
        GeriTepkiDuzeltme(normalPlayer,normalSprite);
        GeriTepkiDuzeltme(SwordPlayer, swordSprite);
        GeriTepkiDuzeltme(spearPlayer,spearSprite);
        GeriTepkiDuzeltme(bowPlayer, bowSprite);

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
        if (bowPlayer.activeSelf)
        {
            bowAnim.SetTrigger("canVerdi");
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
    public void NormalToSword()
    {
        TumKameralariKapat();
        kilicKamera.SetActive(true);

        normalPlayer.SetActive(false);
        spearPlayer.SetActive(false);
        SwordPlayer.SetActive(true);
        bowPlayer.SetActive(false);
    }

    public void CloseAllOpenSpear()
    {
        TumKameralariKapat();
        mizrakKamera.SetActive(true);

        normalPlayer.SetActive(false);
        SwordPlayer.SetActive(false);
        spearPlayer.SetActive(true);
        bowPlayer.SetActive(false);
    }

    public void CloseAllOpenNormal()
    {
        TumKameralariKapat();
        normalKamera.SetActive(true);

        normalPlayer.SetActive(true);
        SwordPlayer.SetActive(false);
        spearPlayer.SetActive(false);
        bowPlayer.SetActive(false);
    }

    public void CloseAllOpenBow()
    {
        TumKameralariKapat();
        okKamera.SetActive(true);

        normalPlayer.SetActive(false);
        SwordPlayer.SetActive(false);
        spearPlayer.SetActive(false);
        bowPlayer.SetActive(true);

    }

    void TumKameralariKapat()
    {
        normalKamera.SetActive(false);
        kilicKamera.SetActive(false);
        okKamera.SetActive(false);
        mizrakKamera.SetActive(false);
    }

    public void PlayeriHareketsizYap()
    {
        if (normalPlayer.activeSelf)
        {
            rb2D.velocity = Vector2.zero;
            normalAnim.SetFloat("hareketHizi", 0);
        }
        if (SwordPlayer.activeSelf)
        {
            rb2D.velocity = Vector2.zero;
            swordAnim.SetFloat("hareketHizi", 0);
        }
        if (spearPlayer.activeSelf)
        {
            rb2D.velocity = Vector2.zero;
            spearAnim.SetFloat("hareketHizi", 0);
        }
        if (bowPlayer.activeSelf)
        {
            rb2D.velocity = Vector2.zero;
            bowAnim.SetFloat("hareketHizi", 0);
        }
    }
}
