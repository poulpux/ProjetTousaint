using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.tap.AddListener((pop) =>
        {
            if(TimeManager.Instance.currentTimer > 3f)
            {
                GameManager.Instance.BackToMenu();
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
