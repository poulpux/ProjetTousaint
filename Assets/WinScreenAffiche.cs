using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreenAffiche : MonoBehaviour
{
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
       text = GetComponent<TextMeshProUGUI>();
       text.text = "-Name : Marquet Ambroise\r\n-Age : 19\r\n\r\n-Score : 19.5/20\r\n\r\n-Current time :    "+TimeManager.Instance.globalTimer.ToString()+"\r\n\r\n-High score :\r\n\t\t";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
