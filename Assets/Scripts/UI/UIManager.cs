using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [SerializeField] Slider slider;

    [SerializeField] TMP_Text coinText;

    private void Awake()
    {
        instance = this;
    }

    public void SliderGuncelle(int gecerliDeger,int maxDeger)
    {
        slider.maxValue = maxDeger;
        slider.value = gecerliDeger;
    }

    public void CoinAdetGuncelle()
    {
        coinText.text = GameManager.instance.toplananCoinAdet.ToString();
    }
}
