using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TapStartScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI txt;
    private float timer;
    bool monte = true;
    void Start()
    {
        this.gameObject.SetActive(true);
        TimeManager.Instance.setCurrentTimeScale(0f);
        InputManager.Instance.TapStart.AddListener(() =>
        {
            TimeManager.Instance.setCurrentTimeScale(1f);
            this.gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        timer += monte == true ? Time.unscaledDeltaTime : -Time.unscaledDeltaTime;

        if(timer>0.7)
            monte = false;
        if (timer < 0.02f && !monte)
            monte = true;

        txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, 0.3f + timer);
    }
}
