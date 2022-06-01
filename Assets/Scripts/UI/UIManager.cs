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

    [SerializeField] Transform butonlarPanel;

    private void Start()
    {
        TumButonlarinAlphasiniDusur();
        butonlarPanel.GetChild(0).GetComponent<CanvasGroup>().alpha = 1f;
        PlayerController.instance.CloseAllOpenNormal();
    }

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

    void TumButonlarinAlphasiniDusur()
    {
        foreach (Transform btn in butonlarPanel)
        {
            btn.gameObject.GetComponent<CanvasGroup>().alpha = 0.25f;
        }
    }

    public void NormalButonaBasildi()
    {
        TumButonlarinAlphasiniDusur();

        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.gameObject.GetComponent<CanvasGroup>().alpha = 1f;

        PlayerController.instance.CloseAllOpenNormal();
    }
    public void KilicButonaBasildi()
    {
        TumButonlarinAlphasiniDusur();

        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.gameObject.GetComponent<CanvasGroup>().alpha = 1f;

        PlayerController.instance.NormalToSword();
    }
    public void OkButonaBasildi()
    {
        TumButonlarinAlphasiniDusur();

        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.gameObject.GetComponent<CanvasGroup>().alpha = 1f;

        PlayerController.instance.CloseAllOpenBow();
    }
    public void MizrakButonaBasildi()
    {
        TumButonlarinAlphasiniDusur();

        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.gameObject.GetComponent<CanvasGroup>().alpha = 1f;

        PlayerController.instance.CloseAllOpenSpear();
    }
}
