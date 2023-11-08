using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private GameObject ennemyToExecute;

    bool canExecuteBis;
    float timerExcute = 10f;
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
            if (cameraRaycast.Instance.detectTouch(touchPos) == "PunchButton")
            {
                throwExecution();
            }
        });

        sphere = GetComponent<SphereCollider>();    
        sphere.radius = executeRange;
    }

    // Update is called once per frame
    void Update()
    {
        canExecuteBis = false;
        isExecuting();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Ennemy")
        {
            changeEnnemyColor();
            canExecuteBis = true;
            executableEnnemy.Add(other.gameObject);
        }

        canExecute.Invoke(canExecuteBis);
    }

    private void isExecuting()
    {
        if(timeToExecute > timerExcute)
        {
            timerExcute +=Time.deltaTime;
            InputManager.Instance.canMove = false;
        }
        else if(InputManager.Instance.canMove == false)
        {
            ennemyToExecute = null;
            InputManager.Instance.canMove = true;
        }
    }

    private void throwExecution()
    {
        if (executableEnnemy.Count > 0 && timeToExecute < timerExcute)
        {
            timerExcute = 0;
            ennemyToExecute = executableEnnemy[0];
            Destroy(executableEnnemy[0], timeToExecute);
            executableEnnemy.RemoveAt(0);
        }
        else
        {
            Debug.Log("No executable enemy available.");
        }
    }

    private void changeEnnemyColor()
    {
        if (ennemyToExecute != null)
        {
            Renderer[] renderers = ennemyToExecute.GetComponentsInChildren<Renderer>();
            int a = 0;
            foreach (var item in renderers)
            {
                if (item == ennemyToExecute.GetComponent<Renderer>())
                {
                    renderers[a] = null;
                    break;
                }
                a++;
            }
            foreach (Renderer renderer in renderers)
            {
                if (renderer != null)
                {
                    Material[] materials = renderer.materials;

                    foreach (Material mat in materials)
                    {
                        mat.color = new Color(1 - (timerExcute / timeToExecute), 0f, 0f); // Changez la couleur selon vos besoins
                    }
                }
            }

        }
    }

    private void changeButtonColor()
    {
        
    }
}
