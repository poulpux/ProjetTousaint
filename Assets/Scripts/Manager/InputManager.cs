using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;
public class InputManager : MonoBehaviour
{
    [HideInInspector] public UnityEvent<Vector2> tap = new UnityEvent<Vector2>();
    [HideInInspector] public UnityEvent<Vector2> doubleTap = new UnityEvent<Vector2>();
    [HideInInspector] public UnityEvent<Vector2> press = new UnityEvent<Vector2>();

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
                    tap.Invoke(item.position);
                }

                if(item.phase == TouchPhase.Moved) 
                {
                    press.Invoke(item.position);
                }
            }
        }
    }
}
