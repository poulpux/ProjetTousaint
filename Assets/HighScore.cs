using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if(PlayerPrefs.GetFloat("highscore1") == 0)
            AllClean();
        triBestScore();
        for (int i = 1; i < 8; i++)
        {
            text.text += PlayerPrefs.GetFloat("highscore" + i.ToString("F2")).ToString()+ "\r\n";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void triBestScore()
    {
        for (int i = 1; i < 8; i++)
        {
            if (TimeManager.Instance.globalTimer < PlayerPrefs.GetFloat("highscore" + i.ToString()))
            {
                PlayerPrefs.SetFloat("highscore" + i.ToString(), TimeManager.Instance.globalTimer);
                break;
            }
        }
    }

    private void AllClean()
    {
        for (int i = 1; i < 8; i++)
        {
                PlayerPrefs.SetFloat("highscore" + i.ToString(), i*100+100);
        }
    }
}
