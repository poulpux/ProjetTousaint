using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] float radiusJoystick;
    TouchPress right, left;

    private Vector2 rJoystickValue, lJoystickValue;

    [HideInInspector]public UnityEvent<string, Vector2> posJoystick = new UnityEvent<string, Vector2>();
    private void Start()
    {
        cameraRaycast.Instance.touchBouton.AddListener((name) =>
        {
             if (name == "DashButton")
            {
                Debug.Log("dash");
            }
        });

        InputManager.Instance.press.AddListener((touchPos) =>
        {
            if (cameraRaycast.Instance.detectTouch(touchPos.posDepart ) == "LimitR")
            {
                right = touchPos;
                Debug.Log("press limitR : ");
            }
            if (cameraRaycast.Instance.detectTouch(touchPos.posDepart) == "LimitL")
            {
                left = touchPos;
                Debug.Log("press limitL : ");
            }
        });

        InputManager.Instance.fingerUp.AddListener((touch) =>
        {
            if (cameraRaycast.Instance.detectTouch(touch.posDepart) == "LimitR")
            {
                right = null;
                Debug.Log("stop press right" );
            }

            if (cameraRaycast.Instance.detectTouch(touch.posDepart) == "LimitL")
            {
                left = null;
                Debug.Log("stop press left");
            }
        });
    }

    private void Update()
    {
        Rotation();
    }

    private void Rotation()
    {
        if(right !=null)
        {
            float distance = Vector2.Distance(right.posDepart, right.currentPos);
            rJoystickValue = right.posDepart - right.currentPos;
            rJoystickValue.Normalize();
            if (distance > radiusJoystick)
            {
                rJoystickValue *= radiusJoystick;
            }
            else
            {
                rJoystickValue *= distance;
            }

            posJoystick.Invoke("CircleMoveR", -rJoystickValue);
        }

        if(left !=null)
        {
            float distance = Vector2.Distance(left.posDepart, left.currentPos);
            lJoystickValue = left.posDepart - left.currentPos;
            lJoystickValue.Normalize();
            if (distance > radiusJoystick)
            {
                lJoystickValue *= radiusJoystick;
            }
            else
            {
                lJoystickValue *= distance; 
            }
            posJoystick.Invoke("CircleMoveL", -lJoystickValue);
        }
    }
}
