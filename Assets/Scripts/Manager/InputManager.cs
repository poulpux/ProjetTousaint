using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;

public class TouchPress
{
    public Touch touch;
    public Vector3 posDepart;
    public Vector3 currentPos;
}
public class InputManager : MonoBehaviour
{
    [HideInInspector] public UnityEvent<Vector3> tap = new UnityEvent<Vector3>();
    [HideInInspector] public UnityEvent<Vector3> doubleTap = new UnityEvent<Vector3>();
    [HideInInspector] public UnityEvent<TouchPress> press = new UnityEvent<TouchPress>();
    [HideInInspector] public UnityEvent<TouchPress> fingerUp = new UnityEvent<TouchPress>();

    List<TouchPress> savePos = new List<TouchPress>();

    [HideInInspector] public UnityEvent TapStart = new UnityEvent();

    private static InputManager instance;
    public static InputManager Instance  { get{return instance; } }

    [SerializeField] float radiusJoystick;
    [HideInInspector] public Vector2 rJoystickValue, lJoystickValue;
    [HideInInspector]public TouchPress right, left;

    [HideInInspector] public UnityEvent<string, Vector2> posJoystick = new UnityEvent<string, Vector2>();

    [SerializeField]
    private float delayTap;

    private float timer;

    public bool canMove = true;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        AddJoystickListeneur();
    }

    void Update()
    {
        stickCalculation();
        TouchGestion();
        timer += Time.deltaTime;
       
    }

    private void LateUpdate()
    {
        canMove = true;
    }

    private void AddJoystickListeneur()
    {
        InputManager.Instance.press.AddListener((touchPos) =>
        {
            if (cameraRaycast.Instance.detectTouch(touchPos.posDepart) == "LimitR")
            {
                right = touchPos;
            }
            if (cameraRaycast.Instance.detectTouch(touchPos.posDepart) == "LimitL")
            {
                left = touchPos;
            }
        });

        InputManager.Instance.tap.AddListener((touchPos) =>
        {
            if (cameraRaycast.Instance.detectTouch(touchPos) == "Start")
            {
                TapStart.Invoke();
            }
           
        });

        InputManager.Instance.fingerUp.AddListener((touch) =>
        {
            if (cameraRaycast.Instance.detectTouch(touch.posDepart) == "LimitR")
            {
                right = null;
            }

            if (cameraRaycast.Instance.detectTouch(touch.posDepart) == "LimitL")
            {
                left = null;
            }
        });
    }

    private void stickCalculation()
    {
        RightStickCalculation();
        LeftStickCalculation();
    }

    private void RightStickCalculation()
    {
        if (right != null)
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
    }

    private void LeftStickCalculation()
    {
        if (left != null)
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

    private void TouchGestion()
    {
        if (Input.touchCount > 0)
        {
            foreach (var item in Input.touches)
            {
                if (item.phase == TouchPhase.Began)
                {
                    TouchPress a = new TouchPress();
                    a.posDepart = item.position;
                    a.touch = item;
                    savePos.Add(a);
                    tap.Invoke((Vector3)item.position);
                }

                if (item.phase == TouchPhase.Moved)
                {
                    foreach (var item1 in savePos)
                    {
                        if (item.fingerId == item1.touch.fingerId)
                        {
                            item1.currentPos = item.position;
                            press.Invoke(item1);
                        }
                    }

                }
                if (item.phase == TouchPhase.Ended)
                {
                    TouchPress desableTouche = new TouchPress();
                    foreach (var item1 in savePos)
                    {
                        if (item.fingerId == item1.touch.fingerId)
                        {
                            desableTouche = item1;
                            fingerUp.Invoke(item1);
                        }
                    }
                    savePos.Remove(desableTouche);
                }
            }
        }
    }
}
