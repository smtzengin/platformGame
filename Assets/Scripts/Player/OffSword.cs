using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffSword : MonoBehaviour
{
    public GameObject SwordHitBox;

    public void InvisibleSword()
    {
        SwordHitBox.SetActive(false);
    }
}
