using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Infiltration : MonoBehaviour
{
    [HideInInspector] public UnityEvent Reperated = new UnityEvent();

    
    [SerializeField] List<GameObject> objectToDelete = new List<GameObject>();
    [SerializeField] List<GameObject> objectToApere = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        Reperated.AddListener(() =>
        {
            foreach (var item in objectToDelete)
            {
                item.SetActive(false);
            }

            foreach (var item in objectToApere)
            {
                item.SetActive(true);
            }
        });

        foreach (var item in objectToDelete)
        {
            item.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
