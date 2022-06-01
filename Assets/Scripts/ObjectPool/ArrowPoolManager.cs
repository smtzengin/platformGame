using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPoolManager : MonoBehaviour
{
    public static ArrowPoolManager instance;
    [SerializeField] GameObject arrowPrefab;
    GameObject arrowObject;

    List<GameObject> arrowPool = new List<GameObject> ();

    private void Awake()
    {
        instance = this;

        CreateArrows();
    }

    void CreateArrows()
    {
        for (int i = 0; i < 10; i++)
        {
            arrowObject = Instantiate(arrowPrefab);
            arrowObject.SetActive(false);
            arrowObject.transform.parent = transform;

            arrowPool.Add(arrowObject);
        }
    }

    public void ArrowFirlat(Transform arrowCikisNoktasi,Transform parent)
    {
        for (int i = 0; i < arrowPool.Count; i++)
        {
            if (!arrowPool[i].gameObject.activeInHierarchy)
            {

                arrowPool[i].transform.localScale = parent.localScale;
                arrowPool[i].SetActive(true);
                arrowPool[i].gameObject.transform.position = arrowCikisNoktasi.position;
                if (parent.transform.localScale.x > 0)
                {
                    arrowPool[i].GetComponent<Rigidbody2D>().velocity = arrowCikisNoktasi.right * transform.localScale.x * 15f;
                }
                else
                {
                    arrowPool[i].GetComponent<Rigidbody2D>().velocity = -arrowCikisNoktasi.right * transform.localScale.x * 15f;
                }            

                return;
            }
            
        }
   
    }

    void AgainArrowCreate(Transform arrowCikisNoktasi, Transform parent)
    {
        arrowObject = Instantiate(arrowPrefab);
        arrowObject.transform.parent = transform;
        arrowPool.Add(arrowObject);

    }
}
