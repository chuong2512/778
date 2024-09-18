using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MoreMountains.NiceVibrations;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public GameObject winpanel;
    public GameObject ingame;
    public GameObject swipe;
    public Text level_nbr_txt;
    public Text level_nbr_win_panel;
    public Text  txt_mmoney , txt_multi;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

        level_nbr_win_panel.text =  level_nbr_txt.text = "LEVEL " + (GameManager.instance.getlevel() + 1);
        txt_mmoney.text = GameManager.instance.getcoin().ToString();
    }

    private void Update()
    {
        txt_mmoney.text = GameManager.instance.getcoin().ToString();
    }


    public void show_win()
    {
        StartCoroutine(show_win_panel());

    }
    //public void show_lose_direct()
    //{
    //    losepanel.SetActive(true);
    //    ingame.SetActive(false);

    //    //Advertisements.Instance.ShowInterstitial();
    //}
    //public void show_lose()
    //{
    //    StartCoroutine(show_lose_panel());
    //}
    //IEnumerator show_lose_panel()
    //{
    //    yield return new WaitForSeconds(2f);
    //    losepanel.SetActive(true);
    //    ingame.SetActive(false);

    //    Advertisements.Instance.ShowInterstitial();
    //}

    IEnumerator show_win_panel()
    {
        yield return new WaitForSeconds(2f);
        winpanel.SetActive(true);
        ingame.SetActive(false);
    }

    public void hide_swipe_panel()
    {
        FindObjectOfType<Controller_Brush>().game_run = true;

        ingame.SetActive(true);
        swipe.SetActive(false);
    }

    //public void btn_retry()
    //{
    //    Advertisements.Instance.ShowInterstitial();

    //    // sound
    //    //SoundManager.instance.Play("click");

    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}

    public void btn_next()
    {
        //Advertisements.Instance.ShowInterstitial();

        // sound
        //SoundManager.instance.Play("click");

        GameManager.instance.setLevel(GameManager.instance.getlevel() + 1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    

    public void increase_money(float nbr)
    {
        GameManager.instance.setcoin(GameManager.instance.getcoin() + nbr);
        txt_mmoney.text = GameManager.instance.getcoin().ToString();
    }

    public void show_multiplication(int nbr)
    {
        txt_multi.text = nbr + " ×";
    }
    
    public void _vibrate()
    {
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact, true, this);
    }
}
