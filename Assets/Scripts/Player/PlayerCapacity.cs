using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCapacity : MonoBehaviour
{
    [SerializeField] private float dashCouldown;
    [SerializeField] private float dashDistance;

    private float timerDash;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.tap.AddListener((touchPos) =>
        {
            if (cameraRaycast.Instance.detectTouch(touchPos) == "DashButton")
            {
                Debug.Log("Dash");
                useDash();
            }

            if (cameraRaycast.Instance.detectTouch(touchPos) == "TimeButton")
            {
                useTimeStop();
            }
        });
    }

        // Update is called once per frame
    void Update()
    {
        timerDash += Time.deltaTime;
    }

    void useDash()
    {
        if (timerDash > dashCouldown)
        {
            Debug.Log("Fonction dash");
            transform.position += new Vector3(InputManager.Instance.lJoystickValue.x, 0f, InputManager.Instance.lJoystickValue.y) * -dashDistance;
            timerDash = 0;
        }
    }

    void useTimeStop()
    {

    }
}
