using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEditor.Experimental.GraphView;
using System.IO;
using UnityEditor.Rendering;
using System;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EndroitToTouch", order = 1)]
public class EndroitToTouch : ScriptableObject
{
    public string pseudo;
    public Vector2 pos;
    public float radius;
}

public class PlayerControler : MonoBehaviour
{
    [SerializeField] List<GameObject> giveObjectToTouch = new List<GameObject>();
    [SerializeField] private List<EndroitToTouch> listEndroitToTouch = new List<EndroitToTouch>();
    [SerializeField] private GameObject circleTest;
    [SerializeField] private Transform parentTransform;

    [SerializeField] Camera cam;
    GraphicRaycaster uiRaycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;

    void Start()
    {
        uiRaycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
        pointerEventData = new PointerEventData(eventSystem);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            pointerEventData.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            uiRaycaster.Raycast(pointerEventData, results);

            foreach (var result in results)
            {
                Debug.Log("Objet d'UI touché : " + result.gameObject.name);
            }
        }
        //if(Input.touchCount > 0) 
        //    ChercheIfWeClickOnButton(Input.touches[0].position);
    }

    private string ChercheIfWeClickOnButton(Vector3 posTouch)
    {
        //float[] distanceList =new  float[listEndroitToTouch.Count];
        foreach (var item in listEndroitToTouch)
        {
            if(Vector2.Distance(posTouch,item.pos)< item.radius)
            {
                Debug.Log(Input.touches[0].position + "           " + item.pseudo);
                return item.pseudo;
            }

        }
        return null;
    }

    private void translateInEndroitToTouch()
    {
        foreach (var item in giveObjectToTouch)
        {
            RectTransform rect = item.GetComponent<RectTransform>();
            EndroitToTouch temp = new EndroitToTouch();
            //temp.pos = rect.position;
           
            //temp.pos = new Vector2(rect.anchorMin.x * Screen.width, rect.anchorMin.y * Screen.height);
           
            temp.radius = rect.rect.width/2f;
            //Vector3 test = new Vector3(temp.radius/2, temp.radius/2, 0f);
            temp.pos += rect.anchoredPosition;
            temp.pseudo = item.name;
            Debug.Log(temp.pseudo+" : "+temp.radius+" : "+temp.pos);
            listEndroitToTouch.Add(temp);
            GameObject newcircle = circleTest;
            newcircle.gameObject.transform.position = temp.pos;
            newcircle.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(temp.radius*2f, temp.radius*2f);

            Instantiate(newcircle, parentTransform);
        }

        giveObjectToTouch.Clear();
    }
}
