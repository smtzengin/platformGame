using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [SerializeField] Slider slider;

    [SerializeField] TMP_Text coinText;

    [SerializeField] Transform butonlarPanel;

    [SerializeField] GameObject pausePanel;
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
            btn.GetComponent<Button>().interactable = true;
        }

    }

    public void NormalButonaBasildi()
    {
        TumButonlarinAlphasiniDusur();

        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.gameObject.GetComponent<CanvasGroup>().alpha = 1f;

        PlayerController.instance.CloseAllOpenNormal();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
    }
    public void KilicButonaBasildi()
    {
        TumButonlarinAlphasiniDusur();

        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.gameObject.GetComponent<CanvasGroup>().alpha = 1f;

        PlayerController.instance.NormalToSword();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
    }
    public void OkButonaBasildi()
    {
        TumButonlarinAlphasiniDusur();

        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.gameObject.GetComponent<CanvasGroup>().alpha = 1f;

        PlayerController.instance.CloseAllOpenBow();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
    }
    public void MizrakButonaBasildi()
    {
        TumButonlarinAlphasiniDusur();

        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.gameObject.GetComponent<CanvasGroup>().alpha = 1f;

        PlayerController.instance.CloseAllOpenSpear();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
    }

    public void PausePanelAcKapat()
    {
        if (!pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void AnaMenuyeDon()
    {
        SceneManager.LoadScene("girisSahnesi");
    }
}
