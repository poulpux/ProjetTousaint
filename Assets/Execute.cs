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
    [SerializeField] private List<GameObject> hideInExecute = new List<GameObject>();
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

            foreach (var item in hideInExecute)
            {
                item.SetActive(!can);
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
            canExecuteBis = true;
            executableEnnemy.Add(other.gameObject);
        }

        canExecute.Invoke(canExecuteBis);
    }

    private void isExecuting()
    {
        if(timeToExecute > timerExcute)
        {
            if (ennemyToExecute != null)
            {
                changeEnnemyColor();
                RotateTowardsPosition(ennemyToExecute.transform.position);
            }
            timerExcute +=Time.deltaTime;
            InputManager.Instance.canMoveExecute = false;
            executableEnnemy.Clear();
        }
        else if(InputManager.Instance.canMove == false)
        {
            ennemyToExecute = null;
            InputManager.Instance.canMoveExecute = true;
        }
    }

    private void throwExecution()
    {
        if (executableEnnemy.Count > 0 && timeToExecute < timerExcute)
        {
            timerExcute = 0;
            SortObjectsByDistance();
            ennemyToExecute = executableEnnemy[0];
            ennemyToExecute.GetComponent<BasicsEnnemy>().inExecution();
            Destroy(executableEnnemy[0], timeToExecute);
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

    public void RotateTowardsPosition(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0; // Garder l'objet à plat

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, Time.deltaTime * 1000f);
        }
    }

    void SortObjectsByDistance()
    {
        executableEnnemy.Sort((a, b) =>
            Vector3.Distance(a.transform.position, transform.position)
            .CompareTo(Vector3.Distance(b.transform.position, transform.position))
        );

        // Maintenant, votre liste "objects" est triée par distance par rapport au joueur
    }
}
