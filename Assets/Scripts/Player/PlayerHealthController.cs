
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int maxSaglik, gecerliSaglilk;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        gecerliSaglilk = maxSaglik;

        if(UIManager.instance != null)
        {
            UIManager.instance.SliderGuncelle(gecerliSaglilk, maxSaglik);
        }

    }

    public void CanAzaltFNC()
    {
        gecerliSaglilk--;
        UIManager.instance.SliderGuncelle(gecerliSaglilk, maxSaglik);
        if (gecerliSaglilk <= 0)
        {
            gecerliSaglilk = 0;
            // gameObject.SetActive(false);

            PlayerController.instance.PlayerCanVerdi();
        }
    }

    public void CaniArttir()
    {
        gecerliSaglilk+= 3;
        if(gecerliSaglilk >= maxSaglik)
        {
            gecerliSaglilk = maxSaglik;
        }

        UIManager.instance.SliderGuncelle(gecerliSaglilk, maxSaglik);
    }
}
