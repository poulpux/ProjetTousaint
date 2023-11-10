using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    [SerializeField] PlayerCapacity player;
    [SerializeField] Image image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(name == "DashButton")
        {
            image.fillAmount = player.timerDash > player.dashCouldown ? 1 : player.timerDash / player.dashCouldown;
        }

        if (name == "TimeButton")
        {
            if (player.inTimeStop)
            {
                image.fillAmount =1-( player.timerTimeStop / player.timeStopDuration*player.timeStopPower);
            }
            else
                image.fillAmount =player.currentTimeStopCharge>0 ? 1 : 0;
        }
    }
}
