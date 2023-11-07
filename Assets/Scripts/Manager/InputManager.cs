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
    [HideInInspector] public UnityEvent<Vector2> tap = new UnityEvent<Vector2>();
    [HideInInspector] public UnityEvent<Vector2> doubleTap = new UnityEvent<Vector2>();
    [HideInInspector] public UnityEvent<TouchPress> press = new UnityEvent<TouchPress>();
    [HideInInspector] public UnityEvent<TouchPress> fingerUp = new UnityEvent<TouchPress>();

    List<TouchPress> savePos = new List<TouchPress>();

    private static InputManager instance;
    public static InputManager Instance  { get{return instance; } }


    [SerializeField]
    private float delayTap;

    private float timer;
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

    }

    void Update()
    {
        timer += Time.deltaTime;
        if(Input.touchCount > 0)
        {
            foreach (var item in Input.touches)
            {
                if(item.phase == TouchPhase.Began) 
                {
                    TouchPress a = new TouchPress();
                    a.posDepart = item.position;
                    a.touch = item;
                    savePos.Add(a);
                    tap.Invoke(item.position);
                }

                if(item.phase == TouchPhase.Moved) 
                {
                    foreach (var item1 in savePos)
                    {
                        if(item.fingerId == item1.touch.fingerId)
                        {
                            item1.currentPos = item.position;
                            press.Invoke(item1);
                        }
                    }
                   
                }
                if(item.phase == TouchPhase.Ended)
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
