using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderController : MonoBehaviour
{
    [SerializeField] Transform[] positions;

    [SerializeField] Slider orumcekSlider;

    [SerializeField] GameObject iksirPrefab;

    public float orumcekHizi;
    public float beklemeSuresi;

    float beklemeSayac;

    Animator anim;

    int kacinciPozisyon;

    public float takipMesafesi = 5f;

    Transform hedefPlayer;

    BoxCollider2D orumcekCollider2D;

    bool atakYapabilirMi;

    Rigidbody2D rb;

    public int maxSaglik;
    int gecerliSaglik;

    bool isDead;

    private void Awake()
    {

        anim = GetComponent<Animator>();
        orumcekCollider2D = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        gecerliSaglik = maxSaglik;
        atakYapabilirMi = true;
        hedefPlayer = GameObject.Find("Player").transform;
        isDead = false;
        orumcekSlider.maxValue = maxSaglik;
        SliderGuncelle();

        foreach (Transform pos in positions)
        {
            pos.parent = null;
        }
    }

    private void Update()
    {
        if (!atakYapabilirMi)
            return;

        if(beklemeSayac > 0) 
        {
            //Örümcek beklediði yerde duruyor
            beklemeSayac -= Time.deltaTime;
            anim.SetBool("hareketEtsinMi", false);
        }
        else
        {
            if(hedefPlayer.position.x > positions[0].position.x && hedefPlayer.position.x < positions[1].position.x)
            {
                if (!isDead)
                {
                    transform.position = Vector3.MoveTowards(transform.position, hedefPlayer.position, orumcekHizi * Time.deltaTime);

                    anim.SetBool("hareketEtsinMi", true);

                    SpiderFlip(hedefPlayer);
                }
            }
            else
            {
                anim.SetBool("hareketEtsinMi", true);

                SpiderFlip(positions[kacinciPozisyon]);

                transform.position = Vector3.MoveTowards(transform.position, positions[kacinciPozisyon].position, orumcekHizi * Time.deltaTime);

                if (Vector3.Distance(transform.position, positions[kacinciPozisyon].position) < 0.1f)
                {
                    beklemeSayac = beklemeSuresi;
                    ChangePosition();
                }

            }       
        }
    }

    void ChangePosition()
    {
        kacinciPozisyon++;
        if(kacinciPozisyon >= positions.Length)
        {
            kacinciPozisyon = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green; // çember çizimi
        Gizmos.DrawWireSphere(transform.position, takipMesafesi);
    }

    void SpiderFlip(Transform beklenenPosition)
    {
        if (transform.position.x > beklenenPosition.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (transform.position.x < beklenenPosition.position.x)
        {
            transform.localScale = Vector3.one;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (orumcekCollider2D.IsTouchingLayers(LayerMask.GetMask("PlayerLayer")) && atakYapabilirMi)
        {
            atakYapabilirMi = false;
            anim.SetTrigger("atakYapti");

            other.GetComponent<PlayerController>().GeriTepki();
            other.GetComponent<PlayerHealthController>().CanAzaltFNC();

            StartCoroutine(YenidenAtakYapsin());
        }
    }

    IEnumerator YenidenAtakYapsin()
    {
        yield return new WaitForSeconds(1f);
        atakYapabilirMi = true;
    }

    public IEnumerator GeriTepkiFNC()
    {
        atakYapabilirMi = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(.1f);

        gecerliSaglik--;
        SliderGuncelle();

        if(gecerliSaglik <= 0)
        {
            gecerliSaglik = 0;
            anim.SetTrigger("canVerdi");
            isDead = true;
            orumcekCollider2D.enabled = false;
            orumcekSlider.gameObject.SetActive(false);
            Instantiate(iksirPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject, 2f);
        }
        else
        {

            for (int i = 0; i < 5; i++)
            {
                rb.velocity = new Vector2(-transform.localScale.x + i, rb.velocity.y);
                yield return new WaitForSeconds(0.05f);

            }

            anim.SetBool("hareketEtsinMi", false);

            yield return new WaitForSeconds(0.25f);

            rb.velocity = Vector2.zero;

            atakYapabilirMi = true;
        }
    }

    void SliderGuncelle()
    {
        orumcekSlider.value = gecerliSaglik;
    }
}
