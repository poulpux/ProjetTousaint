using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class getPlayerTxt : MonoBehaviour
{
    
    private PlayerCapacity player;
    private Shoot shootPlayer;
    private TextMeshProUGUI txt;
    // Start is called before the first frame update
    void Start()
    {
        txt = gameObject.GetComponent<TextMeshProUGUI>();
        player = FindFirstObjectByType<PlayerCapacity>();

        if (name == "ShootTxt")
        {
            shootPlayer = player.gameObject.GetComponent<Shoot>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(name == "ChronoTxt" && txt !=null)
        {
            txt.text = player.currentTimeStopCharge.ToString() + " / "+ player.maxTimeStop.ToString();
        }

        if (name == "ShootTxt" && txt != null)
        {
            txt.text = shootPlayer.currentBullet.ToString() + " / " + shootPlayer.maxBullet.ToString();
        }
    }
}
