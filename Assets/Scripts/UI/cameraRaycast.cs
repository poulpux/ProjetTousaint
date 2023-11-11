using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class cameraRaycast : MonoBehaviour
{
    [SerializeField] 
    List<GameObject> giveObjectToTouch = new List<GameObject>();

    private List<string> listEndroitToTouch = new List<string>();

    [SerializeField] 
    GraphicRaycaster uiRaycaster;

    PointerEventData pointerEventData;

    [SerializeField] 
    EventSystem eventSystem;

    [HideInInspector]
    public UnityEvent<string> touchBouton = new UnityEvent<string>();

    private static cameraRaycast instance;
    public static cameraRaycast Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        pointerEventData = new PointerEventData(eventSystem);
        addListeNameTouchable();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount> 0)
        {
            foreach (var item in Input.touches)
            {
                string key = detectTouch(item.position);
                if (key != null)
                    touchBouton.Invoke(key);
            }
            
        }
    }

    private void addListeNameTouchable()
    {
        foreach (var item in giveObjectToTouch)
        {
            listEndroitToTouch.Add(item.gameObject.name);
        }
        giveObjectToTouch.Clear();
    }

    public string detectTouch(Vector3 touchPos)
    {
        pointerEventData.position = touchPos;
        List<RaycastResult> results = new List<RaycastResult>();
        uiRaycaster.Raycast(pointerEventData, results);

        foreach (var result in results)
        {
            foreach (var item in listEndroitToTouch)
            {
                if (result.gameObject.name == item)
                    return result.gameObject.name;
            }
        }
        return null;
    }
}
