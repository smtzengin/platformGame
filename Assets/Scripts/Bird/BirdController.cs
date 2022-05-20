using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] Transform[] positions;

    public float birdSpeed;
    public float beklemeSuresi;
    float beklemeSayac;
    int kacinciSayac;
    int kacinciPosition;

    Animator anim;

    Vector2 kusYonu;

    private void Awake()
    {
        foreach (Transform pos in positions)
        {
            pos.parent = null;
        }

        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        kacinciPosition = 0;

        transform.position = positions[kacinciPosition].position;
    }

    private void Update()
    {

        if(beklemeSayac > 0)
        {
            anim.SetBool("ucsunMu", false);
            beklemeSayac -= Time.deltaTime;
        }
        else
        {
            kusYonu = new Vector2(positions[kacinciPosition].position.x - transform.position.x, positions[kacinciPosition].position.y - transform.position.y);

            float angle = Mathf.Atan2(kusYonu.y, kusYonu.x) * Mathf.Rad2Deg;
            if (transform.position.x > positions[kacinciPosition].position.x)
            {
                transform.localScale = new Vector3(1, -1, 1);
            }
            else
            {
                transform.localScale = Vector3.one;
            }
            transform.rotation = Quaternion.Euler(0, 0, angle);

            transform.position = Vector3.MoveTowards(transform.position, positions[kacinciPosition].position, birdSpeed * Time.deltaTime);
            anim.SetBool("ucsunMu", true);
            if (Vector3.Distance(transform.position, positions[kacinciPosition].position) < 0.1f)
            {
                PositionChange();
                beklemeSayac = beklemeSuresi;              
            }
        }
    }

    void PositionChange()
    {
        kacinciPosition++;
        if(kacinciPosition >= positions.Length)
        {
            kacinciPosition = 0;
        }
    }
}
