using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionnaireBouton : MonoBehaviour
{
    [SerializeField] GameObject visuOption;
    [SerializeField] Slider sliderMusic;
    [SerializeField] Slider sliderSound;
    [SerializeField] private Image BackgroundMusic;
    [SerializeField] private Image BackgroundSound;
    void Start()
    { 
    }


    void Update()
    {
        BackgroundMusic.color = new Color((100 - sliderMusic.value) / 100f, (100 - sliderMusic.value) / 100f, (100 - sliderMusic.value) / 100f);
        BackgroundSound.color = new Color((100 - sliderSound.value) / 100f, (100 - sliderSound.value) / 100f, (100 - sliderSound.value) / 100f);
    }

    public void GoToOptionMenu()
    {
        visuOption.SetActive(true);
        GameManager.Instance.GoOptionMenu();
    }
    public void QuitOptionMenu()
    {
        visuOption.SetActive(false);
        GameManager.Instance.OutOptionMenu();
    }

    public void GoToPause()
    {
        visuOption.SetActive(true);
        GameManager.Instance.GoToPause();
    }
    public void QuitPause()
    {
        visuOption.SetActive(false);
        GameManager.Instance.QuitPause();
    }
    public void QuitGame()
    {
        GameManager.Instance.onQuitExit();
    }
    public void PlayGame()
    {
        GameManager.Instance.GoInGame();
    }
}
