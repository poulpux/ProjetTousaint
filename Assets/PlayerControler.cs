using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEditor.Experimental.GraphView;
using System.IO;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EndroitToTouch", order = 1)]
public class EndroitToTouch : ScriptableObject
{
    public string pseudo;
    public Vector3 pos;
    public float radius;
}

public class PlayerControler : MonoBehaviour
{
    [SerializeField] List<GameObject> giveObjectToTouch = new List<GameObject>();
    [SerializeField] private List<EndroitToTouch> listEndroitToTouch = new List<EndroitToTouch>();
    // Start is called before the first frame update
    void Start()
    {
        translateInEndroitToTouch();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0) 
            ChercheIfWeClickOnButton(Input.touches[0].position);
    }

    private string ChercheIfWeClickOnButton(Vector3 posTouch)
    {
        foreach (var item in listEndroitToTouch)
        {
            if(Vector3.Distance(posTouch,item.pos)< item.radius)
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
            //temp.pos = item.transform.position;
           
            temp.pos = new Vector2(rect.anchorMin.x * Screen.width, rect.anchorMin.y * Screen.height);
            temp.radius = rect.rect.width/2f;
            //Vector3 test = new Vector3(temp.radius/2, temp.radius/2, 0f);
            //temp.pos += (Vector3)rect.anchoredPosition;
            temp.pseudo = item.name;
            Debug.Log(temp.pseudo+" : "+temp.radius+" : "+temp.pos);
            listEndroitToTouch.Add(temp);
        }

        giveObjectToTouch.Clear();
    }
}
