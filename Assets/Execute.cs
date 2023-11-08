using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Execute : MonoBehaviour
{
    [SerializeField] private float executeRange;
    [SerializeField] private float timeToExecute;
    private SphereCollider sphere;

    [HideInInspector] public UnityEvent<bool> canExecute =new UnityEvent<bool>();

    [SerializeField] private List<GameObject> showInExecute = new List<GameObject>();
    private List<GameObject> executableEnnemy = new List<GameObject>() ;

    bool canExecuteBis;
    // Start is called before the first frame update
    void Start()
    {
        canExecute.AddListener((can) =>
        {
            foreach (var item in showInExecute)
            {
                item.SetActive(can);
            }
        });

        InputManager.Instance.tap.AddListener((touchPos) =>
        {
            if (cameraRaycast.Instance.detectTouch(touchPos) == "LimitR")
            {
                executableEnnemy.Sort();
            }
        });
            sphere = GetComponent<SphereCollider>();    
        sphere.radius = executeRange;
    }

    // Update is called once per frame
    void Update()
    {
        canExecuteBis = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Ennemy")
        {
            canExecuteBis = true;
            executableEnnemy.Add(other.gameObject);
        }

        canExecute.Invoke(canExecuteBis);
    }
}
